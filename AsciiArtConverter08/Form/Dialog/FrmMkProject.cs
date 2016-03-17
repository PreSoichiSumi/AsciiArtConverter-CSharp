using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AsciiArtConverter08.Form.Dialog
{
    public partial class FrmMkProject : System.Windows.Forms.Form
    {
        private bool doWork = false;

        private delegate void WriteMessageDel(string s);

        private decimal fps = 0;
        private decimal tbr = 0;
        private decimal nextFrameCount = 0;

        private string dirName = "";

        private Process process = null;

        private bool iskilledProcess = false;

        private StringBuilder msgsb = new StringBuilder();
        
        public FrmMkProject()
        {
            InitializeComponent();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (openDlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            this.txtMovieFileName.Text = openDlg.FileName;

            this.txtMovieFileName.SelectionStart = this.txtMovieFileName.Text.Length;
            this.txtMovieFileName.SelectionLength = 0;

            btnStart.Enabled = txtMovieFileName.Text != "" && txtProjectDirName.Text != "";
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            if (folderDlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            this.txtProjectDirName.Text = folderDlg.SelectedPath;

            this.txtProjectDirName.SelectionStart = this.txtProjectDirName.Text.Length;
            this.txtProjectDirName.SelectionLength = 0;

            btnStart.Enabled = txtMovieFileName.Text != "" && txtProjectDirName.Text != "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMkProject_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {

                this.prgJob.MarqueeAnimationSpeed = 10;

                this.doWork = true;

                //btnCancel.Enabled = false;

                btnStart.Enabled = false;

                btnFile.Enabled = false;

                btnDir.Enabled = false;

                MkDirectory();

                if (!this.iskilledProcess)
                {
                    GetMovieInfo();
                }

                if (!this.iskilledProcess)
                {
                    GetImageFile();
                }

                if (!this.iskilledProcess)
                {
                    GetMusic();
                }

                if (!this.iskilledProcess)
                {
                    MkProjectFile();
                }

                this.prgJob.MarqueeAnimationSpeed = 0;

                if (!this.iskilledProcess)
                {
                    CompleteWork();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "たまに失敗します。何回やっても失敗する場合はFFMPEGとの相性が悪い可能性があります。"
                    +"\r\nその場合は別バージョンのFFMPEGを使ってみてください。"
                    + "\r\n（このツールと動画プロジェクトのフォルダ名をすべて英数字のみにしたら上手くいったという報告もありました）\r\n\r\n" + ex.StackTrace, "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.doWork = false;
            }

            this.Close();

        }

        private void MkProjectFile()
        {
            using (StreamWriter sw = new StreamWriter(this.dirName + "\\project.aa2"))
            {
                sw.WriteLine("" + this.fps + " fps");
                sw.WriteLine("" + this.tbr + " tbr");
                sw.WriteLine("" + this.nextFrameCount + " nextFrameCount");
            }
        }

        private void CompleteWork()
        {
            this.lbl4.ForeColor = Color.Black;
            this.lbl5.ForeColor = Color.Blue;

            this.lblMessage.Text = "";

            Application.DoEvents();

            MessageBox.Show(this, "プロジェクト作成は正常に終了しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GetMusic()
        {
            this.lbl3.ForeColor = Color.Black;
            this.lbl4.ForeColor = Color.Blue;

            this.lblMessage.Text = "";

            Application.DoEvents();

            Process process = new Process();
            process.StartInfo.FileName = Application.StartupPath + "\\ffmpeg\\ffmpeg.exe";
            process.StartInfo.Arguments = "-i " + "\"" + txtMovieFileName.Text + "\"" + " -ab 128 " + "\"" + dirName + "\\music\\music.mp3" + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += new DataReceivedEventHandler(process_DataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(process_DataReceived);

            this.process = process;

            Thread.Sleep(500);

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

        private void GetImageFile()
        {
            this.lbl2.ForeColor = Color.Black;
            this.lbl3.ForeColor = Color.Blue;

            this.lblMessage.Text = "";

            Application.DoEvents();

            Process process = new Process();
            process.StartInfo.FileName = Application.StartupPath + "\\ffmpeg\\ffmpeg.exe";
            process.StartInfo.Arguments = "-i " + "\"" + txtMovieFileName.Text + "\"" + " -f image2 -r " + this.fps +" \"" + dirName + "\\image\\%d.png" + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += new DataReceivedEventHandler(process_DataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(process_DataReceived);

            this.process = process;

            Thread.Sleep(500);

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

        private void MkDirectory()
        {
            string dirName = "";
            string fileName = new FileInfo(this.txtMovieFileName.Text).Name;

            fileName = fileName.Substring(0,fileName.LastIndexOf('.'));

            if (!Directory.Exists(this.txtProjectDirName.Text + "\\" + fileName))
            {
                Directory.CreateDirectory(this.txtProjectDirName.Text + "\\" + fileName);
            }

            while (true)
            {
                dirName = this.txtProjectDirName.Text + "\\" + fileName + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss");

                if (!Directory.Exists(dirName))
                {
                    break;
                }

                Thread.Sleep(1000);
            }

            this.dirName = dirName;

            Directory.CreateDirectory(dirName);

            Directory.CreateDirectory(dirName + "\\image");

            Directory.CreateDirectory(dirName + "\\music");

            Directory.CreateDirectory(dirName + "\\mp4");
            
        }

        private void GetMovieInfo()
        {
            this.lbl1.ForeColor = Color.Black;
            this.lbl2.ForeColor = Color.Blue;

            this.lblMessage.Text = "";

            Application.DoEvents();

            Process process = new Process();
            process.StartInfo.FileName = Application.StartupPath + "\\ffmpeg\\ffmpeg.exe";
            process.StartInfo.Arguments = "-i " + "\"" + txtMovieFileName.Text + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += new DataReceivedEventHandler(process_DataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(process_DataReceived);

            this.msgsb.Clear();

            this.process = process;

            Thread.Sleep(500);

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

            Regex r = new Regex(@"[\d\.\d]* fps", RegexOptions.IgnoreCase);

            Match m = r.Match(msgsb.ToString());
            
            while (m.Success)
            {
                //一致した対象が見つかったときキャプチャした部分文字列を表示
                Console.WriteLine(m.Value);

                fps = Convert.ToDecimal(m.Value.Replace(" fps", ""));

                //次に一致する対象を検索
                m = m.NextMatch();


            }

            r = new Regex(@"[\d\.\d]* tbr", RegexOptions.IgnoreCase);

            m = r.Match(msgsb.ToString());

            while (m.Success)
            {
                //一致した対象が見つかったときキャプチャした部分文字列を表示
                Console.WriteLine(m.Value);

                tbr = Convert.ToDecimal(m.Value.Replace(" tbr", ""));

                //次に一致する対象を検索
                m = m.NextMatch();
            }

            if (fps == 0)
            {
                fps = tbr;
            }

            if (tbr == 0)
            {
                nextFrameCount = 1;
            }
            else
            {
                nextFrameCount = tbr / fps;
            }

            if (fps == 0)
            {
                MessageBox.Show(this, "プロジェクトの作成に失敗しました。\r\nFFMPEGのバージョンによっては日本語又は特定の文字（「～」等）がファイル名又はフォルダ名に使用できない場合あります。\r\n動画のファイル名及びプロジェクトのフォルダ名から日本語又は全角文字等を除いてください。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.iskilledProcess = true;
                return;
            }

            lblFps.Text = "" + fps + "fps," + tbr + "tbr";

        }

        private void WriteMessage(string s)
        {
            if (InvokeRequired)
            {
                msgsb.Append(s + "\r\n");

                WriteMessageDel msgdel = new WriteMessageDel(WriteMessage);

                this.Invoke(msgdel, new object[] { s });

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

        
    }
}
