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

namespace AsciiArtConverter08.Form.Dialog
{
    public partial class FrmConfig :System.Windows.Forms.Form
    {
        private MainManager mm = null;

        private FrmTone ftone = new FrmTone();

        private object lockObject = new object();

        public FrmConfig()
        {
            InitializeComponent();

            this.cmbImgSize.SelectedIndex = 0;

            this.cmbAccuracy.SelectedIndex = 1;

            this.cmbCharSet.SelectedIndex = 2;

            this.cmbFont.SelectedIndex = 1;

            this.cmbMatchCnt.SelectedIndex = 1;

            if (File.Exists(Application.StartupPath + "\\config\\default.ini"))
            {
                OpenConfig(Application.StartupPath + "\\config\\default.ini");
            }

            this.initFlg = true;

            SetConfig();
        }

        public FrmConfig(MainManager mm)
            : this()
        {
            this.mm = mm;
        }

        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = this.mm.CloseFlag;
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            this.IsBusy = true;

            this.mm.StartJob(new Command[] { new Command("細線化") });
        }

        private void config_Changed(object sender, EventArgs e)
        {
            SetConfig();
        }

        public bool IsBusy
        {
            set
            {
                lock (lockObject)
                {
                    this.panel1.Enabled = !value;
                    this.panel2.Enabled = !value;
                    this.panel3.Enabled = !value;
                    this.menuStrip1.Enabled = !value;

                    this.btnLap.Enabled = !value;
                    this.btnAscii.Enabled = !value;

                    Application.DoEvents();
                }
            }
        }

        private void btnAscii_Click(object sender, EventArgs e)
        {
            this.IsBusy = true;

            this.mm.StartJob(new Command[] { new Command("細線化"), new Command("AA") });
        }

        private void FrmConfig_Load(object sender, EventArgs e)
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

        private void btnTxtColor_Click(object sender, EventArgs e)
        {
            this.colorDialog.Color = lblTxtColor.BackColor;
            if (this.colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                lblTxtColor.BackColor = this.colorDialog.Color;
            }
        }

        private void btnCanvasColor_Click(object sender, EventArgs e)
        {
            this.colorDialog.Color = lblTxtColor.BackColor;
            if (this.colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                lblCanvasColor.BackColor = this.colorDialog.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ftone.ToneText = this.toneTxt;
            ftone.Visible = true;
            ftone.Focus();
            ftone.WindowState = FormWindowState.Normal;
        }

        private void menuSaveDefault_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "現在の設定をデフォルトとして保存します。\r\nよろしいですか？", "メッセージ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            SaveConfig(Application.StartupPath + "\\config\\default.ini");
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "設定ファイル（*.ini）|*.ini";
            this.saveFileDialog.DefaultExt = "ini";

            this.saveFileDialog.FileName = "";

            this.saveFileDialog.InitialDirectory = Application.StartupPath + "\\config\\";

            if (this.saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveConfig(saveFileDialog.FileName);
            }
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {

            this.openFileDialog.Filter = "設定ファイル（*.ini）|*.ini|すべてのファイル|*.*";

            this.openFileDialog.FileName = "";

            this.openFileDialog.InitialDirectory = Application.StartupPath + "\\config\\";

            if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                OpenConfig(openFileDialog.FileName);
            }
        }
    }
}
