namespace XOGame
{
    using Ubiq.Graphics;
    using System;
    
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

            for (int x = 0; x < 3; ++x)
            {
                for (int y = 0; y < 3; ++y)
                {
                    field[x, y] = XOButton.StateEnum.Empty;
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

            field[button.X, button.Y] = button.State;
            if (CheckPosition())
            {
                Win();
            }
        }

        private void Win()
        {
            var label = new TextBlock()
            {
                Text = "Win!",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(new Color(255, 0, 255, 0)),
                Font = new Font(new FontFamily("Arial"), 40)
            };
            Screen.Content = label;
        }

        private bool CheckPosition()
        {
            return CheckPosition((x, y) => Tuple.Create(x, y))  // Vertical lines
                || CheckPosition((x, y) => Tuple.Create(y, x))  // Horizontal lines
                || CheckPosition((x, y) => Tuple.Create(y, y))  // Main diagonal
                || CheckPosition((x, y) => Tuple.Create(2 - y, y));  // Secondary diagonal
        }

        private bool CheckPosition(Func<int, int, Tuple<int, int>> direction)
        {
            for (int x = 0; x < 3; ++x)
            {
                var firstRowState = field[direction(x, 0).Item1, 0];
                if (firstRowState == XOButton.StateEnum.Empty)
                {
                    continue;
                }

                var count = 1;
                for (int y = 1; y < 3; ++y)
                {
                    if (field[direction(x, y).Item1, direction(x, y).Item2] == firstRowState)
                    {
                        ++count;
                    }
                }
                if (count == 3)
                {
                    return true;
                }
            }
            return false;
        }


        private enum TurnEnum 
        {
            X,
            O
        }

        private TurnEnum turn = TurnEnum.X;
        private XOButton.StateEnum[,] field = new XOButton.StateEnum[3, 3];
    }
}
