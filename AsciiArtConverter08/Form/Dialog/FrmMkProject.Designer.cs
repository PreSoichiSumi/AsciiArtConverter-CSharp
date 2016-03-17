namespace AsciiArtConverter08.Form.Dialog
{
    partial class FrmMkProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMkProject));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.txtProjectDirName = new System.Windows.Forms.TextBox();
            this.txtMovieFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFps = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgJob = new System.Windows.Forms.ToolStripProgressBar();
            this.btnStart = new System.Windows.Forms.Button();
            this.folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.openDlg = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDir);
            this.groupBox1.Controls.Add(this.btnFile);
            this.groupBox1.Controls.Add(this.txtProjectDirName);
            this.groupBox1.Controls.Add(this.txtMovieFileName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 163);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ファイル及びフォルダ設定";
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(373, 112);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(38, 27);
            this.btnDir.TabIndex = 1;
            this.btnDir.Text = "参照";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(373, 41);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(38, 27);
            this.btnFile.TabIndex = 0;
            this.btnFile.Text = "参照";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // txtProjectDirName
            // 
            this.txtProjectDirName.Location = new System.Drawing.Point(8, 112);
            this.txtProjectDirName.Multiline = true;
            this.txtProjectDirName.Name = "txtProjectDirName";
            this.txtProjectDirName.ReadOnly = true;
            this.txtProjectDirName.Size = new System.Drawing.Size(359, 42);
            this.txtProjectDirName.TabIndex = 4;
            this.txtProjectDirName.TabStop = false;
            // 
            // txtMovieFileName
            // 
            this.txtMovieFileName.Location = new System.Drawing.Point(8, 41);
            this.txtMovieFileName.Multiline = true;
            this.txtMovieFileName.Name = "txtMovieFileName";
            this.txtMovieFileName.ReadOnly = true;
            this.txtMovieFileName.Size = new System.Drawing.Size(359, 42);
            this.txtMovieFileName.TabIndex = 1;
            this.txtMovieFileName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "プロジェクト作成フォルダ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "変換対象動画ファイル";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFps);
            this.groupBox2.Controls.Add(this.lbl5);
            this.groupBox2.Controls.Add(this.lbl4);
            this.groupBox2.Controls.Add(this.lbl3);
            this.groupBox2.Controls.Add(this.lbl2);
            this.groupBox2.Controls.Add(this.lbl1);
            this.groupBox2.Location = new System.Drawing.Point(439, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 163);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "進捗状況";
            // 
            // lblFps
            // 
            this.lblFps.ForeColor = System.Drawing.Color.Black;
            this.lblFps.Location = new System.Drawing.Point(6, 69);
            this.lblFps.Name = "lblFps";
            this.lblFps.Size = new System.Drawing.Size(218, 14);
            this.lblFps.TabIndex = 5;
            this.lblFps.Visible = false;
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.ForeColor = System.Drawing.Color.Black;
            this.lbl5.Location = new System.Drawing.Point(6, 142);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(45, 12);
            this.lbl5.TabIndex = 4;
            this.lbl5.Text = "５　終了";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.ForeColor = System.Drawing.Color.Black;
            this.lbl4.Location = new System.Drawing.Point(6, 115);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(189, 12);
            this.lbl4.TabIndex = 3;
            this.lbl4.Text = "４　動画ファイルから音楽ファイルの取得";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.ForeColor = System.Drawing.Color.Black;
            this.lbl3.Location = new System.Drawing.Point(6, 86);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(189, 12);
            this.lbl3.TabIndex = 2;
            this.lbl3.Text = "３　動画ファイルから画像ファイルの取得";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.ForeColor = System.Drawing.Color.Black;
            this.lbl2.Location = new System.Drawing.Point(6, 54);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(137, 12);
            this.lbl2.TabIndex = 1;
            this.lbl2.Text = "２　動画ファイルの情報取得";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.ForeColor = System.Drawing.Color.Blue;
            this.lbl1.Location = new System.Drawing.Point(6, 26);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(136, 12);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "１　ファイル及びフォルダ設定";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage,
            this.prgJob});
            this.statusStrip1.Location = new System.Drawing.Point(0, 225);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(681, 23);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(564, 18);
            this.lblMessage.Spring = true;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // prgJob
            // 
            this.prgJob.AutoSize = false;
            this.prgJob.MarqueeAnimationSpeed = 0;
            this.prgJob.Name = "prgJob";
            this.prgJob.Size = new System.Drawing.Size(100, 17);
            this.prgJob.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(300, 181);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(79, 34);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "開始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // folderDlg
            // 
            this.folderDlg.Description = "プロジェクト作成フォルダ";
            // 
            // openDlg
            // 
            this.openDlg.Filter = "動画ファイル|*.avi;*.mpg;*.mp4;*.flv;*.f4v;*.wmv;*.mov|全てのファイル(*.*)|*.*";
            this.openDlg.Title = "動画ファイルの選択";
            // 
            // FrmMkProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(681, 248);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMkProject";
            this.Text = "プロジェクトの作成";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMkProject_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.TextBox txtProjectDirName;
        private System.Windows.Forms.TextBox txtMovieFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar prgJob;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.FolderBrowserDialog folderDlg;
        private System.Windows.Forms.OpenFileDialog openDlg;
        private System.Windows.Forms.Label lblFps;
    }
}