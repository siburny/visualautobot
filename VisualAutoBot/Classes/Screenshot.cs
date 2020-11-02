using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace VisualAutoBot
{
    using HWND = IntPtr;

    public static class ScreenUtilities
    {
        public static Image CaptureScreen()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        /// 
        /*public static Bitmap CaptureWindow(IntPtr handle)
        {
            RECT rect = new RECT();
            GetWindowRect(handle, ref rect);

            return CaptureWindow(handle, rect);
        }*/

        public static Bitmap CaptureWindow(IntPtr handle/*, RECT rect*/)
        {
            RECT rect = new RECT();
            GetWindowRect(handle, ref rect);

            IntPtr hdcSrc = GetWindowDC(handle);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            IntPtr hdcDest = CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
            IntPtr hOld = SelectObject(hdcDest, hBitmap);

            BitBlt(hdcDest, 0, 0, width, height, hdcSrc, rect.left, rect.top, SRCCOPY);

            SelectObject(hdcDest, hOld);

            DeleteDC(hdcDest);
            ReleaseDC(handle, hdcSrc);

            Bitmap img = Bitmap.FromHbitmap(hBitmap);

            DeleteObject(hBitmap);
            return img;
        }

        public static HWND GetWindowByName(string name)
        {
            var all = GetOpenWindows();
            foreach (var i in all)
            {
                if (i.Value.Contains(name))
                    return i.Key;
            }

            return default(IntPtr);
        }

        private static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("USER32.DLL")]
        static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        static extern IntPtr GetShellWindow();

        public const int SRCCOPY = 0x00CC0020;
        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hObjectSource,
            int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
