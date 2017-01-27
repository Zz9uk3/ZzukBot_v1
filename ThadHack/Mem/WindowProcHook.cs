using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZzukBot.Constants;

namespace ZzukBot.Mem
{
    internal static class WindowProcHook
    {
        private const int GWL_WNDPROC = -4;
        // ReSharper disable once UnusedMember.Local
        private const int WM_CLOSE = 0x0010;
        // ReSharper disable once UnusedMember.Local
        private const int WM_DESTROY = 0x0002;

        private static IntPtr _hWnd = IntPtr.Zero;
        private static IntPtr _oldCallback;
        private static WinImports.WindowProc _newCallback;
        private static bool Applied;

        private static bool WindowProc(IntPtr hWnd, IntPtr lParam)
        {
            int procId;
            WinImports.GetWindowThreadProcessId(hWnd, out procId);
            if (procId == Memory.Reader.Process.Id)
            {
                if (WinImports.IsWindowVisible(hWnd))
                {
                    var l = WinImports.GetWindowTextLength(hWnd);
                    if (l != 0)
                    {
                        var builder = new StringBuilder(l + 1);
                        WinImports.GetWindowText(hWnd, builder, builder.Capacity);
                        if (builder.ToString() == "World of Warcraft")
                        {
                            _hWnd = hWnd;
                        }
                    }
                }
            }
            return true;
        }

        public static void Init()
        {
            if (Applied) return;
            WinImports.EnumWindows(WindowProc, IntPtr.Zero);
            _newCallback = WndProc; // Pins WndProc - will not be garbage collected.
            _oldCallback = WinImports.SetWindowLong(_hWnd, GWL_WNDPROC,
                Marshal.GetFunctionPointerForDelegate(_newCallback));
            Applied = true;
        }

        internal static void SendUpDown(Keys parKey)
        {
            var tmpThr = new Thread(delegate()
            {
                WinImports.SendMessage((int) _hWnd, (uint) Action.WM_KEYDOWN, (int) parKey, 0);
                Thread.Sleep(100);
                WinImports.SendMessage((int) _hWnd, (uint) Action.WM_KEYUP, (int) parKey, 0);
            });
            tmpThr.Start();
        }

        internal static void Send(Action parAction, Keys parKey)
        {
            WinImports.SendMessage((int) _hWnd, (uint) parAction, (int) parKey, 0);
        }


        private static int WndProc(IntPtr hWnd, int Msg, int wParam, int lParam)
        {
            //Console.WriteLine("hWnd: " + hWnd.ToString("X8") + " | Msg: " + Msg + " | wParam: " + wParam + " | lParam: " + lParam);   
            return WinImports.CallWindowProc(_oldCallback, hWnd, Msg, wParam, lParam);
        }

        internal enum Action
        {
            WM_SYSKEYDOWN = 260,
            WM_SYSKEYUP = 261,
            WM_CHAR = 258,
            WM_KEYDOWN = 256,
            WM_KEYUP = 257
        }
    }
}