using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AsciiArtConverter08.Form.Dialog;
using AsciiArtConverter08.Util;
using System.Security.Cryptography;

namespace AsciiArtConverter08.Manager
{
    public partial class MainManager
    {
        private bool isCansel = false;

        private void StartProject(Dictionary<string, string> config)
        {
            this.cm.IsProject = true;
            this.cm.SetProjectConfig(config);

            string projectFilename = this.cm.ProjectFileName;
            string baseDir = new FileInfo(projectFilename).Directory.FullName;

            string[] files = Directory.GetFiles(baseDir + "\\image", "*.png");

            Array.Sort(files, new Sorter());

            int step = this.cm.ProjectSkip;

            baseDir = SetUpDir();

            Command command = new Command("開始", files.Length);
            this.bg.ReportProgress(0, command);


            this.isCansel = false;
                        
            string base64Md5 = "";
            bool isEqualHash = false;
            string backAA = "";

            MD5CryptoServiceProvider md5 =
                    new System.Security.Cryptography.MD5CryptoServiceProvider();
                        
            for (int i = 0; i < files.Length; i += step)
            {
                Application.DoEvents();

                if (isCansel)
                {
                    break;
                }

                isEqualHash = false;

                string filename = files[i];

                if (File.Exists(baseDir + "\\ext_image\\" + (i + 1) + ".png"))
                {
                    filename = baseDir + "\\ext_image\\" + (i + 1) + ".png";
                }

                //スキップ
                if (this.cm.ProjectMode == ProjectMode.画像があればスキップ.ToString())
                {
                    if (File.Exists(baseDir + "\\text_image\\" + (i + 1) + ".png"))
                    {
                        if ((i / step) % 30 == 0)
                        {
                            this.fimage.FileName = filename;
                        }
                        //１処理完了
                        command = new Command("１処理完了", i + 1);
                        this.bg.ReportProgress(0, command);
                        continue;
                    }
                }

                //画像セット
                this.fimage.FileName = filename;

                using (FileStream fs = new System.IO.FileStream(
                                                    filename,
                                                    System.IO.FileMode.Open,
                                                    System.IO.FileAccess.Read,
                                                    System.IO.FileShare.Read))
                {
                    string s = Convert.ToBase64String(md5.ComputeHash(fs));
                    if (base64Md5 == s)
                    {
                        isEqualHash = true;
                    }
                    else
                    {
                        base64Md5 = s;
                    }
                }


                //細線化
                command = new Command("細線化");
                                
                command.Value = ConvertLine();
                this.bg.ReportProgress(0, command);

                if (this.cm.ProjectLine)
                {
                    ((Bitmap)command.Value).Save(baseDir + "\\line_image\\" + (i + 1) + ".png", ImageFormat.Png);
                }

                //AA変換
                command = new Command("AA", command.Value);
                string[] aa = null;

                if (this.cm.ProjectMode == ProjectMode.テキストファイルを利用.ToString())
                {
                    if (File.Exists(baseDir + "\\text\\" + this.cm.ProjectEnc + "\\" + (i + 1) + ".txt"))
                    {
                        using (StreamReader sr = new StreamReader(baseDir + "\\text\\" + this.cm.ProjectEnc + "\\" + (i + 1) + ".txt", Encoding.GetEncoding(this.cm.ProjectEnc)))
                        {
                            aa = new string[2];
                            aa[0] = sr.ReadToEnd();
                            aa[1] = "";
                        }
                    }
                }
                else if (isEqualHash)
                {
                    aa = new string[2];
                    aa[0] = backAA;
                    aa[1] = "";
                }

                command.Value = ConvertAA((Bitmap)command.Value, aa);

                this.bg.ReportProgress(0, command);
                ((Bitmap)this.fascii.Image).Save(baseDir + "\\text_image\\" + (i + 1) + ".png", ImageFormat.Png);
                using (StreamWriter sw = new StreamWriter(baseDir + "\\text\\" + this.cm.ProjectEnc + "\\" + (i + 1) + ".txt", false, Encoding.GetEncoding(this.cm.ProjectEnc)))
                {
                    sw.Write(((string[])command.Value)[0]);
                }

                backAA = ((string[])command.Value)[0];

                //１処理完了
                command = new Command("１処理完了", i + 1);
                this.bg.ReportProgress(0, command);


            }

            if (isCansel)
            {
                return;
            }

            //動画の作成
            if (this.cm.ProjectMkMp4)
            {
                FrmMkMp4 fm = new FrmMkMp4(baseDir + "\\text_image", this.cm.ProjectOrignFps, this.cm.ProjectSkip, this.cm.ProjectBitrate, "MP4");
                fm.ShowDialog(this.fmain);
            }
        }

