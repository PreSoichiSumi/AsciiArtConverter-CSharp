using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AsciiArtConverter08.Manager;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using AsciiArtConverter08.Patturn;
using System.Diagnostics;
using System.Threading;
using AsciiArtConverter08.Util.Helper;
using System.Windows.Forms;

namespace AsciiArtConverter08.Util
{
    //AAUtil.Convertのデバッグ用クラス
    public class AAUtilForDebug
    {
        
        public static String[] Convert(Bitmap bmp, ConfigManager cm, CharManager charm)
        {
            char[,] table = GetTable(bmp);  //画像取得
            char[,] toneTable = GetToneTable(bmp, cm);  //トーン

            int h = charm[0].Height;
            int w = table.GetLength(0);

            List<char[,]> lstSplitTable = new List<char[,]>();
            List<char[,]> lstSplitToneTable = new List<char[,]>();

            for (int top = 0; top < table.GetLength(1); top += h)
            {
                lstSplitTable.Add(TrimTable(table, top, 0, h, w, 0));

                if (cm.Tone && cm.ToneText.Length > 0)
                {
                    lstSplitToneTable.Add(TrimTable(toneTable, top, 0, h, w, 0));
                }
                else
                {
                    lstSplitToneTable.Add(new char[0, 0]);
                }
            }

            char[][,] splitTable = lstSplitTable.ToArray();

            StringBuilder sbAA = new StringBuilder();
            StringBuilder sbAAType = new StringBuilder();

            int prog = 0;

            int multi = 1;

            if (cm.IsMulti)
            {
                AsciiArtConverter08.Win32.Win32Api.SYSTEM_INFO sysInfo = new AsciiArtConverter08.Win32.Win32Api.SYSTEM_INFO();
                AsciiArtConverter08.Win32.Win32Api.GetSystemInfo(ref sysInfo);

                multi = (int)sysInfo.dwNumberOfProcessors;
            }

            for (int i = 0; i < splitTable.Length; i+=multi)
            {
                if (i + multi >= splitTable.Length)
                {
                    multi = splitTable.Length - i;
                }

                AAConvThread[] at = new AAConvThread[multi];
                Thread[] t = new Thread[multi];

                for (int j = 0; j < at.Length; j++)
                {
                    at[j] = new AAConvThread(cm, charm, splitTable[i + j], lstSplitToneTable[i + j]);
                    t[j] = new Thread(at[j].DoThread);
                    t[j].Start();
                }

                while (true)
                {
                    Application.DoEvents();
                    Thread.Sleep(100);

                    for (int j = 0; j < at.Length; j++)
                    {
                        if (at[j].AA == "")
                        {
                            goto LOOP;
                        }
                    }

                    break;

                LOOP:

                        ;

                }

                prog+=at.Length;


                for (int j = 0; j < at.Length; j++)
                {
                    sbAA.Append(at[j].AA + "\r\n");
                    sbAAType.Append(at[j].AA_Type + "\r\n");
                }
            }

            //Debug.WriteLine(sb.ToString());

            return new string[] { sbAA.ToString(), sbAAType.ToString() };
        }
        public static ConfigManager initCM(ConfigManager cm)
        {
            cm.accuracy= 50;
            cm.angle= 2;
            cm.canvsColor = Color.White;
            cm.charSet = 2;
            cm.config = null;
            cm.connectRange = 1;
            cm.font = new Font("ＭＳ　ゴシック", 12);
            cm.isProject = false;
            cm.lapRange = 9;
            cm.match = 2;
            cm.matchCnt = 2;
            cm.multi = true;
            cm.noizeLen = 20;
            cm.pitch = 0;
            cm.projLine = false;
            cm.reversal = false;
            cm.score1 = 85;
            cm.score2 = 100;
            cm.score3 = 80;
            cm.score4 = 100;
            cm.sizeImage = new Size(0, 0);
            cm.sizeType = 0;
            cm.textColor = Color.Black;
            cm.tone = false;
            cm.toneTxt = new String[] { "::", ":", ".", "" };
            cm.toneValue = 222;
            cm.useNotDir = true;
            return cm;
        }

        public static void DebugTable(char[,] c)
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < c.GetLength(1); y++)
            {
                for (int x = 0; x < c.GetLength(0); x++)
                {
                    sb.Append(c[x, y]);
                }

                sb.Append("\r\n");
            }

            Debug.WriteLine(sb.ToString());
        }

        public static char[,] TrimTable(char[,] table, int top, int left, int height, int width, int margin)
        {
            //上下左右に1ピクセルの余白が必要なので+2
            char[,] c = new char[width + margin * 2, height + margin * 2];

            for (int y = 0; y < c.GetLength(1); y++)
            {
                for (int x = 0; x < c.GetLength(0); x++)
                {
                    if (y < margin || y > c.GetLength(1) - margin - 1 || x < margin || x > c.GetLength(0) - margin - 1 || y - margin + top >= table.GetLength(1) || x - margin + left >= table.GetLength(0))
                    {
                        c[x, y] = '□';
                        continue;
                    }

                    c[x, y] = table[x - margin + left, y - margin + top];
                }
            }

            return c;

        }
        
        private static char[,] GetTable(Bitmap bmp)
        {
            int nx = bmp.Width;
            int ny = bmp.Height;

            char[,] table = new char[nx, ny];

            for (int y = 0; y < table.GetLength(1); y++)
            {
                for (int x = 0; x < table.GetLength(0); x++)
                {
                    table[x, y] = '□';
                }
            }

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, nx, ny), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] b = new byte[nx * ny * 4];

            Marshal.Copy(data.Scan0, b, 0, b.Length);

            for (int y = 0; y < ny; y++)
            {
                for (int x = 0; x < nx; x++)
                {
                    if (b[x * 4 + y * nx * 4] == 0)
                    {
                        table[x, y] = '■';
                    }
                }
            }

            bmp.UnlockBits(data);

            return table;
        }

        private static char[,] GetToneTable(Bitmap bmp,ConfigManager cm)
        {
            int nx = bmp.Width;
            int ny = bmp.Height;

            char[,] table = new char[nx, ny];

            for (int y = 0; y < table.GetLength(1); y++)
            {
                for (int x = 0; x < table.GetLength(0); x++)
                {
                    table[x, y] = ' ';
                }
            }

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, nx, ny), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] b = new byte[nx * ny * 4];

            Marshal.Copy(data.Scan0, b, 0, b.Length);

            int val = 200 / cm.ToneText.Length;

            for (int y = 0; y < ny; y++)
            {
                for (int x = 0; x < nx; x++)
                {
                    if (b[x * 4 + y * nx * 4] == 0 || b[x * 4 + y * nx * 4] == 255)
                    {
                        table[x, y] = ' ';
                    }
                    else
                    {
                        table[x, y] = (b[x * 4 + y * nx * 4] / val - 1).ToString()[0];
                    }
                }
            }

            bmp.UnlockBits(data);

            return table;
        }
    }
}
