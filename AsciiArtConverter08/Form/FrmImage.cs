using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsciiArtConverter08.Manager;
using System.IO;

namespace AsciiArtConverter08
{
    public partial class FrmImage : System.Windows.Forms.Form
    {
        private MainManager mm = null;

        private Image img = null;

        private object lockObject = new object();

        private string fileName = "";

        public FrmImage()
        {
            InitializeComponent();
        }

        public FrmImage(MainManager mm)
            : this()
        {
            this.mm = mm;

            this.fileName = Application.StartupPath + "\\image\\title.png";

            ShowImageFile(this.fileName);
        }

        private void FrmImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = this.mm.CloseFlag;
        }

        public void ShowImageFile(string filename)
        {
            lock (lockObject)
            {
                if (this.pnlImage.BackgroundImage != null)
                {
                    Image img = this.pnlImage.BackgroundImage;
                    this.pnlImage.BackgroundImage = null;
                    img.Dispose();
                }

                this.pnlImage.BackgroundImage = Image.FromFile(filename);

                this.img = this.pnlImage.BackgroundImage;

                this.fileName = filename;

                this.Text = "画像イメージ　" + new FileInfo(this.fileName).Name + "　[" + this.img.Width + " x " + this.img.Height + "]";
            }
        }

        public Image Image
        {
            get
            {
                lock (lockObject)
                {
                    return this.img;
                }
            }
        }

        public string FileName
        {
            get
            {
                lock (lockObject)
                {
                    return this.fileName;
                }
            }

            set
            {
                lock (lockObject)
                {
                    ShowImageFile(value);
                }
            }
        }

        private void FrmImage_Load(object sender, EventArgs e)
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
