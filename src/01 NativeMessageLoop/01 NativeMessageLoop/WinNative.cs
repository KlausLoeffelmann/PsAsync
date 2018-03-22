using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static PInvoke.User32;
using static PInvoke.Kernel32;

namespace NativeMessageLoop
{

    public sealed class WinNative
    {

        private WinNative()
        {
        }

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassEx")]
        public extern static System.UInt16 RegisterClassEx([In()] ref WNDCLASSEX lpWndClass);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowEx")]
        public extern static IntPtr CreateWindowEx(int dwExStyle, UInt16 regResult, string lpWindowName, 
            UInt32 dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public extern static bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public extern static sbyte GetMessage(ref MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        public extern static sbyte PeekMessage(ref MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        public extern static bool TranslateMessage([In()] ref MSG lpMsg);

        [DllImport("user32.dll")]
        public extern static IntPtr DispatchMessage([In()] ref MSG lpmsg);

        [DllImport("user32.dll")]
        public extern static void PostQuitMessage(int nExitCode);

        private const uint COLOR_WINDOW = 5;
        private const uint COLOR_BACKGROUND = 3;
        private const uint IDC_CROSS = 32515;

        public delegate IntPtr WndProcDel(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static IntPtr CreateWindow(string windowTitel, WndProcDel wndProc, Rectangle rec)
        {
            WNDCLASSEX wind_class = new WNDCLASSEX();

            wind_class.cbSize = Marshal.SizeOf(typeof(WNDCLASSEX));
            wind_class.style = (int)(ClassStyles.CS_HREDRAW | ClassStyles.CS_VREDRAW | ClassStyles.CS_DBLCLKS);
            wind_class.hbrBackground = ((IntPtr)COLOR_BACKGROUND) + 1;
            wind_class.cbClsExtra = 0;
            wind_class.cbWndExtra = 0;
            wind_class.hInstance = Process.GetCurrentProcess().Handle;
            wind_class.hIcon = IntPtr.Zero;
            wind_class.hCursor = LoadCursor(IntPtr.Zero, new IntPtr(IDC_CROSS));
            wind_class.lpszMenuName = null;
            wind_class.lpszClassName = "winClass";
            wind_class.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(wndProc);
            wind_class.hIconSm = IntPtr.Zero;

            ushort regResult = RegisterClassEx(ref wind_class);

            if (regResult == 0)
            {
                var error = GetLastError();
            }
            string wndClass = wind_class.lpszClassName;

            IntPtr hWnd = CreateWindowEx(0, regResult, windowTitel, Convert.ToUInt32(WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_VISIBLE), 
                                         rec.X, rec.Y, rec.Width, rec.Height, IntPtr.Zero, IntPtr.Zero, wind_class.hInstance, IntPtr.Zero);

            if (hWnd == (new IntPtr(0)))
            {
                var error = GetLastError();
            }
            ShowWindow(hWnd, WindowShowStyle.SW_SHOWNORMAL);
            UpdateWindow(hWnd);
            return hWnd;
        }

        public static IntPtr CreateButton(string caption, IntPtr ParentWindow, Rectangle rec, ushort uniqueId)
        {

            var hwnd = PInvoke.User32.CreateWindow("BUTTON", caption, WindowStyles.WS_CHILD | WindowStyles.WS_VISIBLE, 
                        rec.X, rec.Y, rec.Width, rec.Height, ParentWindow, new IntPtr(uniqueId), Process.GetCurrentProcess().Handle, IntPtr.Zero);
            return hwnd;

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WNDCLASSEX
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int style;
            public IntPtr lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }
    }
}

