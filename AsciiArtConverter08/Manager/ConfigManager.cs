﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AsciiArtConverter08.Exports;
using System.Runtime.InteropServices;

namespace AsciiArtConverter08.Manager
{
    public class ConfigManager
    {
        
        public int sizeType = 0;
        public Size sizeImage = new Size();

        public int accuracy = 0;
        public int lapRange = 0;
        public int noizeLen = 0;
        public int connectRange = 0;

        public int pitch = 0;
        public int charSet = 0;
        public int match = 0;
        public Font font = null;
        
        public int score1 = 0;
        public int score2 = 0;
        public bool multi = false;

        public int matchCnt = 0;

        public bool tone = false;
        public bool reversal = false;
        public int toneValue = 0;
        public string[] toneTxt = null;

        public Color textColor;
        public Color canvsColor;

        public int angle = 0;

        public bool useNotDir = false;
        public int score3 = 0;
        public int score4 = 0;

        public string projFileName = "";
        public string projMode = "";
        public bool projLine = false;
        public bool projMkMp4 = false;
        public int projBitrate = 0;
        public int projSkip = 0;
        public string projFps = "";
        public string projEnc = "";
        public decimal projOrignFPS = 0.0m;

        public bool isProject = false;

        public Dictionary<string, string> config = null;

        public ConfigManager() { }

        public unsafe ConfigManager(ConfigManagerStruct data)
        {
            this.sizeType = data.sizeType;

            this.sizeImage.Width = data.sizeImageW;
            this.sizeImage.Height = data.sizeImageH;

            this.accuracy = data.accuracy;
            this.lapRange = data.lapRange;
            this.noizeLen = data.noizeLen;
            this.connectRange = data.connectRange;
            
            this.font = new Font(Marshal.PtrToStringUni(data.fontName), data.fontSize);

            this.pitch = data.pitch;
            this.match = data.match;
            
            this.score1 = data.score1;
            this.score2 = data.score2;

            this.multi = data.multi;

            this.matchCnt = data.matchCnt;

            this.charSet = data.charSet;

            this.tone = data.tone;
            this.reversal = data.reversal;
            this.toneValue = data.toneValue;
            
            this.toneTxt = Marshal.PtrToStringUni(data.toneTxt).Split('＠');

            this.textColor = Color.FromArgb(data.textColor);
            this.canvsColor = Color.FromArgb(data.canvsColor);

            this.angle = data.angle;

            this.useNotDir = data.useNotDir;

            this.score3 = data.score3;
            this.score4 = data.score4;
        }

        public void SetProjectConfig(Dictionary<string, string> dic)
        {
            foreach (string key in dic.Keys)
            {
                this.config[key] = dic[key];
            }

            this.projFileName = dic["proj_filename"];

            this.projMode = dic["proj_mode"];

            this.projLine = dic["proj_line"].ToLower() == "true";

            this.projMkMp4 = dic["proj_mk"].ToLower() == "true";

            this.projBitrate = Convert.ToInt32(dic["proj_bitrate"]);

            this.projSkip = Convert.ToInt32(dic["proj_skip"]);

            this.projFps = dic["proj_fps"];

            this.projEnc = dic["proj_enc"];

            this.projOrignFPS = Convert.ToDecimal(dic["proj_orign_fps"]);
        }

