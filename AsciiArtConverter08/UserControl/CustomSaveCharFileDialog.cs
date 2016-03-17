using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AsciiArtConverter08.UserControl
{
    class CustomSaveCharFileDialog
    {
        [DllImport("user32.dll")]
        private static extern IntPtr CreateWindowEx(
           uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y,
           int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);
        const uint WS_VISIBLE = 0x10000000;
        static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);
        static readonly IntPtr NULL = IntPtr.Zero;

        public static DialogResult Save(SaveFileDialog dlg, Control c, ref string charSet)
        {
            IntPtr dummy = CreateWindowEx(0, "Message", null, WS_VISIBLE, 0, 0, 0, 0,
                HWND_MESSAGE, NULL, NULL, NULL);

            AddCharSetControl ac = new AddCharSetControl();

            HandleHelperChar hh = new HandleHelperChar(ac);

            hh.AssignHandle(dummy);

            dlg.Title = "名前をつけて保存";
            dlg.AutoUpgradeEnabled = false;

            DialogResult result = dlg.ShowDialog(c);

            hh.DestroyHandle();

            c.Focus();

            charSet = ac.CharSet;

            return result;

        }
    }
}
