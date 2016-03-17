using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsciiArtConverter08.Manager;
using AsciiArtConverter08.Util;

namespace AsciiArtConverter08.Form
{
    public partial class FrmInfo : System.Windows.Forms.Form
    {
        private MainManager mm = null;

        public FrmInfo()
        {
            InitializeComponent();
        }

        public FrmInfo(MainManager mm)
            : this()
        {
            this.mm = mm;

            this.mm.BeforeStartJob += BeforeStartJob;
        }

        void BeforeStartJob(object o)
        {

            this.txtInfo.Text = InfoUtil.GetInfoAA((Dictionary<string, string>)o) + InfoUtil.GetInfoProject((Dictionary<string, string>)o);

            this.txtInfo.SelectionStart = 0;
            this.txtInfo.SelectionLength = 0;
        }

        private void FrmInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = this.mm.CloseFlag;
        }

        private void FrmInfo_Load(object sender, EventArgs e)
        {
            IntPtr menu = AsciiArtConverter08.Win32.Win32Api.GetSystemMenu(this.Handle, false);
            int menuCount = AsciiArtConverter08.Win32.Win32Api.GetMenuItemCount(menu);
            if (menuCount > 1)
            {
                //メニューの「閉じる」とセパレータを削除
                AsciiArtConverter08.Win32.Win32Api.RemoveMenu(menu, (uint)(menuCount - 1), AsciiArtConverter08.Win32.Win32Api.MF_BYPOSITION | AsciiArtConverter08.Win32.Win32Api.MF_REMOVE);
                AsciiArtConverter08.Win32.Win32Api.RemoveMenu(menu, (uint)(menuCount - 2), AsciiArtConverter08.Win32.Win32Api.MF_BYPOSITION | AsciiArtConverter08.Win32.Win32Api.MF_REMOVE);
                AsciiArtConverter08.Win32.Win32Api.DrawMenuBar(this.Handle);
            }
        }


    }
}
