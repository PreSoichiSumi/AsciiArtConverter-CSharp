using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtConverter08.Exports
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ConfigManagerStruct
    {
        public int sizeType;    //0..変更なし，1以上..sizeImageのW,Hにする
        public int sizeImageW;
        public int sizeImageH;

        public int accuracy;
        public int lapRange;    //輪郭抽出の比較範囲
        public int noizeLen;
        public int connectRange;

        public int pitch;
        public int charSet;
        public int match;

        //public string fontName; //changed
        public IntPtr fontName;
        public float fontSize;    //changed

        public int score1;
        public int score2;
        public bool multi;

        public int matchCnt;

        public bool tone;
        public bool reversal;
        public int toneValue;
        //public string[] toneTxt;
        public IntPtr toneTxt;
        public int textColor; //changed from ConfigManager
        public int canvsColor; //chagnged

        public int angle;

        public bool useNotDir;
        public int score3;
        public int score4;

        public bool isProject;
    }
}
