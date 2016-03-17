using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsciiArtConverter08.Manager;
using System.Drawing.Imaging;
using System.IO;

namespace AsciiArtConverter08.Form
{
    public partial class FrmLine : System.Windows.Forms.Form
    {
        private MainManager mm = null;
        private Image img = null;

        private object lockObject = new object();

        public FrmLine()
        {
            InitializeComponent();

            this.saveFileDialog.InitialDirectory = Application.StartupPath + "\\";
        }

        public FrmLine(MainManager mm)
            : this()
        {
            this.mm = mm;
        }

        private void FrmLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = this.mm.CloseFlag;
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

            set
            {
                lock (lockObject)
                {
                    this.img = value;

                    if (this.pnlImage.BackgroundImage != null)
                    {
                        Image img = this.pnlImage.BackgroundImage;
                        this.pnlImage.BackgroundImage = null;
                        img.Dispose();
                    }

                    this.pnlImage.BackgroundImage = this.img;

                    this.Text = "輪郭イメージ　[" + this.img.Width + " x " + this.img.Height + "]";

                    //this.img.Save("c:\\aaa.png", ImageFormat.Png);
                }
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "PNG(*.png)|*.png";

            string filename = new FileInfo(this.mm.FileName).Name;

            if (filename.LastIndexOf('.')>=0){
                filename = filename.Substring(0,filename.LastIndexOf('.'));
            }

            filename = filename + "_Line";

            this.saveFileDialog.FileName = filename;

            if (this.saveFileDialog.InitialDirectory == "")
            {
                this.saveFileDialog.InitialDirectory = Application.StartupPath + "\\";
            }

            if (this.saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                filename = this.saveFileDialog.FileName;

                ((Bitmap)this.pnlImage.BackgroundImage).Save(filename, ImageFormat.Png);
            }
        }

        private void contextMenuStrip1_VisibleChanged(object sender, EventArgs e)
        {
            if (this.mm.IsBusy || this.pnlImage.BackgroundImage == null)
            {
                this.menuItemSave.Enabled = false;
            }
            else
            {
                this.menuItemSave.Enabled = true;
            }
        }

        private void FrmLine_Load(object sender, EventArgs e)
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
