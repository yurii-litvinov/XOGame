namespace XOGame
{
    using System;
    using Ubiq.Graphics;

    class XOButton
    {
        public enum StateEnum
        {
            Empty,
            X,
            O
        }

        public StateEnum State
        {
            get
            {
                return state;
            }
            set
            {
                var str = "X";
                switch (value)
                {
                    case StateEnum.Empty:
                        throw new Exception("Can not set button state to Empty");
                    case StateEnum.O:
                        str = "O";
                        break;
                    case StateEnum.X:
                        str = "X";
                        break;
                }
                control.Text = str;
                //owner.ReDrawGraphics();
            }
        }

        public VisualElement Control
        {
            get
            {
                return control;
            }
        }

        public int X
        {
            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        public event Action<XOButton> Clicked = (sender) => { };

        public XOButton(int x, int y, MExtendedThreadApp owner)
        {
            control = new Button
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,

                // VerticalAlignment.Stretch and HorizontalAlignment.Stretch make buttons appear in lower right corner,
                // instead of stretching buttons to a whole grid cell, so setting Height and Width properties manually.
                Height = owner.Screen.Height / 3 - 5,
                Width = owner.Screen.Width / 3 - 5,

                Background = new SolidColorBrush(Colors.DarkGray),
                Text = " "
            };

            control.Pressed += (s, e) => Clicked(this);
            this.x = x;
            this.y = y;
            this.owner = owner;
        }

        private readonly Button control;

        private StateEnum state = StateEnum.Empty;

        private readonly MExtendedThreadApp owner;

        private readonly int x;

        private readonly int y;
    }
}
