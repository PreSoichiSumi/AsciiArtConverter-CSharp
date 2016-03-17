using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace AsciiArtConverter08.UserControl
{
    class HandleHelperImg:NativeWindow
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

        public HandleHelperImg(Control addControl)
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
                            pos.cx += addControl.Width;
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

            addControl.Location = new Point((int)(_DialogClientRect.Width - addControl.Width), 0);
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

    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPOS
    {
        public IntPtr hwnd;
        public IntPtr hwndAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public uint flags;

        #region Overrides
        public override string ToString()
        {
            return x + ":" + y + ":" + cx + ":" + cy + ":" + ((SWP_Flags)flags).ToString();
        }
        #endregion
    }
    internal enum ZOrderPos
    {
        HWND_TOP = 0,
        HWND_BOTTOM = 1,
        HWND_TOPMOST = -1,
        HWND_NOTOPMOST = -2
    }

    [Flags]
    internal enum SWP_Flags
    {
        SWP_NOSIZE = 0x0001,
        SWP_NOMOVE = 0x0002,
        SWP_NOZORDER = 0x0004,
        SWP_NOACTIVATE = 0x0010,
        SWP_FRAMECHANGED = 0x0020, /* The frame changed: send WM_NCCALCSIZE */
        SWP_SHOWWINDOW = 0x0040,
        SWP_HIDEWINDOW = 0x0080,
        SWP_NOOWNERZORDER = 0x0200, /* Don't do owner Z ordering */

        SWP_DRAWFRAME = SWP_FRAMECHANGED,
        SWP_NOREPOSITION = SWP_NOOWNERZORDER
    }

    [Flags]
    internal enum SetWindowPosFlags
    {
        SWP_NOSIZE = 0x0001,
        SWP_NOMOVE = 0x0002,
        SWP_NOZORDER = 0x0004,
        SWP_NOREDRAW = 0x0008,
        SWP_NOACTIVATE = 0x0010,
        SWP_FRAMECHANGED = 0x0020,
        SWP_SHOWWINDOW = 0x0040,
        SWP_HIDEWINDOW = 0x0080,
        SWP_NOCOPYBITS = 0x0100,
        SWP_NOOWNERZORDER = 0x0200,
        SWP_NOSENDCHANGING = 0x0400,
        SWP_DRAWFRAME = 0x0020,
        SWP_NOREPOSITION = 0x0200,
        SWP_DEFERERASE = 0x2000,
        SWP_ASYNCWINDOWPOS = 0x4000
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        #region Properties

        public POINT Location
        {
            get { return new POINT((int)left, (int)top); }
            set
            {
                right -= (left - value.x);
                bottom -= (bottom - value.y);
                left = value.x;
                top = value.y;
            }
        }

        internal uint Width
        {
            get { return (uint)Math.Abs(right - left); }
            set { right = left + (int)value; }
        }

        internal uint Height
        {
            get { return (uint)Math.Abs(bottom - top); }
            set { bottom = top + (int)value; }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return left + ":" + top + ":" + right + ":" + bottom;
        }
        #endregion
    }


    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;

        #region Constructors
        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public POINT(Point point)
        {
            x = point.X;
            y = point.Y;
        }
        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWINFO
    {
        public UInt32 cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public UInt32 dwStyle;
        public UInt32 dwExStyle;
        public UInt32 dwWindowStatus;
        public UInt32 cxWindowBorders;
        public UInt32 cyWindowBorders;
        public UInt16 atomWindowType;
        public UInt16 wCreatorVersion;
    } 



}
