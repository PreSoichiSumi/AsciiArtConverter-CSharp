using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace AsciiArtConverter08.Form.Dialog
{
    public partial class FrmMkMp4 : System.Windows.Forms.Form
    {
        private string dirName = "";
        private decimal fps = 0;
        private int convertFrame = 0;
        private int bitRate = 0;
        private string format = "";

        private Process process = null;

        private delegate void WriteMessageDel(string s);

        private StringBuilder msgsb = new StringBuilder();

        private EventHandler idleDel = null;

        private bool doWork = true;

        private bool iskilledProcess = false;

        private string fileName = "";

        private string workDir = "";
        
        public FrmMkMp4(string dirName,decimal fps,int convertFrame,int bitRate,string format)
        {
            InitializeComponent();

            this.dirName = dirName;

            this.fps = fps;

            this.convertFrame = convertFrame;

            this.bitRate = bitRate;

            this.format = format.ToLower();

            this.idleDel = new EventHandler(Application_Idle);

            Application.Idle += this.idleDel;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            Application.Idle-=this.idleDel;

            for (int i = 0; i < 100; i++)
            {
                Application.DoEvents();
            }

            StartMkMp4();

            this.doWork = false;

            this.Close();


            //throw new NotImplementedException();
        }

        private void WriteMessage(string s)
        {
            if (InvokeRequired)
            {
                msgsb.Append(s + "\r\n");

                WriteMessageDel msgdel = new WriteMessageDel(WriteMessage);

                if (!this.IsDisposed)
                {
                    this.Invoke(msgdel, new object[] { s });
                }

                return;
            }

            this.lblMessage.Text = s;
        }

        void process_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                WriteMessage(e.Data);
            }
        }

        private void StartMkMp4()
        {
            try
            {

                Application.DoEvents();

                this.workDir = DateTime.Now.ToString("yyyyMMddHHmmss");

                InitAndCopy2Work();

                lbl1.ForeColor = Color.Black;
                lbl2.ForeColor = Color.Blue;

                if (!this.iskilledProcess)
                {
                    MkAvi();
                }

                lbl2.ForeColor = Color.Black;
                lbl3.ForeColor = Color.Blue;

                if (!this.iskilledProcess)
                {
                    AddMusic();
                }

                lbl3.ForeColor = Color.Black;
                lbl4.ForeColor = Color.Blue;

                if (!this.iskilledProcess)
                {
                    Application.DoEvents();
                    Move2Project();
                }

                lbl4.ForeColor = Color.Black;
                lbl5.ForeColor = Color.Blue;

                this.prgJob.MarqueeAnimationSpeed = 0;

                if (!this.iskilledProcess)
                {
                    MessageBox.Show(this, "アスキーアート変換処理が終了しました。\r\nファイルは「" + this.fileName + "」です。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    System.Diagnostics.Process.Start("EXPLORER.EXE", @"/select," + this.fileName);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "アスキーアート変換処理に失敗しました。" + e.StackTrace, "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\work\\MkAvi.log", true))
                {
                    sw.WriteLine("");
                    sw.WriteLine("***********************************************************************************");
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sw.WriteLine("***********************************************************************************");
                    sw.WriteLine("");
                    sw.WriteLine(msgsb.ToString());
                    sw.WriteLine("");
                }
            }
        }

        private void Move2Project()
        {
            string from = Application.StartupPath + "\\work\\" + this.workDir + "\\aa_music." + this.format;

            string to = new DirectoryInfo(dirName).Parent.Parent.Parent.FullName + "\\mp4";

            this.fileName = to + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + this.format;

            File.Copy(from, fileName);

            //一時フォルダの削除
            Directory.Delete(Application.StartupPath + "\\work\\" + this.workDir, true);
        }

        private void AddMusic()
        {
            if (!File.Exists(new DirectoryInfo(dirName).Parent.Parent.Parent.FullName + "\\music\\music.mp3")
                || new FileInfo(new DirectoryInfo(dirName).Parent.Parent.Parent.FullName + "\\music\\music.mp3").Length == 0)
            {
                File.Copy(Application.StartupPath + "\\work\\" + this.workDir + "\\aa." + this.format, Application.StartupPath + "\\work\\" + this.workDir + "\\aa_music." + this.format);
                return;
            }

            Process process = new Process();
            process.StartInfo.FileName = Application.StartupPath + "\\ffmpeg\\ffmpeg.exe";

            string arg = "";

            arg += " -i \"" + Application.StartupPath + "\\work\\" + this.workDir + "\\aa." + this.format + "\"";
            arg += " -i \"" + new DirectoryInfo(dirName).Parent.Parent.Parent.FullName + "\\music\\music.mp3\"";
            arg += " -vcodec copy";
            arg += " -acodec copy";
            arg += " \"" + Application.StartupPath + "\\work\\" + this.workDir + "\\aa_music." + this.format + "\"";

            Debug.WriteLine(arg);

            process.StartInfo.Arguments = arg;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += new DataReceivedEventHandler(process_DataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(process_DataReceived);

            this.process = process;

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();


            while (!process.HasExited)
            {
                Thread.Sleep(1);
                Application.DoEvents();
            }

            process.Close();

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        private void MkAvi()
        {
            Process process = new Process();
            process.StartInfo.FileName = Application.StartupPath + "\\ffmpeg\\ffmpeg.exe";

            string arg = "";

            decimal fps = Convert.ToDecimal(this.fps) / Convert.ToDecimal(this.convertFrame);

            fps = Math.Round(fps, 2);
            
            arg = " -r " + fps + " -i ";

            arg += "\"" + Application.StartupPath + "\\work\\" + this.workDir + "\\%d.png\"";

            if (this.bitRate != 0)
            {
                arg += " -vcodec mpeg4 -b " + this.bitRate + "k -bt " + this.bitRate * 10 + "k \"" + Application.StartupPath + "\\work\\" + this.workDir + "\\aa." + this.format + "\"";
            }
            else
            {
                arg += " -vcodec mpeg4 -q:v 0 \"" + Application.StartupPath + "\\work\\" + this.workDir + "\\aa." + this.format + "\"";
            }

            //arg += " -sameq \"" + Application.StartupPath + "\\work\\aa." + this.format + "\"";

            Debug.WriteLine(arg);

            this.msgsb.Append(arg + "\r\n");

            process.StartInfo.Arguments = arg;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += new DataReceivedEventHandler(process_DataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(process_DataReceived);

            this.process = process;

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();


            while (!process.HasExited)
            {
                Thread.Sleep(1);
                Application.DoEvents();
            }

            process.Close();

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        private void InitAndCopy2Work()
        {
            //ワークフォルダ作成
            if (!Directory.Exists(Application.StartupPath + "\\work"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\work");
            }
            Directory.CreateDirectory(Application.StartupPath + "\\work\\" + this.workDir);


            //ファイルのコピー

            FileInfo[] fileInfos = new DirectoryInfo(this.dirName).GetFiles("*.png");

            Array.Sort(fileInfos, new Sorter());

            int index = 1;
            for (int i = 0; i < fileInfos.Length; i ++)
            {
                if ((Convert.ToInt32(fileInfos[i].Name.Replace(".png", "")) - 1) % this.convertFrame != 0)
                {
                    continue;
                }

                File.Copy(fileInfos[i].FullName, Application.StartupPath + "\\work\\"+ this.workDir +"\\" +index + ".png");

                Application.DoEvents();

                index++;

                if (this.iskilledProcess)
                {
                    break;
                }
            }

        }

        private void FrmMkAvi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (doWork)
            {
                if (MessageBox.Show(this, "処理を強制終了しますか？", "メッセージ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (this.process != null)
            {
                try
                {
                    process.Kill();
                }
                catch (Exception)
                {
                }
            }

            iskilledProcess = true;
        }

        private class Sorter : IComparer<FileInfo>
        {
            public int Compare(FileInfo x, FileInfo y)
            {
                string fileX = x.Name.Replace(".png", "");
                string fileY = y.Name.Replace(".png", "");

                int indexX = Convert.ToInt32(fileX);
                int indexY = Convert.ToInt32(fileY);

                return indexX.CompareTo(indexY);
            }
        }
    }
}
