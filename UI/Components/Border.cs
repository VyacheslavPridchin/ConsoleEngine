using ConsoleEngine.Tools;
using System;

namespace ConsoleEngine.UI.Components
{
    public class Border : Component
    {
        public ConsoleColor BorderColor;
        public Border(NumbersPairInt size, NumbersPairInt location, ConsoleColor borderColor)
        {
            Size = size;
            Location = location;
            BorderColor = borderColor;
        }
        public Border(int width, int height, NumbersPairInt location, ConsoleColor borderColor)
        {
            Width = width;
            Height = height;
            Location = location;
            BorderColor = borderColor;
        }
        public Border(int width, int height, int left, int top, ConsoleColor borderColor)
        {
            Width = width;
            Height = height;
            Left = left;
            Top = top;
            BorderColor = borderColor;
        }
        public override void Draw()
        {
            if (!Enabled) return;

            NumbersPairInt cursorPosition = new NumbersPairInt(Console.CursorLeft, Console.CursorTop);

            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = BorderColor;

            Console.SetCursorPosition(Parent.OffsetLeft + Left, Parent.OffsetTop + Top);
            Console.Write("╔");

            Console.SetCursorPosition(Parent.OffsetLeft + Left + Width - 1, Parent.OffsetTop + Top);
            Console.Write("╗");

            Console.SetCursorPosition(Parent.OffsetLeft + Left + Width - 1, Parent.OffsetTop + Top + Height - 1);
            Console.Write("╝");

            Console.SetCursorPosition(Parent.OffsetLeft + Left, Parent.OffsetTop + Top + Height - 1);
            Console.Write("╚");

            for (int x = Parent.OffsetLeft + Left + 1; x < Parent.OffsetLeft + Left + Width - 1; x++)
            {
                Console.SetCursorPosition(x, Parent.OffsetTop + Top);
                Console.Write("═");
                Console.SetCursorPosition(x, Parent.OffsetTop + Top + Height - 1);
                Console.Write("═");
            }

            for (int y = Parent.OffsetTop + Top + 1; y < Parent.OffsetTop + Top + Height - 1; y++)
            {
                Console.SetCursorPosition(Parent.OffsetLeft + Left, y);
                Console.Write("║");
                Console.SetCursorPosition(Parent.OffsetLeft + Left + Width - 1, y);
                Console.Write("║");
            }

            Console.SetCursorPosition(cursorPosition.X, cursorPosition.Y);
        }
    }
}
