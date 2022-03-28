using ConsoleEngine.Tools;
using ConsoleEngine.UI.Components;
using System;
using System.Collections.Generic;
using static ConsoleEngine.Tools.NativeMethods;

namespace ConsoleEngine.UI
{
    public class Frame
    {
        public Frame()
        {
            ConsoleListener.MouseEvent += MouseHandler;
            ConsoleListener.KeyEvent += KeyHandler;
            State.MousePositionInConsole = new NumbersPair();
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        public static int AbsoluteMinWidth = 0;
        public static int AbsoluteMinHeight = 0;

        private int MinWidth = 0;
        private int MinHeight = 0;
        public static Action OnRepaint;

        public int OffsetLeft
        {
            get
            {
                if (Console.WindowWidth < MinWidth) return 0;
                else
                    return (Console.WindowWidth - MinWidth) / 2;
            }
        }

        public int OffsetTop = 0;

        public Action OnMouseClick;
        public Action OnMouseWheel;
        public Action OnMouseMove;
        public Action OnKeyboardActive;

        public struct FrameState
        {
            public enum MouseButton
            {
                None = 0, Left = 1, Right = 2, LeftRight = 3, Middle = 4, LeftMiddle = 5, RightMiddle = 6, LeftRightMiddle = 7
            }

            public MouseButton MouseButtonPressed;
            public int MouseWheelDirection;

            public NumbersPair MousePositionInConsole;
            public bool KeyPressed;
            public bool ControlKeyPressed;
            public char LastKeyChar;
        }

        public FrameState State = new FrameState();
        public ConsoleColor BackgroundColor = ConsoleColor.Black;
        public ConsoleColor TextColor = ConsoleColor.White;

        public List<Component> Components = new List<Component>();

        bool Initialized = false;
        public void Show()
        {
            void Initialize()
            {
                if (Initialized) return;
                ConsoleListener.Start();
                OnRepaint += () => Show();
                Initialized = true;
            }

            foreach (Component component in Components)
            {
                if (component.Left + component.Width > MinWidth)
                {
                    MinWidth = component.Left + component.Width;
                }

                if (component.Top + component.Height > MinHeight)
                {
                    MinHeight = component.Top + component.Height;
                }

                if (Console.WindowWidth < MinWidth)
                {
                    Console.SetBufferSize(MinWidth, Console.WindowHeight);
                    Console.SetWindowSize(MinWidth, Console.WindowHeight);
                }

                if (Console.WindowHeight < MinHeight)
                {
                    Console.SetBufferSize(Console.WindowWidth, MinHeight);
                    Console.SetWindowSize(Console.WindowWidth, MinHeight);
                }
            }

            if (AbsoluteMinWidth < MinWidth) AbsoluteMinWidth = MinWidth;
            if (AbsoluteMinHeight < MinHeight + OffsetTop) AbsoluteMinHeight = MinHeight + OffsetTop;

            foreach (Component component in Components)
            {
                component.Parent = this;
                component.Draw();
            }

            IntPtr inHandle = GetStdHandle(STD_INPUT_HANDLE);
            uint mode = 0;
            GetConsoleMode(inHandle, ref mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE; //disable
            mode |= ENABLE_WINDOW_INPUT; //enable (if you want)
            mode |= ENABLE_MOUSE_INPUT; //enable

            SetConsoleMode(inHandle, mode);

            Initialize();

            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            Console.SetCursorPosition(0, Frame.AbsoluteMinHeight + 1);
        }


        private uint PreviousMouseCode;
        private void MouseHandler(MOUSE_EVENT_RECORD r)
        {
            //MouseWheelUp = 7864320, MouseWheelDown = 4287102976

            bool mousePositionChanged = false, mouseButtonChanged = false, mouseWheelChanged = false;

            var mousePosition = new NumbersPair(r.dwMousePosition.X, r.dwMousePosition.Y);

            if (!State.MousePositionInConsole.Equals(mousePosition))
                mousePositionChanged = true;

            if (!State.MouseButtonPressed.Equals((FrameState.MouseButton)r.dwButtonState))
                mouseButtonChanged = true;

            if (PreviousMouseCode != r.dwButtonState)
            {
                mouseWheelChanged = true;
            }

            PreviousMouseCode = r.dwButtonState;

            if (r.dwButtonState >= 4287102976)
            {
                State.MouseWheelDirection = -1;
                State.MouseButtonPressed = (FrameState.MouseButton)(r.dwButtonState - 4287102976);
            }
            else
            if (r.dwButtonState >= 7864320)
            {
                State.MouseWheelDirection = 1;
                State.MouseButtonPressed = (FrameState.MouseButton)(r.dwButtonState - 7864320);
            }
            else
            {
                State.MouseWheelDirection = 0;
                State.MouseButtonPressed = (FrameState.MouseButton)r.dwButtonState;
            }

            State.MousePositionInConsole = mousePosition;

            if (mousePositionChanged)
                OnMouseMove?.Invoke();

            if (mouseButtonChanged)
                OnMouseClick?.Invoke();

            if (mouseWheelChanged || mouseButtonChanged)
                OnMouseWheel?.Invoke();
        }
        private void KeyHandler(KEY_EVENT_RECORD r)
        {
            State.LastKeyChar = (char)r.AsciiChar;
            State.ControlKeyPressed = r.dwControlKeyState == 0 ? false : true;
            State.KeyPressed = r.bKeyDown;

            OnKeyboardActive?.Invoke();
        }
        public static void Fill(int Left, int Top, int Width, int Height, ConsoleColor backgroundColor)
        {
            Console.BackgroundColor = backgroundColor;

            for (int x = Left; x < Left + Width; x++)
            {
                for (int y = Top; y < Top + Height; y++)
                {
                    Console.CursorSize = 100;
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }
    }
}