        private string SetUpDir()
        {
            string filename = this.cm.ProjectFileName;

            string baseDir = new FileInfo(filename).Directory.FullName + "\\AsciiArt";

            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            if (this.cm.SizeType == 0)
            {
                if (!Directory.Exists(baseDir + "\\no_zoom"))
                {
                    Directory.CreateDirectory(baseDir + "\\no_zoom");
                }
                baseDir = baseDir + "\\no_zoom";
            }
            else
            {
                if (!Directory.Exists(baseDir + "\\" + this.cm.SizeImage.Width + "x" + this.cm.SizeImage.Height))
                {
                    Directory.CreateDirectory(baseDir + "\\" + this.cm.SizeImage.Width + "x" + this.cm.SizeImage.Height);
                }
                baseDir = baseDir + "\\" + this.cm.SizeImage.Width + "x" + this.cm.SizeImage.Height;
            }

            if (!Directory.Exists(baseDir + "\\ext_image"))
            {
                Directory.CreateDirectory(baseDir + "\\ext_image");
            }

            if (!Directory.Exists(baseDir + "\\line_image"))
            {
                Directory.CreateDirectory(baseDir + "\\line_image");
            }

            if (!Directory.Exists(baseDir + "\\text"))
            {
                Directory.CreateDirectory(baseDir + "\\text");
            }

            if (!Directory.Exists(baseDir + "\\text\\Shift_JIS"))
            {
                Directory.CreateDirectory(baseDir + "\\text\\Shift_JIS");
            }

            if (!Directory.Exists(baseDir + "\\text\\UTF-8"))
            {
                Directory.CreateDirectory(baseDir + "\\text\\UTF-8");
            }

            if (!Directory.Exists(baseDir + "\\text_image"))
            {
                Directory.CreateDirectory(baseDir + "\\text_image");
            }

            return baseDir;
        }

        public bool Cansel
        {
            set
            {
                this.isCansel = value;
            }
        }


        private class Sorter : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                string fileX = new FileInfo(x).Name.Replace(".png", "");
                string fileY = new FileInfo(y).Name.Replace(".png", "");

                int indexX = Convert.ToInt32(fileX);
                int indexY = Convert.ToInt32(fileY);

                return indexX.CompareTo(indexY);

            }
        }

        public void SaveConfig(string filename)
        {
            this.cm.SetConfig(this.fconfig.Config);

            string baseDir = new FileInfo(filename).Directory.FullName + "\\AsciiArt";

            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            if (this.cm.SizeType == 0)
            {
                if (!Directory.Exists(baseDir + "\\no_zoom"))
                {
                    Directory.CreateDirectory(baseDir + "\\no_zoom");
                }
                baseDir = baseDir + "\\no_zoom";
            }
            else
            {
                if (!Directory.Exists(baseDir + "\\" + this.cm.SizeImage.Width + "x" + this.cm.SizeImage.Height))
                {
                    Directory.CreateDirectory(baseDir + "\\" + this.cm.SizeImage.Width + "x" + this.cm.SizeImage.Height);
                }
                baseDir = baseDir + "\\" + this.cm.SizeImage.Width + "x" + this.cm.SizeImage.Height;
            }

            this.fconfig.SaveConfig(baseDir + "\\config.ini");

            string infoAA = InfoUtil.GetInfoAA(this.cm.ConfigDic);

            using (StreamWriter sw = new StreamWriter(baseDir + "\\info.txt"))
            {
                sw.Write(infoAA);
            }
        }

        public DialogResult ChkConfig(string filename)
        {

            this.cm.SetConfig(this.fconfig.Config);

            string baseDir = new FileInfo(filename).Directory.FullName + "\\AsciiArt";

            if (this.cm.SizeType == 0)
            {
                baseDir = baseDir + "\\no_zoom";
            }
            else
            {
                baseDir = baseDir + "\\" + this.cm.SizeImage.Width + "x" + this.cm.SizeImage.Height;
            }

            if (!File.Exists(baseDir + "\\info.txt"))
            {
                return DialogResult.OK;
            }

            string infoAA = InfoUtil.GetInfoAA(this.cm.ConfigDic);

            string oldInfoAA = "";

            using (StreamReader sr = new StreamReader(baseDir + "\\info.txt"))
            {
                oldInfoAA = sr.ReadToEnd();
            }

            if (infoAA == oldInfoAA)
            {
                return DialogResult.OK;
            }

            using (FrmChkAAInfo f = new FrmChkAAInfo(oldInfoAA, infoAA))
            {
                f.ShowDialog(this.fmain);

                if (f.IsConfigOpen)
                {
                    this.fconfig.OpenConfig(baseDir + "\\config.ini");
                }

                if (f.IsOK)
                {
                    return DialogResult.OK;
                }
            }

            return DialogResult.Cancel;
        }
    }
    
}
