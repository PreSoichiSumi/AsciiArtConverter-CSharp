using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace AsciiArtConverter08.UserControl
{
    class HandleHelperChar:NativeWindow
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, SetWindowPosFlags flags);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hwnd, ref RECT rect);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetClientRect(IntPtr hwnd, ref RECT rect);
        [DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hwnd, int x, int y, 
            int nWidth,int nHeight, int bRepaint);
        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsCallBack lpEnumFunc, int lParam);
        internal delegate bool EnumWindowsCallBack(IntPtr hWnd, int lParam);
        [DllImport("User32.Dll")]
        public static extern void GetClassName(IntPtr hWnd, StringBuilder param, int length);
        [DllImport("User32.Dll")]
        public static extern int GetDlgCtrlID(IntPtr hWndCtl);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowInfo(IntPtr hwnd, out WINDOWINFO pwi);

        


        private const SetWindowPosFlags UFLAGSZORDER =
                SetWindowPosFlags.SWP_NOACTIVATE |
                SetWindowPosFlags.SWP_NOMOVE |
                SetWindowPosFlags.SWP_NOSIZE;

        const int WM_ACTIVATE = 0x0006;
        const int WM_WINDOWPOSCHANGING = 0x0046;
        const int WM_SHOWWINDOW = 0x0018;
       

        


        IntPtr fDlgHndle = IntPtr.Zero;

        bool initialized = false;

        Control addControl = null;

        MessageHucker cl = null;

        private RECT _DialogWindowRect = new RECT();
        private RECT _DialogClientRect = new RECT();

        public HandleHelperChar(Control addControl)
        {
            this.addControl = addControl;
        }

        protected override void WndProc(ref Message m)
        {
            //Debug.WriteLine(m);

            if (m.Msg == WM_SHOWWINDOW)
            {
                initialized = true;
                InitControls();
            }

            if (m.Msg == WM_ACTIVATE)
            {
                if (fDlgHndle == IntPtr.Zero)
                {
                    fDlgHndle = m.LParam;
                    ReleaseHandle();
                    AssignHandle(fDlgHndle);
                }
            }

            if (m.Msg == WM_WINDOWPOSCHANGING)
            {
                if (!initialized)
                {
                    WINDOWPOS pos = (WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(WINDOWPOS));
                    //Debug.WriteLine(pos.flags);
                    if (pos.flags != 0 && ((pos.flags & (int)SWP_Flags.SWP_NOSIZE) != (int)SWP_Flags.SWP_NOSIZE))
                    {
                        if (m.HWnd == fDlgHndle)
                        {
                            pos.cy += addControl.Height;
                            Marshal.StructureToPtr(pos, m.LParam, true);
                        }
                    }
                }

            }

            base.WndProc(ref m);
        }

        private void InitControls()
        {
            GetClientRect(fDlgHndle, ref _DialogClientRect);
            GetWindowRect(fDlgHndle, ref _DialogWindowRect);

            PopulateWindowsHandlers();

            addControl.Location = new Point(112, (int)(_DialogClientRect.Height - addControl.Height));
            SetParent(addControl.Handle, fDlgHndle);
            SetWindowPos(addControl.Handle, (IntPtr)ZOrderPos.HWND_BOTTOM, 0, 0, 0, 0, UFLAGSZORDER);
            MoveWindow(fDlgHndle,100,100,640,480,0);
        }

        private void PopulateWindowsHandlers()
        {
            EnumChildWindows(fDlgHndle, new EnumWindowsCallBack(FileDialogEnumWindowCallBack), 0);
        }

        private bool FileDialogEnumWindowCallBack(IntPtr hwnd, int lParam)
        {
            StringBuilder className = new StringBuilder(256);
            GetClassName(hwnd, className, className.Capacity);
            int controlID = GetDlgCtrlID(hwnd);
            WINDOWINFO windowInfo;
            GetWindowInfo(hwnd, out windowInfo);

            Debug.WriteLine(className + "@" + controlID + "@" + Convert.ToString(hwnd.ToInt32(),16));

            if (className.ToString() == "#32770")
            {
                cl = new MessageHucker(this.addControl, fDlgHndle);
                cl.ReleaseHandle();
                cl.AssignHandle(hwnd);
            }

            return true;
        }
    }
}
