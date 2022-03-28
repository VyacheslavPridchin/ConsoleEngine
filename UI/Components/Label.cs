using ConsoleEngine.Tools;
using System;

namespace ConsoleEngine.UI.Components
{
    public class Label : Component
    {
        public string Text = "∙•●●•∙";
        public HorizontalAlignment HorizontalAlignment = HorizontalAlignment.Center;
        public VerticalAlignment VerticalAlignment = VerticalAlignment.Middle;
        public ConsoleColor TextColor = ConsoleColor.White;
        public override void Draw()
        {

            if (!Enabled) return;

            NumbersPairInt cursorPosition = new NumbersPairInt(Console.CursorLeft, Console.CursorTop);

            Frame.Fill(Parent.OffsetLeft + Left, Parent.OffsetTop + Top, Width, Height, BackgroundColor);

            int left = 0;
            int top = 0;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    left = Parent.OffsetLeft + Left;
                    break;
                case HorizontalAlignment.Center:
                    left = Parent.OffsetLeft + Left + (Width - Text.Length) / 2;
                    break;
                case HorizontalAlignment.Right:
                    left = Parent.OffsetLeft + Left + Width - Text.Length;
                    break;
            }

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    top = Parent.OffsetTop + Top;
                    break;
                case VerticalAlignment.Middle:
                    top = Parent.OffsetTop + Top + Height / 2;
                    break;
                case VerticalAlignment.Bottom:
                    top = Parent.OffsetTop + Top + Height - 1;
                    break;
            }

            if (left < 0) left = 0;
            if (top < 0) top = 0;

            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = TextColor;
            Console.Write(Text);

            Console.SetCursorPosition(cursorPosition.X, cursorPosition.Y);
        }
    }
}