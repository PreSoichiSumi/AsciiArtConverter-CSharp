using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AsciiArtConverter08.Manager;

namespace AsciiArtConverter08.Form.Dialog
{
	public enum ProjectMode
	{
		画像があればスキップ,
		テキストファイルを利用,
		作り直す
	}

	public partial class FrmConfigProject : System.Windows.Forms.Form
	{
		private ProjectMode mode = ProjectMode.画像があればスキップ;

		private int convertFrame = 1;

        double fps = 0;

        private MainManager mm = null;

        public FrmConfigProject(MainManager mm):this()
        {
            this.mm = mm;
        }

        public string FPS
        {
            get
            {
                return "" + (this.fps / Convert.ToDouble(this.numUDSkip.Value)).ToString("0.00") + "fps";
            }
        }

        public double OrignFPS
        {
            get
            {
                return this.fps;
            }
        }

        public bool MkMp4
        {
            get
            {
                return rdoMkMp4_1.Checked;
            }
        }

        public string FileName
        {
            get
            {
                return this.txtFileName.Text;
            }
        }

        public int BitRate
        {
            get
            {
                if (cmbBitrate.Text != "無圧縮")
                {
                    return Convert.ToInt32(cmbBitrate.Text.Replace("k", "").Replace(",", ""));
                }
                else
                {
                    return 0;
                }
            }
        }

        public string AA_Encoding
        {
            get
            {
                return this.cmbEnc.Text;
            }
        }

		public int ConvertFrame
		{
			get
			{
				return this.convertFrame;
			}
		}

		public ProjectMode ProjectMode
		{
			get
			{
				return mode;
			}
		}
		
		/// <summary>
		/// 有効無効
		/// </summary>
		public new bool Enabled
		{
			set
			{
				pnlBase.Enabled = value;

				if (value == false)
				{
					this.Opacity = 0.9;
				}
				else
				{
					this.Opacity = 1;
				}
			}
		}

		public FrmConfigProject()
		{
			InitializeComponent();

            this.cmbBitrate.SelectedIndex = 6;

            this.cmbEnc.SelectedIndex = 0;

		}

        public bool SaveLine
        {
            get
            {
                return this.chkLine.Checked;
            }
        }


		private void ProjectMode_CheckedChanged(object sender, EventArgs e)
		{
			if (rdoMode1.Checked)
			{
				mode = ProjectMode.画像があればスキップ;
			}

			if (rdoMode2.Checked)
			{
				mode = ProjectMode.テキストファイルを利用;
			}

			if (rdoMode3.Checked)
			{
				mode = ProjectMode.作り直す;
			}
		}

		private void FrmConfigProject_FormClosing(object sender, FormClosingEventArgs e)
		{
            //e.Cancel = true;
            //this.Visible = false;
		}

        private void numUDSkip_ValueChanged(object sender, EventArgs e)
        {
            this.convertFrame = Convert.ToInt32(numUDSkip.Value);

            if (this.fps > 0)
            {
                this.lblFps.Text = "（" + (this.fps / Convert.ToDouble(this.numUDSkip.Value)).ToString("0.00") + "fps）";
            }
        }

        private void numUDSkip_Leave(object sender, EventArgs e)
        {
            try
            {
                this.numUDSkip.Value = Convert.ToInt32(this.numUDSkip.Text);
            }
            catch (Exception)
            {
                this.numUDSkip.Text = "" + this.convertFrame;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.AutoUpgradeEnabled = true;
            openFileDialog.Filter = "プロジェクトファイル|project.aa2";
            openFileDialog.FileName = "project.aa2";
            openFileDialog.Title = "プロジェクトファイルを開く";

            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            this.txtFileName.Text = openFileDialog.FileName;

            this.txtFileName.SelectionStart = this.txtFileName.Text.Length - 1;
            this.txtFileName.SelectionLength = 0;

            using (StreamReader sr = new StreamReader(this.txtFileName.Text))
            {
                string s = sr.ReadLine();
                s = s.Replace("fps", "");
                this.fps = Convert.ToDouble(s);
            }

            this.lblFps.Text = "（" + (this.fps / Convert.ToDouble(this.numUDSkip.Value)).ToString("0.00") + "fps）";

            this.btnOk.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!this.rdoMode3.Checked)
            {
                if (this.mm.ChkConfig(this.txtFileName.Text) != DialogResult.OK)
                {
                    return;
                }
            }

            this.mm.SaveConfig(this.txtFileName.Text);

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void bntCansel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
	}
}
