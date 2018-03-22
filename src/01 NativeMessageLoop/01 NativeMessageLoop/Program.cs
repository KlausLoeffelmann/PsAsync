using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PInvoke;
using static PInvoke.User32;

namespace NativeMessageLoop
{
    static class Program
    {
        public const int CAUSE_WORKLOAD_BUTTON_ID = 42;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var hWndOfMainWin = WinNative.CreateWindow("Native Windows Message Loop", 
                                                       WndProc, 
                                                       new Rectangle(100, 100, 700, 400)); // Window Pos and Size


            var buttonHwnd = WinNative.CreateButton("Do some work",                 //The Name of the Button.
                                                    hWndOfMainWin,                  // Parent Windows.
                                                    new Rectangle(50, 50, 100, 50), // Size of Button.
                                                    CAUSE_WORKLOAD_BUTTON_ID);      // Identifier for Message Queue
            DoMessageLoop();
        }

        unsafe public static void DoMessageLoop()
        {
            MSG msg = default;

            //Get Message Waits for a Message until it returns not 0.
            while (GetMessage(&msg, IntPtr.Zero, 0, 0) != 0)
            {
                //When we need to get WM_KEYDOWN, WM_KEYUP, we need to do this:
                TranslateMessage(&msg);
                //Messages get dispatched to WndProc.
                DispatchMessage(&msg);
            }
        }

        private static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            WindowMessage wMsg = (WindowMessage)msg;

            var ID_MENU = ((IntPtr) wParam).ToInt32() & 0b11111111;

            if (wMsg == WindowMessage.WM_COMMAND)
            {
                Button1_Click();
            }

            else if (wMsg == WindowMessage.WM_DESTROY)
            {
                PostQuitMessage(0);
            }
            else
            {
            }

            //Those messages we did not process, are getting processed here:
            return DefWindowProc(hWnd, wMsg, (IntPtr)wParam, (IntPtr)lParam);
        }


        unsafe public static void DoEvents()
        {
            MSG msg = default;

            if (PeekMessage(&msg, IntPtr.Zero, 0, 0, 0))
            {
                GetMessage(&msg, IntPtr.Zero, 0, 0);
                //When we need to get WM_KEYDOWN, WM_KEYUP, we need to do this:
                TranslateMessage(&msg);
                //Messages get dispatched to WndProc.
                DispatchMessage(&msg);
            }
        }

        /// <summary>
        /// Runs the long lasting operation.
        /// </summary>
        private static void Button1_Click()
        {
            for (var z = int.MinValue; z < int.MaxValue; z++)
            {
                //This is just another way to burn processor workload like in a loop.
                //(But in addition the hardware can be informed that it is busy waiting).
                Thread.SpinWait(2);
                //When we want to have this non-blocking, we need to uncomment this.
                DoEvents();
            }
        }
        
    }
}
