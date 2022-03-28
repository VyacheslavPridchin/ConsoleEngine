using System;
using ConsoleEngine.UI;
using ConsoleEngine.UI.Components;

namespace ConsoleEngine.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Frame frame = new Frame();

            Button button = new Button(frame)
            {
                Text = "Example Button",
                Width = 20,
                Height = 3,
            };

            Label label = new Label()
            {
                Text = "Example Label",
                Width = 20,
                Height = 3,
                Top = 3,
                TextColor = ConsoleColor.Magenta,
            };

            Border border = new Border(20, 10, 0, 6, ConsoleColor.Green);

            frame.Components.Add(button);
            frame.Components.Add(label);
            frame.Components.Add(border);

            frame.Show();

            Console.WriteLine("Console.WriteLine method after Frame.Show()...");
            Console.Write("...and");
            Console.Write(" Console.Write");
            Console.Write(" methods.");

        }
    }
}
