using ConsoleEngine.Tools;
using System;


namespace ConsoleEngine.UI.Components
{
    public abstract class Component
    {
        public bool Enabled = true;
        public Frame Parent;
        public int Left = 0;

        private int top = 0;
        public int Top
        {
            get
            {
                return top;
            }
            set
            {
                top = Console.CursorTop + value;
            }
        }

        public int Width = 3, Height = 5;
        public NumbersPairInt Location
        {
            get
            {
                return new NumbersPairInt(Top, Left);
            }
            set
            {
                Left = value.X;
                Top = value.Y;
            }
        }

        public NumbersPairInt Size
        {
            get
            {
                return new NumbersPairInt(Width, Height);
            }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public ConsoleColor BackgroundColor = ConsoleColor.Black;
        public abstract void Draw();
    }
}
