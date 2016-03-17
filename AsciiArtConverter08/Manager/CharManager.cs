using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsciiArtConverter08.Manager.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace AsciiArtConverter08.Manager
{
    public class CharManager
    {
        private List<CharData> lstData = new List<CharData>();
        private Dictionary<char, CharData> dicData = new Dictionary<char, CharData>();
        private int count = 0;

        public CharManager(ConfigManager cm)
        {
            Font font = cm.Font;
            int pitch = cm.Pitch;
            
            string chardata = "";

            HashSet<char> hash = new HashSet<char>();

            string charSetName = "";

            if (cm.CharSet < 5)
            {
                charSetName = "\\基本設定\\charset_" + (cm.CharSet + 1) + ".txt";
            }
            else
            {
                charSetName = "\\user_def_" + (cm.CharSet - 4) + ".txt";
            }

            using (StreamReader sr = new StreamReader(Application.StartupPath + "\\charset" + charSetName, Encoding.GetEncoding("UTF-8")))
            {
                chardata = sr.ReadLine();

                if (chardata.Trim().StartsWith("//"))
                {
                    chardata = sr.ReadLine();
                }
            }

            //半角空白は必須
            if (chardata.IndexOf(' ') == -1)
            {
                chardata += " ";
            }

            //全角空白は必須
            if (chardata.IndexOf('　') == -1)
            {
                chardata += "　";
            }

            //ピリオドは必須
            if (chardata.IndexOf('.') == -1)
            {
                chardata += ".";
            }

            //全角｜と半角|は必須
            if (chardata.IndexOf('｜') == -1)
            {
                chardata += "｜";
            }
            if (chardata.IndexOf('|') == -1)
            {
                chardata += "|";
            }

            for (int i = 0; i < chardata.Length; i++)
            {
                hash.Add(chardata[i]);
            }

            //トーン文字を追加
            if (cm.Tone)
            {
                foreach (string t in cm.ToneText)
                {
                    foreach (char c in t)
                    {
                        hash.Add(c);
                    }
                }
            }

            foreach (char c in hash)
            {
                lstData.Add(new CharData(c, font, pitch));
            }

            for (int i = 0; i < lstData.Count; i++)
            {
                dicData[lstData[i].Character] = lstData[i];
            }

            lstData.Sort(new Sorter());

            //全角空白はトップ
            CharData cd = dicData['　'];
            lstData.Remove(cd);
            lstData.Insert(0, cd);


            this.count = lstData.Count;

            //for (int i = 0; i < chardata.Length; i++)
            //{
            //    Debug.WriteLine(this.lstData[i].ToString());
            //}
        }

        public CharData this[int index]{

            get
            {
                return this.lstData[index];
            }
        }

        public CharData this[char c]
        {

            get
            {
                if (this.dicData.ContainsKey(c))
                {
                    return this.dicData[c];
                }
                else
                {
                    return null;
                }
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

    }

    public class Sorter : IComparer<CharData>
    {
        public int Compare(CharData x, CharData y)
        {
            return x.Patturn.GetLength(0).CompareTo(y.Patturn.GetLength(0));
        }
    }
}
