using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsciiArtConverter08.Manager;
using AsciiArtConverter08.UserControl;
using System.Threading;
using System.IO;
using AsciiArtConverter08.Form.Dialog;

namespace AsciiArtConverter08.Form
{
    public partial class FrmMain : System.Windows.Forms.Form
    {
        private MainManager mm = null;
        private FrmProcessCansel fcansel = new FrmProcessCansel();
        private bool closeReq = false;

        public FrmMain()
        {
            InitializeComponent();

            this.openFileDialog.InitialDirectory = Application.StartupPath + "\\";

            Application.Idle += Application_Idle;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            this.mm = new MainManager(this);

            this.mm.BeforeAAConvert += BeforeAAConvert;
            this.mm.AfterAAConvert += AfterAAConvert;
            this.mm.BeforeStartJob += BeforeStartJob;
            this.mm.AfterEndJob += AfterEndJob;
            this.mm.GoNextProjext += GoNextProjext;

            Application.Idle -= Application_Idle;
        }

        void GoNextProjext(object o)
        {
            int i = (int)o;

            this.ProgProjVal = i;
            this.LabelProgProject = "[" + this.ProgProjVal + "/" + this.ProgProjMax + "]";
        }

        void AfterEndJob(object o)
        {
            //this.ProgProjVal = this.ProgProjMax;
            this.LabelProgProject = "[" + this.ProgProjVal + "/" + this.ProgProjMax + "]";

            this.menuItemOpenImage.Enabled = true;
            this.menuItemProjCreate.Enabled = true;
            this.menuItemProjConv.Enabled = true;
            this.menuItemProjStop.Enabled = false;

            if (o != null)
            {
                Dictionary<string, string> dic = (Dictionary<string, string>)o;

                this.LabelMessage = "プロジェクト変換終了" + "　" + dic["time"];
            }
        }

        void BeforeStartJob(object o)
        {
            Dictionary<string, string> dic = (Dictionary<string, string>)o;

            this.ProgProjVal = 0;
            this.ProgProjMax = Convert.ToInt32(dic["max"]);
            this.LabelProgProject = "[0/" + this.ProgProjMax + "]";
            this.LabelMessage = "";

            this.menuItemOpenImage.Enabled = false;
            this.menuItemProjCreate.Enabled = false;
            this.menuItemProjConv.Enabled = false;
            this.menuItemProjStop.Enabled = true;
        }

        void AfterAAConvert(object o)
        {
            Dictionary<string,string> dic = (Dictionary<string,string>)o;

            this.LabelMessage = "アスキーアート変換終了" + "　" + dic["time"];
            this.ProgProjVal = this.ProgProjVal + 1;
        }

        void BeforeAAConvert(object o)
        {
            Dictionary<string,string> dic = (Dictionary<string,string>)o;

            this.LabelFileName = dic["filename"];
            this.LabelMessage = "アスキーアート変換中";
            //this.ProgProjMax = 1;
            //this.ProgProjVal = 0;
        }

        public Size ClientRealSize
        {
            get
            {
                Size s = base.ClientSize;
                s.Height = s.Height - this.statusStrip1.Height - this.menuStrip1.Height;
                return s;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Enabled = false;

            if (this.mm.IsBusy)
            {
                DialogResult result = MessageBox.Show(this, "現在、アプリケーションは作業中です。\r\n作業を中断してアプリケーションを終了しますか？", "メッセージ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    this.Enabled = true;
                    e.Cancel = true;
                    return;
                }

                fcansel.Left = this.Left + (this.Width - fcansel.Width) / 2;
                fcansel.Top = this.Top + (this.Height - fcansel.Height) / 2;

                fcansel.Show(this);

                this.closeReq = true;

                this.mm.Cansel = true;

                return;
            }

            this.mm.CloseFlag = false;
            
            if (e.Cancel == true)
            {
                this.Close();
            }
        }

        private void menuLineUp_Click(object sender, EventArgs e)
        {
            this.mm.LineUp();
        }

        public string LabelMessage
        {
            set
            {
                this.lblMessage.Text = value;
            }

            get
            {
                return this.lblMessage.Text;
            }
        }

        public string LabelFileName
        {
            set
            {
                this.lblFileName.Text = value;
            }

            get
            {
                return this.lblFileName.Text;
            }
        }

        public int ProgConvMax
        {
            set
            {
                this.progConv.Maximum = value;
            }
            get
            {
                return this.progConv.Maximum;
            }
        }

        public int ProgConvVal
        {
            set
            {
                this.progConv.Value = value;
            }
            get
            {
                return this.progConv.Value;
            }
        }

        public int ProgProjMax
        {
            set
            {
                this.progProject.Maximum = value;
            }
            get
            {
                return this.progProject.Maximum;
            }
        }

        public int ProgProjVal
        {
            set
            {
                this.progProject.Value = value;
            }
            get
            {
                return this.progProject.Value;
            }
        }

        public string LabelProgProject
        {
            set
            {
                this.lblProgProject.Text = value;
            }
            get
            {
                return lblProgProject.Text;
            }
        }

        private void menuItemOpenImage_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "すべてのピクチャファイル(*.bmp,*.png,*.jpg,*.jpeg,*.gif)|*.bmp;*.png;*.jpg;*.jpeg;*.gif";

            openFileDialog.FileName = "";

            if (CustomOpenImgFileDialog.Open(openFileDialog, this) == DialogResult.OK)
            {
                this.mm.ShowImageFile(openFileDialog.FileName);
            }
        }

        public void AfterProcess()
        {
            Thread.Sleep(200);

            fcansel.Visible = false;
            if (closeReq)
            {
                this.Close();
            }
        }

        private void menuItemProjCreate_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Application.StartupPath + "\\ffmpeg\\ffmpeg.exe"))
            {
                MessageBox.Show(this, "「ffmpeg.exe」が必要です。詳しくは「マニュアル.pdf」を読んでください。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (FrmMkProject frm = new FrmMkProject())
            {
                frm.ShowDialog(this);
            }
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItemProjConv_Click(object sender, EventArgs e)
        {
            this.mm.DoProject();
        }

        private void menuItemProjStop_Click(object sender, EventArgs e)
        {
            this.mm.Cansel = true;

            fcansel.Left = this.Left + (this.Width - fcansel.Width) / 2;
            fcansel.Top = this.Top + (this.Height - fcansel.Height) / 2;

            fcansel.Show(this);
        }

        private void ShowHelp(object sender, EventArgs e)
        {
            using (FrmHelp f = new FrmHelp())
            {
                f.ShowDialog(this);
            }
            
        }
    }
}
