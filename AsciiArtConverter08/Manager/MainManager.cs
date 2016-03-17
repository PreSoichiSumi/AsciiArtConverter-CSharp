using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using AsciiArtConverter08.Form;
using AsciiArtConverter08.Util;
using System.Threading;
using System.Drawing.Imaging;
using AsciiArtConverter08.Form.Dialog;

namespace AsciiArtConverter08.Manager
{
    public partial class MainManager
    {
        private FrmImage fimage = null;
        private FrmLine fline = null;
        private FrmAscii fascii = null;
        private FrmInfo finfo = null;
        private FrmConfig fconfig = null;

        private FrmMain fmain = null;

        private bool closeFlg = true;

        private BackgroundWorker bg = null;

        private ConfigManager cm = null;

        private CharManager charm = null;

        private bool isBusy = false;

        public delegate void Handler(object o);

        public event Handler BeforeStartJob;
        public event Handler AfterEndJob;
        public event Handler BeforeAAConvert;
        public event Handler AfterAAConvert;
        public event Handler GoNextProjext;

        private FrmConfigProject fcp = null;


        public MainManager(FrmMain fmain)
        {
            this.fmain = fmain;

            this.bg = new BackgroundWorker();

            this.cm = new ConfigManager();

            this.bg.DoWork += new DoWorkEventHandler(bg_DoWork);

            this.bg.ProgressChanged += new ProgressChangedEventHandler(bg_ProgressChanged);

            this.bg.WorkerReportsProgress = true;

            SetupForms();

        }

        void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Command command = (Command)e.UserState;

            if (command.CommandString == "細線化")
            {
                this.fline.Image = (Image)command.Value;
            }

            if (command.CommandString == "開始")
            {
                Dictionary<string, string> dic = this.cm.ConfigDic;
                dic["max"] = command.Value.ToString();

                if (this.BeforeStartJob != null)
                {
                    this.BeforeStartJob(dic);
                }

                this.fconfig.IsBusy = true;
            }

            if (command.CommandString == "終了")
            {
                if (command.Value != null)
                {
                    long dif = (long)command.Value;
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic["time"] = (Convert.ToDouble(dif) / 10000000.0d / 60).ToString("0.00") + "分";
                    command.Value = dic;
                }

                if (this.AfterEndJob != null)
                {
                    this.AfterEndJob(command.Value);
                }

                this.fconfig.IsBusy = false;
            }

            if (command.CommandString == "AA開始")
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["filename"]  = this.fimage.FileName;

                if (this.BeforeAAConvert != null)
                {
                    this.BeforeAAConvert(dic);
                }
            }

            if (command.CommandString == "AA終了")
            {
                this.fmain.LabelMessage = "アスキーアート変換終了";

                long dif = (long)command.Value;

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["time"] = (Convert.ToDouble(dif) / 10000000.0d).ToString("0.00") + "秒";

                if (this.AfterAAConvert != null)
                {
                    this.AfterAAConvert(dic);
                }
            }

            if (command.CommandString == "処理終了")
            {
                this.fmain.AfterProcess();
            }

