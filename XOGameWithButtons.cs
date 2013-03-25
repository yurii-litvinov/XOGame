namespace XOGame
{
    using Ubiq.Graphics;
    
    public sealed class XOGameWithButtons: MExtendedThreadApp
    {
        protected override void MainOverride()
        {
            Screen.Init();
            
            const int gridSize = 3;
            var grid = new Grid(gridSize, gridSize)
            {
                Margin = new Thickness(5, 5, 5, 5),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            for (int x = 0; x < 3; ++x)
            {
                for (int y = 0; y < 3; ++y)
                {
                    var button = new XOButton(x, y, this);
                    button.Clicked += OnButtonClicked;
                    grid[x, y] = button.Control;
                }
            }

            Screen.Content = grid;
            Screen.ControlMode = true;
            
            WaitForInput("7890\r");            
        }

        void OnButtonClicked(XOButton button)
        {
            if (button.State != XOButton.StateEnum.Empty)
            {
                return;
            }

            switch (turn)
            {
                case TurnEnum.X:
                    button.State = XOButton.StateEnum.X;
                    turn = TurnEnum.O;
                    break;
                case TurnEnum.O:
                    button.State = XOButton.StateEnum.O;
                    turn = TurnEnum.X;
                    break;
            }

            CheckPosition();
        }

        private void CheckPosition()
        {
        }

        private enum TurnEnum 
        {
            X,
            O
        }

        private TurnEnum turn = TurnEnum.X;
    }
}
