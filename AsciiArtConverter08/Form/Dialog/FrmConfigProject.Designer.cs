namespace AsciiArtConverter08.Form.Dialog
{
	partial class FrmConfigProject
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigProject));
            this.pnlBase = new System.Windows.Forms.Panel();
            this.bntCansel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblFps = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numUDSkip = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoMkMp4_2 = new System.Windows.Forms.RadioButton();
            this.cmbBitrate = new System.Windows.Forms.ComboBox();
            this.rdoMkMp4_1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEnc = new System.Windows.Forms.ComboBox();
            this.chkLine = new System.Windows.Forms.CheckBox();
            this.rdoMode3 = new System.Windows.Forms.RadioButton();
            this.rdoMode2 = new System.Windows.Forms.RadioButton();
            this.rdoMode1 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlBase.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDSkip)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.bntCansel);
            this.pnlBase.Controls.Add(this.btnOk);
            this.pnlBase.Controls.Add(this.groupBox3);
            this.pnlBase.Controls.Add(this.groupBox2);
            this.pnlBase.Controls.Add(this.groupBox1);
            this.pnlBase.Controls.Add(this.groupBox4);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Location = new System.Drawing.Point(3, 3);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(418, 392);
            this.pnlBase.TabIndex = 0;
            // 
            // bntCansel
            // 
            this.bntCansel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bntCansel.Location = new System.Drawing.Point(230, 343);
            this.bntCansel.Name = "bntCansel";
            this.bntCansel.Size = new System.Drawing.Size(106, 37);
            this.bntCansel.TabIndex = 5;
            this.bntCansel.Text = "キャンセル";
            this.bntCansel.UseVisualStyleBackColor = true;
            this.bntCansel.Click += new System.EventHandler(this.bntCansel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(90, 343);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(106, 37);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "実行";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblFps);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numUDSkip);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 288);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 44);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "処理フレーム";
            // 
            // lblFps
            // 
            this.lblFps.Location = new System.Drawing.Point(248, 21);
            this.lblFps.Name = "lblFps";
            this.lblFps.Size = new System.Drawing.Size(155, 12);
            this.lblFps.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "フレーム毎にアスキーアートに変換する";
            // 
            // numUDSkip
            // 
            this.numUDSkip.Location = new System.Drawing.Point(10, 19);
            this.numUDSkip.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numUDSkip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDSkip.Name = "numUDSkip";
            this.numUDSkip.Size = new System.Drawing.Size(46, 19);
            this.numUDSkip.TabIndex = 0;
            this.numUDSkip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUDSkip.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDSkip.ValueChanged += new System.EventHandler(this.numUDSkip_ValueChanged);
            this.numUDSkip.Leave += new System.EventHandler(this.numUDSkip_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoMkMp4_2);
            this.groupBox2.Controls.Add(this.cmbBitrate);
            this.groupBox2.Controls.Add(this.rdoMkMp4_1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 81);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "アスキーアート動画作成処理";
            // 
            // rdoMkMp4_2
            // 
            this.rdoMkMp4_2.AutoSize = true;
            this.rdoMkMp4_2.Location = new System.Drawing.Point(10, 55);
            this.rdoMkMp4_2.Name = "rdoMkMp4_2";
            this.rdoMkMp4_2.Size = new System.Drawing.Size(174, 16);
            this.rdoMkMp4_2.TabIndex = 3;
            this.rdoMkMp4_2.Text = "アスキーアート動画を作成しない";
            this.rdoMkMp4_2.UseVisualStyleBackColor = true;
            // 
            // cmbBitrate
            // 
            this.cmbBitrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBitrate.FormattingEnabled = true;
            this.cmbBitrate.Items.AddRange(new object[] {
            "4,000k",
            "8,000k",
            "16,000k",
            "24,000k",
            "32,000k",
            "40,000k",
            "無圧縮"});
            this.cmbBitrate.Location = new System.Drawing.Point(186, 18);
            this.cmbBitrate.Name = "cmbBitrate";
            this.cmbBitrate.Size = new System.Drawing.Size(72, 20);
            this.cmbBitrate.TabIndex = 1;
            // 
            // rdoMkMp4_1
            // 
            this.rdoMkMp4_1.AutoSize = true;
            this.rdoMkMp4_1.Checked = true;
            this.rdoMkMp4_1.Location = new System.Drawing.Point(10, 22);
            this.rdoMkMp4_1.Name = "rdoMkMp4_1";
            this.rdoMkMp4_1.Size = new System.Drawing.Size(164, 16);
            this.rdoMkMp4_1.TabIndex = 0;
            this.rdoMkMp4_1.TabStop = true;
            this.rdoMkMp4_1.Text = "アスキーアート動画を作成する";
            this.rdoMkMp4_1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbEnc);
            this.groupBox1.Controls.Add(this.chkLine);
            this.groupBox1.Controls.Add(this.rdoMode3);
            this.groupBox1.Controls.Add(this.rdoMode2);
            this.groupBox1.Controls.Add(this.rdoMode1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 149);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "アスキーアート変換処理";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "アスキーアートの文字コード";
            // 
            // cmbEnc
            // 
            this.cmbEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnc.FormattingEnabled = true;
            this.cmbEnc.Items.AddRange(new object[] {
            "Shift_JIS",
            "UTF-8"});
            this.cmbEnc.Location = new System.Drawing.Point(145, 112);
            this.cmbEnc.Name = "cmbEnc";
            this.cmbEnc.Size = new System.Drawing.Size(72, 20);
            this.cmbEnc.TabIndex = 4;
            // 
            // chkLine
            // 
            this.chkLine.AutoSize = true;
            this.chkLine.Location = new System.Drawing.Point(10, 85);
            this.chkLine.Name = "chkLine";
            this.chkLine.Size = new System.Drawing.Size(210, 16);
            this.chkLine.TabIndex = 3;
            this.chkLine.Text = "アスキーアート作成時に線画も保存する";
            this.chkLine.UseVisualStyleBackColor = true;
            // 
            // rdoMode3
            // 
            this.rdoMode3.AutoSize = true;
            this.rdoMode3.Location = new System.Drawing.Point(10, 63);
            this.rdoMode3.Name = "rdoMode3";
            this.rdoMode3.Size = new System.Drawing.Size(94, 16);
            this.rdoMode3.TabIndex = 2;
            this.rdoMode3.Text = "すべて作り直す";
            this.rdoMode3.UseVisualStyleBackColor = true;
            this.rdoMode3.CheckedChanged += new System.EventHandler(this.ProjectMode_CheckedChanged);
            // 
            // rdoMode2
            // 
            this.rdoMode2.AutoSize = true;
            this.rdoMode2.Location = new System.Drawing.Point(10, 40);
            this.rdoMode2.Name = "rdoMode2";
            this.rdoMode2.Size = new System.Drawing.Size(365, 16);
            this.rdoMode2.TabIndex = 1;
            this.rdoMode2.Text = "すでにアスキーアートののテキストファイルがあれば利用し、画像は作り直す";
            this.rdoMode2.UseVisualStyleBackColor = true;
            this.rdoMode2.CheckedChanged += new System.EventHandler(this.ProjectMode_CheckedChanged);
            // 
            // rdoMode1
            // 
            this.rdoMode1.AutoSize = true;
            this.rdoMode1.Checked = true;
            this.rdoMode1.Location = new System.Drawing.Point(10, 18);
            this.rdoMode1.Name = "rdoMode1";
            this.rdoMode1.Size = new System.Drawing.Size(279, 16);
            this.rdoMode1.TabIndex = 0;
            this.rdoMode1.TabStop = true;
            this.rdoMode1.Text = "すでにアスキーアートの画像ファイルがあれば作成しない";
            this.rdoMode1.UseVisualStyleBackColor = true;
            this.rdoMode1.CheckedChanged += new System.EventHandler(this.ProjectMode_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnOpen);
            this.groupBox4.Controls.Add(this.txtFileName);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(418, 58);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "プロジェクト";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(357, 18);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(46, 19);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "参照";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(10, 18);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(341, 19);
            this.txtFileName.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FrmConfigProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 398);
            this.Controls.Add(this.pnlBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfigProject";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "プロジェクト変換設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConfigProject_FormClosing);
            this.pnlBase.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDSkip)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdoMode3;
		private System.Windows.Forms.RadioButton rdoMode2;
		private System.Windows.Forms.RadioButton rdoMode1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numUDSkip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoMkMp4_2;
        private System.Windows.Forms.ComboBox cmbBitrate;
        private System.Windows.Forms.RadioButton rdoMkMp4_1;
        private System.Windows.Forms.CheckBox chkLine;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button bntCansel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblFps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbEnc;

	}
}