using AsciiArtConverter08.UserControl;
namespace AsciiArtConverter08.Form
{
    partial class FrmAscii
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAscii));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemSaveTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveHtml = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveImg = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlImage = new AsciiArtConverter08.UserControl.PanelDBuffer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlImageDetail = new AsciiArtConverter08.UserControl.PanelDBuffer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlImageDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSaveTxt,
            this.menuItemSaveHtml,
            this.menuItemSaveImg});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(231, 70);
            this.contextMenuStrip1.VisibleChanged += new System.EventHandler(this.contextMenuStrip1_VisibleChanged);
            // 
            // menuItemSaveTxt
            // 
            this.menuItemSaveTxt.Name = "menuItemSaveTxt";
            this.menuItemSaveTxt.Size = new System.Drawing.Size(230, 22);
            this.menuItemSaveTxt.Text = "AAをテキストで保存する(&S)";
            this.menuItemSaveTxt.Click += new System.EventHandler(this.menuItemSaveTxt_Click);
            // 
            // menuItemSaveHtml
            // 
            this.menuItemSaveHtml.Name = "menuItemSaveHtml";
            this.menuItemSaveHtml.Size = new System.Drawing.Size(230, 22);
            this.menuItemSaveHtml.Text = "AAをHTMLで保存する(&H)";
            this.menuItemSaveHtml.Click += new System.EventHandler(this.menuItemSaveHtml_Click);
            // 
            // menuItemSaveImg
            // 
            this.menuItemSaveImg.Name = "menuItemSaveImg";
            this.menuItemSaveImg.Size = new System.Drawing.Size(230, 22);
            this.menuItemSaveImg.Text = "AAを画像で保存する(&I)";
            this.menuItemSaveImg.Click += new System.EventHandler(this.menuItemSaveImg_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "png";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(523, 357);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnlImage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(515, 331);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "AAイメージ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlImage
            // 
            this.pnlImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlImage.ContextMenuStrip = this.contextMenuStrip1;
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(3, 3);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(509, 325);
            this.pnlImage.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pnlImageDetail);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(515, 331);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "マッチング結果";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnlImageDetail
            // 
            this.pnlImageDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlImageDetail.ContextMenuStrip = this.contextMenuStrip1;
            this.pnlImageDetail.Controls.Add(this.label2);
            this.pnlImageDetail.Controls.Add(this.label1);
            this.pnlImageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImageDetail.Location = new System.Drawing.Point(3, 3);
            this.pnlImageDetail.Name = "pnlImageDetail";
            this.pnlImageDetail.Size = new System.Drawing.Size(509, 325);
            this.pnlImageDetail.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(180, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "線の角度を考慮しないマッチング結果";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(5, 311);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "線の角度を考慮したマッチング結果";
            // 
            // FrmAscii
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(523, 357);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAscii";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "アスキーアートイメージ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAscii_FormClosing);
            this.Load += new System.EventHandler(this.FrmAscii_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.pnlImageDetail.ResumeLayout(false);
            this.pnlImageDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveTxt;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveHtml;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveImg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private PanelDBuffer pnlImage;
        private System.Windows.Forms.TabPage tabPage2;
        private PanelDBuffer pnlImageDetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}