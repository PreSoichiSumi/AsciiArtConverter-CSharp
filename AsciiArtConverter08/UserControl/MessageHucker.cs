using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AsciiArtConverter08.UserControl
{
    class MessageHucker:NativeWindow
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);

        const int WM_NOTIFY = 0x004E;

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public uint code;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OFNOTIFY
        {
            public NMHDR hdr;
            public IntPtr OPENFILENAME;
            public IntPtr fileNameShareViolation;
        }

        public enum DialogChangeStatus : long
        {
            CDN_FIRST = 0xFFFFFDA7,
            CDN_INITDONE = (CDN_FIRST - 0x0000),
            CDN_SELCHANGE = (CDN_FIRST - 0x0001),
            CDN_FOLDERCHANGE = (CDN_FIRST - 0x0002),
            CDN_SHAREVIOLATION = (CDN_FIRST - 0x0003),
            CDN_HELP = (CDN_FIRST - 0x0004),
            CDN_FILEOK = (CDN_FIRST - 0x0005),
            CDN_TYPECHANGE = (CDN_FIRST - 0x0006),
        }

        Control addControl = null;
        IntPtr parent = IntPtr.Zero;

        public MessageHucker(Control addControl,IntPtr parent)
        {
            this.addControl = addControl;
            this.parent = parent;
        }

        protected override void WndProc(ref Message m)
        {
            //Debug.WriteLine(m);

            if (m.Msg == WM_NOTIFY)
            {
                OFNOTIFY ofNotify = (OFNOTIFY)Marshal.PtrToStructure(m.LParam, typeof(OFNOTIFY));
                if (ofNotify.hdr.code == (uint)DialogChangeStatus.CDN_SELCHANGE)
                {
                    StringBuilder filePath = new StringBuilder(256);
                    uint cd = 0x0465;//CDM_GETFILEPATH;
                    SendMessage(parent, cd, (IntPtr)256, filePath);

                    if (filePath.ToString() != "")
                    {
                        if (filePath.ToString().ToLower().EndsWith("bmp")
                            || filePath.ToString().ToLower().EndsWith("png")
                            || filePath.ToString().ToLower().EndsWith("jpg"))
                        {
                            ((AddPrevControl)addControl).SetImage(filePath.ToString());
                        }
                    }
                }

            }

            base.WndProc(ref m);
        }
    }
}