        public void SetConfig(Dictionary<string, string> dic)
        {
            this.sizeType = Convert.ToInt32(dic["sizeType"]);

            this.sizeImage.Width = Convert.ToInt32(dic["sizeImage_w"]);
            this.sizeImage.Height = Convert.ToInt32(dic["sizeImage_h"]);

            this.accuracy = Convert.ToInt32(dic["accuracy"]);
            this.lapRange = Convert.ToInt32(dic["lapRange"]);
            this.noizeLen = Convert.ToInt32(dic["noizeLen"]);
            this.connectRange = Convert.ToInt32(dic["connectRange"]);

            if (this.font != null)
            {
                this.font.Dispose();
            }
            this.font = new Font(dic["fontName"], Convert.ToSingle(dic["fontSize"]));
            this.pitch = Convert.ToInt32(dic["pitch"]);
            this.match = Convert.ToInt32(dic["match"]);

            this.score1 = Convert.ToInt32(dic["score1"]);
            this.score2 = Convert.ToInt32(dic["score2"]);

            this.multi = "true" == dic["multi"].ToLower();

            this.matchCnt = Convert.ToInt32(dic["matchCnt"]);

            this.charSet = Convert.ToInt32(dic["charSet"]);

            this.tone = "true" == dic["tone"].ToLower();
            this.reversal = "true" == dic["reversal"].ToLower();
            this.toneValue = Convert.ToInt32(dic["toneValue"]);
            this.toneTxt = dic["toneTxt"].Split('＠');

            string[] color = dic["textColor"].Split(',');
            this.textColor = Color.FromArgb(Convert.ToInt32(color[0]), Convert.ToInt32(color[1]), Convert.ToInt32(color[2]));
            color = dic["canvsColor"].Split(',');
            this.canvsColor = Color.FromArgb(Convert.ToInt32(color[0]), Convert.ToInt32(color[1]), Convert.ToInt32(color[2]));

            this.angle = Convert.ToInt32(dic["angle"]);

            this.useNotDir = dic["useNotDir"].ToLower() == "true";

            this.score3 = Convert.ToInt32(dic["score3"]);
            this.score4 = Convert.ToInt32(dic["score4"]);

            this.config = new Dictionary<string, string>(dic);

        }

        public Dictionary<string, string> ConfigDic
        {
            get
            {
                return this.config;
            }
        }

        public Size SizeImage
        {
            get
            {
                return this.sizeImage;
            }
        }

        public int SizeType
        {
            get
            {
                return this.sizeType;
            }
        }

        public int Accuracy
        {
            get
            {
                return this.accuracy;
            }
        }

        public int LapRange
        {
            get
            {
                return this.lapRange;
            }
        }

        public int NoizeLen
        {
            get
            {
                return this.noizeLen;
            }
        }

        public int ConnectRange
        {
            get
            {
                return this.connectRange;
            }
        }

        public Font Font
        {
            get
            {
                return this.font;
            }
        }

        public int Pitch
        {
            get
            {
                return this.pitch;
            }
        }

        public int Match
        {
            get
            {
                return this.match;
            }
        }

        public int Score1
        {
            get
            {
                return this.score1;
            }
        }

        public int Score2
        {
            get
            {
                return this.score2;
            }
        }

        public int Score3
        {
            get
            {
                return this.score3;
            }
        }

        public int Score4
        {
            get
            {
                return this.score4;
            }
        }

        public bool IsMulti
        {
            get
            {
                return this.multi;
            }
        }

        public int MatchCount
        {
            get
            {
                return this.matchCnt;
            }
        }

        public int CharSet
        {
            get
            {
                return this.charSet;
            }
        }

        public bool Tone
        {
            get
            {
                return this.tone;
            }
        }

        public bool Reversal
        {
            get
            {
                return this.reversal;
            }
        }

        public int ToneValue
        {
            get
            {
                return this.toneValue;
            }
        }

        public string[] ToneText
        {
            get
            {
                return this.toneTxt;
            }
        }

        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
        }

        public Color CanvasColor
        {
            get
            {
                return this.canvsColor;
            }
        }

        public int Angle
        {
            get
            {
                return this.angle;
            }
        }

        public bool UseNotDir
        {
            get
            {
                return this.useNotDir;
            }
        }

        public bool IsProject
        {
            get
            {
                return this.isProject;
            }
            set
            {
                this.isProject = value;

                this.config["isProject"] = value.ToString();
            }
        }

        public string ProjectFileName
        {
            get
            {
                return this.projFileName;
            }
        }

        public string ProjectMode
        {
            get
            {
                return this.projMode;
            }
        }

        public bool ProjectLine
        {
            get
            {
                return this.projLine;
            }
        }

        public bool ProjectMkMp4
        {
            get
            {
                return this.projMkMp4;
            }
        }

        public int ProjectBitrate
        {
            get
            {
                return this.projBitrate;
            }
        }

        public int ProjectSkip
        {
            get
            {
                return this.projSkip;
            }
        }

        public string ProjectEnc
        {
            get
            {
                return this.projEnc;
            }
        }

        public decimal ProjectOrignFps
        {
            get
            {
                return this.projOrignFPS;
            }
        }
    }
}
