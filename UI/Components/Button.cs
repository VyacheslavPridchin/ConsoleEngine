using ConsoleEngine.Tools;
using System;

namespace ConsoleEngine.UI.Components
{
    public class Button : Label
    {
        public ConsoleColor BackgroundColorHover = ConsoleColor.DarkGray;
        public ConsoleColor BackgroundColorClick = ConsoleColor.Gray;

        public Button(Frame parent)
        {
            Parent = parent;
            Parent.OnMouseMove += () => CheckMousePosition();
            Parent.OnMouseClick += () => CheckMouseClick();
        }

        public Action onMouseEnter;
        public Action onMouseLeave;
        public Action onMouseDown;
        public Action onMouseUp;

        private bool MouseInButton = false;
        private void CheckMousePosition()
        {
            if ((Parent.State.MousePositionInConsole.X >= Parent.OffsetLeft + Left) &&
                (Parent.State.MousePositionInConsole.X < Parent.OffsetLeft + Left + Width) &&
                (Parent.State.MousePositionInConsole.Y >= Parent.OffsetTop + Top) &&
                (Parent.State.MousePositionInConsole.Y < Parent.OffsetTop + Top + Height))
            {

                if (!MouseInButton)
                {
                    onMouseEnter?.Invoke();
                    MouseInButton = true;
                    Draw();
                }
            }
            else
            {
                if (MouseInButton)
                {
                    onMouseLeave?.Invoke();
                    MouseInButton = false;
                    Draw();
                }
            }
        }

        private bool MouseClick = false;
        private void CheckMouseClick()
        {
            if ((Parent.State.MousePositionInConsole.X >= Parent.OffsetLeft + Left) &&
                (Parent.State.MousePositionInConsole.X < Parent.OffsetLeft + Left + Width) &&
                (Parent.State.MousePositionInConsole.Y >= Parent.OffsetTop + Top) &&
                (Parent.State.MousePositionInConsole.Y < Parent.OffsetTop + Top + Height))
            {
                if (Parent.State.MouseButtonPressed != Frame.FrameState.MouseButton.None)
                {
                    if (!MouseClick)
                    {
                        onMouseDown?.Invoke();
                        MouseClick = true;
                        Draw();
                    }
                }
            }
            if (Parent.State.MouseButtonPressed == Frame.FrameState.MouseButton.None)
            {
                if (MouseClick)
                {
                    onMouseUp?.Invoke();
                    MouseClick = false;
                    Draw();
                }
            }
        }

        public override void Draw()
        {
            if (!Enabled) return;

            NumbersPairInt cursorPosition = new NumbersPairInt(Console.CursorLeft, Console.CursorTop);

            if (MouseClick)
                Frame.Fill(Parent.OffsetLeft + Left, Parent.OffsetTop + Top, Width, Height, BackgroundColorClick);
            else
            if (MouseInButton)
                Frame.Fill(Parent.OffsetLeft + Left, Parent.OffsetTop + Top, Width, Height, BackgroundColorHover);
            else
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

            new Border(Width, Height, Left, Top - Console.CursorTop, TextColor)
            {
                BackgroundColor = MouseClick ? BackgroundColorClick : (MouseInButton ? BackgroundColorHover : BackgroundColor),
                Parent = Parent
            }.Draw();

            Console.SetCursorPosition(cursorPosition.X, cursorPosition.Y);
        }
    }
}