            if (command.CommandString == "１処理完了")
            {
                if (this.GoNextProjext != null)
                {
                    this.GoNextProjext(command.Value);
                }
            }
        }
        
        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            this.isBusy = true;

            Command[] commands = (Command[])e.Argument;

            List<Command> cmdList = new List<Command>(commands);

            SetConfig();

            if (cmdList[0].CommandString == "プロジェクト")
            {
                long start = DateTime.Now.Ticks;

                StartProject((Dictionary<string, string>)cmdList[0].Value);

                long dif = DateTime.Now.Ticks - start;

                this.bg.ReportProgress(0, new Command("終了", dif));
            }
            else
            {
                this.bg.ReportProgress(0, new Command("開始", 1));

                while (cmdList.Count > 0)
                {
                    Command command = cmdList[0];

                    if (command.CommandString == "細線化")
                    {
                        command.Value = ConvertLine();

                        if (cmdList.Count > 1 && cmdList[1].CommandString == "AA")
                        {
                            cmdList[1].Value = command.Value;
                        }
                    }
                    else if (command.CommandString == "AA")
                    {
                        command.Value = ConvertAA((Bitmap)command.Value, null);
                    }

                    this.bg.ReportProgress(0, cmdList[0]);

                    cmdList.RemoveAt(0);
                }

                this.bg.ReportProgress(0, new Command("終了"));
            }

            this.isBusy = false;

            this.bg.ReportProgress(0, new Command("処理終了"));
            
        }

        private void SetupForms()
        {
            Size s = this.fmain.ClientRealSize;

            s.Height -= 4;
            s.Width -= 4;

            fimage = new FrmImage(this);
            fimage.MdiParent = this.fmain;

            fline = new FrmLine(this);
            fline.MdiParent = this.fmain;

            fascii = new FrmAscii(this);
            fascii.MdiParent = this.fmain;

            finfo = new FrmInfo(this);
            finfo.MdiParent = this.fmain;

            fconfig = new FrmConfig(this);
            //fconfig.MdiParent = this.fmain;

            LineUp();

            fimage.Show();
            fline.Show();
            fascii.Show();
            finfo.Show();
            fconfig.Show(this.fmain);

        }

        public void LineUp()
        {
            Size s = this.fmain.ClientRealSize;

            s.Height -= 4;
            s.Width -= 4;

            fimage.Width = s.Width / 2;
            fimage.Height = s.Height / 2;
            fimage.Left = 0;
            fimage.Top = 0;
            fimage.WindowState = FormWindowState.Normal;

            fline.Width = s.Width / 2;
            fline.Height = s.Height / 2;
            fline.Left = s.Width / 2;
            fline.Top = 0;
            fline.WindowState = FormWindowState.Normal;

            fascii.Width = s.Width / 2;
            fascii.Height = s.Height / 2;
            fascii.Left = 0;
            fascii.Top = s.Height / 2;
            fascii.WindowState = FormWindowState.Normal;

            finfo.Width = s.Width / 2;
            finfo.Height = s.Height / 2;
            finfo.Left = s.Width / 2;
            finfo.Top = s.Height / 2;
            finfo.WindowState = FormWindowState.Normal;

            fconfig.WindowState = FormWindowState.Normal;



        }

        public void StartJob(Command[] commands)
        {
            this.bg_DoWork(null, new DoWorkEventArgs(commands));
        }

        private Image ConvertLine()
        {
            Image img = this.fimage.Image;

            int w = img.Width;
            int h = img.Height;

            if (this.cm.SizeType != 0)
            {
                w = this.cm.SizeImage.Width;
                h = this.cm.SizeImage.Height;
            }

            Image lapImage = null;

            using (Image newImage = ImageUtil.ZoomImage(img, new Size(w, h), false))
            {
                //newImage.Save("c:\\aaa.png", ImageFormat.Png);

                int ac = this.cm.Accuracy;
                int lr = this.cm.LapRange;
                int nl = this.cm.NoizeLen;
                int cr = this.cm.ConnectRange;

                using (Image tmpImage = ImageUtil.GetLineImage(newImage, ac, lr, nl, cr, cm))
                {
                    lapImage = ImageUtil.ZoomImage(tmpImage, new Size(w, h), true);
                }
            }

            return lapImage;

        }

        private string[] ConvertAA(Bitmap bmp,string[] aa)
        {
            this.bg.ReportProgress(0, new Command("AA開始"));

            if (this.charm == null || this.fconfig.IsChanged)
            {
                this.charm = new CharManager(this.cm);

                this.fconfig.IsChanged = false;
            }

            long start = DateTime.Now.Ticks;

            if (aa == null)
            {
                aa = AAUtil.Convert(bmp, cm, charm, this);
            }
           
            this.fascii.DrawAA(aa, bmp.Size, cm, charm);
            
            long dif = DateTime.Now.Ticks - start;

            this.bg.ReportProgress(0, new Command("AA終了", dif));

            return aa;
        }

        public void SetConfig()
        {
            Dictionary<string, string> dic = this.fconfig.Config;

            this.cm.SetConfig(dic);

            //この時点ではプロジェクトモードでは泣くAAモードにしておく
            this.cm.IsProject = false;
        }

        public bool CloseFlag
        {
            set
            {
                this.closeFlg = value;
            }
            get
            {
                return this.closeFlg;
            }
        }

        public void SetProgConvMax(int max)
        {
            this.fmain.ProgConvMax = max;
        }

        public void SetProgConvVal(int val)
        {
            this.fmain.ProgConvVal = val;
        }

        public void ShowImageFile(string fileName)
        {
            this.fimage.FileName = fileName;
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
        }

        public string FileName
        {
            get
            {
                return this.fimage.FileName;
            }
        }

        public void DoProject()
        {
            if (this.fcp == null)
            {
                this.fcp = new FrmConfigProject(this);
            }

            DialogResult result = this.fcp.ShowDialog(this.fmain);

            if (result != DialogResult.OK)
            {
                return;
            }

            //プロジェクト設定をコンフィグに設定する。
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic["proj_filename"] = this.fcp.FileName;
            dic["proj_mode"] = this.fcp.ProjectMode.ToString();
            dic["proj_line"] = this.fcp.SaveLine.ToString();
            dic["proj_bitrate"] = this.fcp.BitRate.ToString();
            dic["proj_skip"] = this.fcp.ConvertFrame.ToString();
            dic["proj_mk"] = this.fcp.MkMp4.ToString();
            dic["proj_fps"] = this.fcp.FPS;
            dic["proj_enc"] = this.fcp.AA_Encoding;
            dic["proj_orign_fps"] = this.fcp.OrignFPS.ToString();

            this.StartJob(new Command[] { new Command("プロジェクト", dic) });
        }
    }
}
