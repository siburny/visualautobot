using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot
{
    class MouseControl
    {
        static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        static MousePoint GetCursorPosition()
        {
            var gotPoint = GetCursorPos(out MousePoint currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            MouseEvent
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        const int SPEED = 250;
        const int STEPS = 25;
        public static void Click(int x, int y)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            MousePoint start = GetCursorPosition();
            double dx = 1.0 * (x - start.X) / STEPS,
                dy = 1.0 * (y - start.Y) / STEPS;

            t.Interval = SPEED / STEPS;
            int i = 0;
            t.Elapsed += (sender, args) =>
            {
                SetCursorPosition((int)(start.X + dx * i), (int)(start.Y + dy * i));
                if (i++ == STEPS)
                {
                    t.Stop();
                }
            };
            t.Start();

            while (t.Enabled)
            {
                Application.DoEvents();
            }
            t.Dispose();

            //MouseEvent(MouseEventFlags.LeftDown | MouseEventFlags.LeftUp);
        }

        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        private static extern void MouseEvent(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
