using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AsciiArtConverter08.Form.Dialog
{
    partial class FrmConfig
    {
        private int accuracy;
        private int charSet;
        private int connectRange;
        private string fontName;
        private float fontSize;
        private bool initFlg;
        private bool isChanged;
        private int lapRange;
        private int match;
        private int matchCnt;
        private bool multi;
        private int noizeLen;
        private int pitch;
        private bool reversal;
        private int score1;
        private int score2;
        private int sizeType;
        private bool tone;
        private string[] toneTxt;
        private int toneValue;
        private Size sizeImage;
        private Color textColor;
        private Color canvasColor;
        private int angle;
        private bool useNotDir;
        private int score3;
        private int score4;



        public Dictionary<string, string> Config
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["sizeType"] = Convert.ToString(this.sizeType);
                dic["sizeImage_w"] = Convert.ToString(this.sizeImage.Width);
                dic["sizeImage_h"] = Convert.ToString(this.sizeImage.Height);
                dic["accuracy"] = Convert.ToString(this.accuracy);
                dic["lapRange"] = Convert.ToString(this.lapRange);
                dic["noizeLen"] = Convert.ToString(this.noizeLen);
                dic["connectRange"] = Convert.ToString(this.connectRange);
                dic["fontName"] = this.fontName;
                dic["fontSize"] = Convert.ToString(this.fontSize);
                dic["pitch"] = Convert.ToString(this.pitch);
                dic["charSet"] = Convert.ToString(this.charSet);
                dic["match"] = Convert.ToString(this.match);
                dic["score1"] = Convert.ToString(this.score1);
                dic["score2"] = Convert.ToString(this.score2);
                dic["multi"] = Convert.ToString(this.multi);
                dic["matchCnt"] = Convert.ToString(this.matchCnt);
                dic["tone"] = Convert.ToString(this.tone);
                dic["reversal"] = Convert.ToString(this.reversal);
                dic["toneValue"] = Convert.ToString(this.toneValue);
                string tone = "";
                foreach (string t in this.toneTxt)
                {
                    if (tone != "")
                    {
                        tone = tone + "＠";
                    }
                    tone = tone + t;
                }
                dic["toneTxt"] = tone;

                dic["textColor"] = "" + this.textColor.R + "," + this.textColor.G + "," + this.textColor.B;
                dic["canvsColor"] = "" + this.canvasColor.R + "," + this.canvasColor.G + "," + this.canvasColor.B;

                dic["angle"] = Convert.ToString(this.angle);

                dic["useNotDir"] = Convert.ToString(this.useNotDir);
                dic["score3"] = Convert.ToString(this.score3);
                dic["score4"] = Convert.ToString(this.score4);

                return dic;
            }
        }

        private void SetConfig()
        {
            if (this.initFlg)
            {
                if (this.rdoImgeSize_0.Checked)
                {
                    this.sizeType = 0;
                }
                if (this.rdoImgeSize_1.Checked)
                {
                    this.sizeType = 1;
                    string[] s = this.cmbImgSize.Text.Split(new char[] { 'x' });
                    this.sizeImage.Width = Convert.ToInt32(s[0]);
                    this.sizeImage.Height = Convert.ToInt32(s[1]);
                }
                if (this.rdoImgeSize_2.Checked)
                {
                    this.sizeType = 2;
                    this.sizeImage.Width = Convert.ToInt32(this.numImgW.Value);
                    this.sizeImage.Height = Convert.ToInt32(this.numImgH.Value);
                }
                this.accuracy = this.trkAccuracy.Value;
                this.lblAccuracy.Text = Convert.ToString(this.accuracy);
                this.lapRange = ((this.cmbAccuracy.SelectedIndex + 1) * 2) + 1;
                this.noizeLen = Convert.ToInt32(this.numNoize.Value);
                this.connectRange = Convert.ToInt32(this.numConnectRange.Value);
                if ((this.cmbFont.SelectedIndex == 0) || (this.cmbFont.SelectedIndex == 1))
                {
                    this.fontName = "ＭＳ ゴシック";
                }
                else
                {
                    this.fontName = "ＭＳ ゴシック";
                }
                if ((this.cmbFont.SelectedIndex == 0) || (this.cmbFont.SelectedIndex == 2))
                {
                    this.fontSize = 9f;
                }
                else
                {
                    this.fontSize = 12f;
                }
                this.pitch = this.rdoPitch0.Checked ? 0 : 2;
                this.charSet = this.cmbCharSet.SelectedIndex;
                if (this.rdoMatch1.Checked)
                {
                    this.match = 1;
                }
                else if (this.rdoMatch2.Checked)
                {
                    this.match = 2;
                }
                else if (this.rdoMatch3.Checked)
                {
                    this.match = 3;
                }
                this.score1 = Convert.ToInt32(this.numScore1.Value);
                this.score2 = Convert.ToInt32(this.numScore2.Value);
                this.multi = this.chkMulti.Checked;
                this.matchCnt = this.cmbMatchCnt.SelectedIndex;
                this.tone = this.chkTone.Checked;
                this.reversal = this.chkR.Checked;
                this.toneValue = Convert.ToInt32(this.trackTone.Value);
                string[] tone = this.txtTone.Text.Split(new char[] { '＠' });
                List<string> lst = new List<string>();
                foreach (string t in tone)
                {
                    if (t != "")
                    {
                        lst.Add(t);
                    }
                }
                this.toneTxt = lst.ToArray();

                this.textColor = this.lblTxtColor.BackColor;

                this.canvasColor = this.lblCanvasColor.BackColor;

                this.lblToneValue.Text = "" + this.trackTone.Value + " 以下の輝度をトーンの対象にします。";

                this.angle = this.rdoAngle1.Checked ? 1 : 2;

                this.useNotDir = this.chkUseNotDir.Checked;
                this.score3 = Convert.ToInt32(this.numScore3.Value);
                this.score4 = Convert.ToInt32(this.numScore4.Value);
                
                this.isChanged = true;
            }
        }


        public void SaveConfig(string fileName)
        {
            Dictionary<string, string> dic = Config;

            string[] s = this.cmbImgSize.Text.Split(new char[] { 'x' });
            dic["selectImg_w"] = s[0];
            dic["selectImg_h"] = s[1];
            dic["sizeImage_w"] = Convert.ToString(this.numImgW.Value);
            dic["sizeImage_h"] = Convert.ToString(this.numImgH.Value);


            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("UTF-8")))
            {
                foreach (string key in dic.Keys)
                {
                    sw.Write(key + "=" + dic[key] + "\r\n");
                }
            }
        }

        public void OpenConfig(string fileName)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("UTF-8")))
                {
                    string s;

                    while ((s = sr.ReadLine()) != null)
                    {
                        int index = s.IndexOf('=');
                        string key = s.Substring(0, index);
                        string value = s.Substring(index + 1, s.Length - index - 1);
                        dic[key] = value;
                    }
                }

                this.initFlg = false;

                if (dic["sizeType"] == "0")
                {
                    this.rdoImgeSize_0.Checked = true;
                }
                if (dic["sizeType"] == "1")
                {
                    this.rdoImgeSize_1.Checked = true;
                }
                if (dic["sizeType"] == "2")
                {
                    this.rdoImgeSize_2.Checked = true;
                }

                if (dic["selectImg_w"].Trim() == "640" && dic["selectImg_h"].Trim() == "480")
                {
                    this.cmbImgSize.SelectedIndex = 0;
                }
                if (dic["selectImg_w"].Trim() == "800" && dic["selectImg_h"].Trim() == "600")
                {
                    this.cmbImgSize.SelectedIndex = 1;
                }
                if (dic["selectImg_w"].Trim() == "960" && dic["selectImg_h"].Trim() == "540")
                {
                    this.cmbImgSize.SelectedIndex = 2;
                }
                if (dic["selectImg_w"].Trim() == "1024" && dic["selectImg_h"].Trim() == "768")
                {
                    this.cmbImgSize.SelectedIndex = 3;
                }
                if (dic["selectImg_w"].Trim() == "1280" && dic["selectImg_h"].Trim() == "720")
                {
                    this.cmbImgSize.SelectedIndex = 4;
                }
                if (dic["selectImg_w"].Trim() == "1920" && dic["selectImg_h"].Trim() == "1080")
                {
                    this.cmbImgSize.SelectedIndex = 5;
                }
                
                this.numImgW.Value = Convert.ToInt32(dic["sizeImage_w"]);
                this.numImgH.Value = Convert.ToInt32(dic["sizeImage_h"]);

                this.trkAccuracy.Value = Convert.ToInt32(dic["accuracy"]);

                if (dic["lapRange"] == "3")
                {
                    this.cmbAccuracy.SelectedIndex = 0;
                }
                if (dic["lapRange"] == "5")
                {
                    this.cmbAccuracy.SelectedIndex = 1;
                }
                if (dic["lapRange"] == "7")
                {
                    this.cmbAccuracy.SelectedIndex = 2;
                }
                if (dic["lapRange"] == "9")
                {
                    this.cmbAccuracy.SelectedIndex = 3;
                }

                this.numNoize.Value = Convert.ToInt32(dic["noizeLen"]);

                this.numConnectRange.Value = Convert.ToInt32(dic["connectRange"]);

                if (dic["fontName"] == "ＭＳ Ｐゴシック" && dic["fontSize"] == "9")
                {
                    this.cmbFont.SelectedIndex = 0;
                }
                if (dic["fontName"] == "ＭＳ Ｐゴシック" && dic["fontSize"] == "12")
                {
                    this.cmbFont.SelectedIndex = 1;
                }
                if (dic["fontName"] == "ＭＳ ゴシック" && dic["fontSize"] == "9")
                {
                    this.cmbFont.SelectedIndex = 2;
                }
                if (dic["fontName"] == "ＭＳ ゴシック" && dic["fontSize"] == "12")
                {
                    this.cmbFont.SelectedIndex = 3;
                }

                if (dic["pitch"] == "0")
                {
                    this.rdoPitch0.Checked = true;
                }
                if (dic["pitch"] == "2")
                {
                    this.rdoPitch2.Checked = true;
                }

                this.cmbCharSet.SelectedIndex = Convert.ToInt32(dic["charSet"]);

                if (dic["match"] == "1")
                {
                    this.rdoMatch1.Checked = true;
                }
                if (dic["match"] == "2")
                {
                    this.rdoMatch2.Checked = true;
                }
                if (dic["match"] == "3")
                {
                    this.rdoMatch3.Checked = true;
                }

                this.numScore1.Value = Convert.ToInt32(dic["score1"]);
                this.numScore2.Value = Convert.ToInt32(dic["score2"]);

                if (dic["multi"].ToLower() == "true")
                {
                    this.chkMulti.Checked = true;
                }
                else
                {
                    this.chkMulti.Checked = false;
                }

                this.cmbMatchCnt.SelectedIndex = Convert.ToInt32(dic["matchCnt"]);

                this.chkTone.Checked = dic["tone"].ToLower() == "true" ? true : false;

                this.chkR.Checked = dic["reversal"].ToLower() == "true" ? true : false;

                this.trackTone.Value = Convert.ToInt32(dic["toneValue"]);

                this.txtTone.Text = dic["toneTxt"];

                string[] color = dic["textColor"].Split(',');
                lblTxtColor.BackColor = Color.FromArgb(Convert.ToInt32(color[0]), Convert.ToInt32(color[1]), Convert.ToInt32(color[2]));

                color = dic["canvsColor"].Split(',');
                lblCanvasColor.BackColor = Color.FromArgb(Convert.ToInt32(color[0]), Convert.ToInt32(color[1]), Convert.ToInt32(color[2]));

                this.rdoAngle1.Checked = dic["angle"] == "1";

                this.chkUseNotDir.Checked = dic["useNotDir"].ToLower() == "true";

                this.numScore3.Value = Convert.ToInt32(dic["score3"]);
                this.numScore4.Value = Convert.ToInt32(dic["score4"]);

            }
            catch
            {
                MessageBox.Show(this, "設定ファイルが壊れています。", "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.initFlg = true;

            SetConfig();

        }


        public bool IsChanged
        {
            get
            {
                return this.isChanged;
            }
            set
            {
                this.isChanged = false;
            }
        }
 



    }
}
