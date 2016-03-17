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
using AsciiArtConverter08.UserControl;
using System.Web;
using System.Diagnostics;
using AsciiArtConverter08.Manager.Data;

namespace AsciiArtConverter08.Form
{
    public partial class FrmAscii : System.Windows.Forms.Form
    {
        private MainManager mm = null;

        private object lockObject = new object();

        private string aa = "";

        private bool isHtmlTarget = false;

        private Image img = null;

        private Dictionary<char, CharData> charDic = new Dictionary<char, CharData>();

        public FrmAscii()
        {
            InitializeComponent();

            this.saveFileDialog.InitialDirectory = Application.StartupPath + "\\";
        }

        public FrmAscii(MainManager mm)
            : this()
        {
            this.mm = mm;
        }

        private void FrmAscii_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = this.mm.CloseFlag;
        }

        public Image Image
        {
            get
            {
                return this.img;
            }
        }
        
        private delegate void DrawAADelegate(string[] aaall, Size imgsize, ConfigManager cm, CharManager charm);
 

        public void DrawAA(string[] aaall,Size imgsize,ConfigManager cm, CharManager charm)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DrawAADelegate(DrawAA), new object[] { aa, imgsize, cm, charm });
            }

            lock (lockObject)
            {
                if (this.pnlImage.BackgroundImage != null)
                {
                    this.pnlImage.BackgroundImage.Dispose();
                }

                if (this.pnlImageDetail.BackgroundImage != null)
                {
                    this.pnlImageDetail.BackgroundImage.Dispose();
                }

                Bitmap bmpAA = new Bitmap(imgsize.Width, imgsize.Height, PixelFormat.Format32bppArgb);
                Bitmap bmpType = new Bitmap(imgsize.Width, imgsize.Height, PixelFormat.Format32bppArgb);

                string aa = aaall[0];
                string type = aaall[1];

                char[] cs_aa = aa.Replace("\r\n", "\n").ToCharArray();
                char[] cs_type = type.Replace("\r\n", "\n").ToCharArray();

                int x = 0;
                int y = 0;
                int addY = 0;

                if (cm.Pitch == 2)
                {
                    addY = 1;
                }

                using (Graphics gType = Graphics.FromImage(bmpType))
                {
                    using (Graphics gAA = Graphics.FromImage(bmpAA))
                    {
                        using (Brush brush = new SolidBrush(cm.TextColor))
                        {
                            gAA.Clear(cm.CanvasColor);
                            gType.Clear(Color.White);

                            for (int index = 0; index < cs_aa.Length; index++)
                            {
                                char c = cs_aa[index];
                                char t = '\0';

                                if (cs_type.Length > index)
                                {
                                    t = cs_type[index];
                                }
                                else
                                {
                                    t = '0';
                                }

                                if (c == '\n')
                                {
                                    x = 0;
                                    y = y + charm[0].Height;
                                    continue;
                                }

                                gAA.DrawString(Convert.ToString(c), cm.Font, brush, x, y + addY);

                                if (t == '0')
                                {
                                    gType.DrawString(Convert.ToString(c), cm.Font, Brushes.Blue, x, y + addY);
                                }
                                else
                                {
                                    gType.DrawString(Convert.ToString(c), cm.Font, Brushes.Red, x, y + addY);
                                }

                                if (charm[c] == null)
                                {
                                    if (!this.charDic.ContainsKey(c))
                                    {
                                        this.charDic[c] = new CharData(c, cm.Font, cm.Pitch);
                                    }

                                    x += this.charDic[c].Width;
                                }
                                else
                                {
                                    x += charm[c].Width;
                                }
                            }
                        }
                    }
                }

                this.pnlImage.BackgroundImage = bmpAA;
                this.pnlImageDetail.BackgroundImage = bmpType;


                if (cm.Font.Name == "ＭＳ Ｐゴシック" && cm.Font.Size == 12.0f && cm.Pitch == 2)
                {
                    this.isHtmlTarget = true;
                }
                else
                {
                    this.isHtmlTarget = false;
                }

                this.aa = aa;
                this.img = bmpAA;
            }
        }

        private void contextMenuStrip1_VisibleChanged(object sender, EventArgs e)
        {
            if (this.mm.IsBusy || this.aa == "")
            {
                this.menuItemSaveTxt.Enabled = false;
                this.menuItemSaveHtml.Enabled = false;
                this.menuItemSaveImg.Enabled = false;
            }
            else
            {
                this.menuItemSaveTxt.Enabled = true;
                this.menuItemSaveHtml.Enabled = true;
                this.menuItemSaveImg.Enabled = true;
            }
        }

        private void menuItemSaveTxt_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "テキスト(*.txt)|*.txt";
            this.saveFileDialog.DefaultExt = ".txt";

            string filename = new FileInfo(this.mm.FileName).Name;

            if (filename.LastIndexOf('.') >= 0)
            {
                filename = filename.Substring(0, filename.LastIndexOf('.'));
            }

            filename = filename + "_AA";

            this.saveFileDialog.FileName = filename;

            if (this.saveFileDialog.InitialDirectory == "")
            {
                this.saveFileDialog.InitialDirectory = Application.StartupPath + "\\";
            }

            string charSet = "";

            if (CustomSaveCharFileDialog.Save(this.saveFileDialog, this, ref charSet) == DialogResult.OK)
            {
                filename = this.saveFileDialog.FileName;

                using (StreamWriter sw = new StreamWriter(filename, false, Encoding.GetEncoding(charSet)))
                {
                    sw.Write(this.aa);
                }
            }
        }

        private void menuItemSaveImg_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "PNG(*.png)|*.png";

            string filename = new FileInfo(this.mm.FileName).Name;

            if (filename.LastIndexOf('.') >= 0)
            {
                filename = filename.Substring(0, filename.LastIndexOf('.'));
            }

            filename = filename + "_AA";

            this.saveFileDialog.FileName = filename;

            if (this.saveFileDialog.InitialDirectory == "")
            {
                this.saveFileDialog.InitialDirectory = Application.StartupPath + "\\";
            }

            if (this.saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                filename = this.saveFileDialog.FileName;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    ((Bitmap)this.pnlImage.BackgroundImage).Save(filename, ImageFormat.Png);
                }
                else
                {
                    ((Bitmap)this.pnlImageDetail.BackgroundImage).Save(filename, ImageFormat.Png);
                }
            }
        }

        private void menuItemSaveHtml_Click(object sender, EventArgs e)
        {
            if (!this.isHtmlTarget)
            {
                DialogResult result = MessageBox.Show(this, "このAAは「ＭＳ Ｐゴシック 12Pt　行間 2ピクセル」ではないため、HTMLにした場合にズレが発生します。\r\n\r\n続行しますか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result != DialogResult.OK)
                {
                    return;
                }
            }

            this.saveFileDialog.Filter = "HTML (*.html) | *.html";
            this.saveFileDialog.DefaultExt = "html";

            string filename = new FileInfo(this.mm.FileName).Name;

            if (filename.LastIndexOf('.') >= 0)
            {
                filename = filename.Substring(0, filename.LastIndexOf('.'));
            }

            filename = filename + "_AA";

            this.saveFileDialog.FileName = filename;

            string s1_UTF = "<html>\n<head>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\n<title>アスキーアート</title>\n</head>\n<body style=\"font-family:'ＭＳ Ｐゴシック';font-size:16px;line-height:18px;\">";
            string s1_SJIS = "<html>\n<head>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=Windows-31J\">\n<title>アスキーアート</title>\n</head>\n<body style=\"font-family:'ＭＳ Ｐゴシック';font-size:16px;line-height:18px;\">";
            string s2 = "</body>\n</html>";

            string s = this.aa.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r\n", "<br>\r\n");

            //Debug.Write(s.IndexOf("\n"));

            //saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string charSet = "";

            if (this.saveFileDialog.InitialDirectory == "")
            {
                this.saveFileDialog.InitialDirectory = Application.StartupPath + "\\";
            }

            if (CustomSaveCharFileDialog.Save(this.saveFileDialog, this, ref charSet) == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.GetEncoding(charSet)))
                {
                    if (charSet == "UTF-8")
                    {
                        sw.WriteLine(s1_UTF);
                    }
                    else
                    {
                        sw.WriteLine(s1_SJIS);
                    }
                    sw.WriteLine(s);
                    sw.WriteLine(s2);
                }
            }
        }

        private void FrmAscii_Load(object sender, EventArgs e)
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
