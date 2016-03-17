namespace AsciiArtConverter08.Form
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLineUp = new System.Windows.Forms.ToolStripMenuItem();
            this.プロジェクトPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProjCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemProjConv = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProjStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.progConv = new System.Windows.Forms.ToolStripProgressBar();
            this.progProject = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProgProject = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuView,
            this.プロジェクトPToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpenImage,
            this.toolStripSeparator1,
            this.menuItemClose});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(92, 24);
            this.menuFile.Text = "ファイル(&F)";
            // 
            // menuItemOpenImage
            // 
            this.menuItemOpenImage.Name = "menuItemOpenImage";
            this.menuItemOpenImage.Size = new System.Drawing.Size(243, 24);
            this.menuItemOpenImage.Text = "イメージファイルを開く(&O)";
            this.menuItemOpenImage.Click += new System.EventHandler(this.menuItemOpenImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(240, 6);
            // 
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            this.menuItemClose.Size = new System.Drawing.Size(243, 24);
            this.menuItemClose.Text = "閉じる(&X)";
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLineUp});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(68, 24);
            this.menuView.Text = "表示(&V)";
            // 
            // menuLineUp
            // 
            this.menuLineUp.Name = "menuLineUp";
            this.menuLineUp.Size = new System.Drawing.Size(123, 24);
            this.menuLineUp.Text = "整列(L)";
            this.menuLineUp.Click += new System.EventHandler(this.menuLineUp_Click);
            // 
            // プロジェクトPToolStripMenuItem
            // 
            this.プロジェクトPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemProjCreate,
            this.toolStripSeparator3,
            this.menuItemProjConv,
            this.menuItemProjStop});
            this.プロジェクトPToolStripMenuItem.Name = "プロジェクトPToolStripMenuItem";
            this.プロジェクトPToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.プロジェクトPToolStripMenuItem.Text = "プロジェクト(&P)";
            // 
            // menuItemProjCreate
            // 
            this.menuItemProjCreate.Name = "menuItemProjCreate";
            this.menuItemProjCreate.Size = new System.Drawing.Size(234, 24);
            this.menuItemProjCreate.Text = "プロジェクトの作成(&P)";
            this.menuItemProjCreate.Click += new System.EventHandler(this.menuItemProjCreate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(231, 6);
            // 
            // menuItemProjConv
            // 
            this.menuItemProjConv.Name = "menuItemProjConv";
            this.menuItemProjConv.Size = new System.Drawing.Size(234, 24);
            this.menuItemProjConv.Text = "プロジェクトのAA変換(&C)";
            this.menuItemProjConv.Click += new System.EventHandler(this.menuItemProjConv_Click);
            // 
            // menuItemProjStop
            // 
            this.menuItemProjStop.Enabled = false;
            this.menuItemProjStop.Name = "menuItemProjStop";
            this.menuItemProjStop.Size = new System.Drawing.Size(234, 24);
            this.menuItemProjStop.Text = "変換処理の中止(&S)";
            this.menuItemProjStop.Click += new System.EventHandler(this.menuItemProjStop_Click);
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelp});
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(177, 24);
            this.menuItemHelp.Text = "ヘルプの表示(&A)";
            this.menuItemHelp.Click += new System.EventHandler(this.ShowHelp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(10);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage,
            this.lblFileName,
            this.progConv,
            this.progProject,
            this.lblProgProject});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = false;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(300, 17);
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = false;
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(158, 17);
            this.lblFileName.Spring = true;
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progConv
            // 
            this.progConv.Name = "progConv";
            this.progConv.Size = new System.Drawing.Size(200, 16);
            // 
            // progProject
            // 
            this.progProject.Name = "progProject";
            this.progProject.Size = new System.Drawing.Size(200, 16);
            // 
            // lblProgProject
            // 
            this.lblProgProject.AutoSize = false;
            this.lblProgProject.Name = "lblProgProject";
            this.lblProgProject.Size = new System.Drawing.Size(100, 17);
            this.lblProgProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "アスキーアート動画変換ツール　Ver. 0.8.1.1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuLineUp;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.ToolStripStatusLabel lblFileName;
        private System.Windows.Forms.ToolStripProgressBar progConv;
        private System.Windows.Forms.ToolStripProgressBar progProject;
        private System.Windows.Forms.ToolStripStatusLabel lblProgProject;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.ToolStripMenuItem プロジェクトPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemProjCreate;
        private System.Windows.Forms.ToolStripMenuItem menuItemProjConv;
        private System.Windows.Forms.ToolStripMenuItem menuItemProjStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
    }
}

