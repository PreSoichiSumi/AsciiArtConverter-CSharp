using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AsciiArtConverter08.UserControl
{
    class CustomOpenImgFileDialog
    {
        [DllImport("user32.dll")]
        private static extern IntPtr CreateWindowEx(
           uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y,
           int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);
        const uint WS_VISIBLE = 0x10000000;
        static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);
        static readonly IntPtr NULL = IntPtr.Zero;

        public static DialogResult Open(OpenFileDialog dlg,Control c)
        {
            IntPtr dummy = CreateWindowEx(0, "Message", null, WS_VISIBLE, 0, 0, 0, 0,
                HWND_MESSAGE, NULL, NULL, NULL);

            AddPrevControl ac = new AddPrevControl();

            HandleHelperImg hh = new HandleHelperImg(ac);

            hh.AssignHandle(dummy);

            dlg.Title = "ファイルを開く";
            dlg.AutoUpgradeEnabled = false;
                        
            DialogResult result = dlg.ShowDialog(c);

            ac.ReleaseImage();

            hh.DestroyHandle();

            c.Focus();

            return result;
            
        }
    }
}
