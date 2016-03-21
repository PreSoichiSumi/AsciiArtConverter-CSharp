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
using AsciiArtConverter08.Exports;
using AsciiArtConverter08.Util;

namespace AsciiArtConverter08.Exports
{

    public class AAConvUtilForJNA
    {
        [RGiesecke.DllExport.DllExport]
        public unsafe static IntPtr getAA(int* buf,int bufW,int bufH, IntPtr cmsPtr/*, IntPtr res*/)//JNAのPointerはint[]では受け取れない．オブジェクト的なやつだからね
        {   //AA変換結果であるresだけはjavaが利用し終わるまでGCしてはならない
            
            ConfigManagerStruct cms = *(ConfigManagerStruct*)(cmsPtr.ToPointer());
            
            ConfigManager cm = new ConfigManager(cms);
            CharManager charm = new CharManager(cm);    //ここまでok



            Bitmap bmp;
            
            unsafe
            {
               bmp = new Bitmap(bufW, bufH, bufW*4,PixelFormat.Format32bppArgb, new IntPtr(buf)); //buf
            }
            bmp= (Bitmap)ConvertLine(bmp, cm);
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
           // String test = "auioe";
            ///if (true) return Marshal.StringToCoTaskMemUni(test);
            for (int i = 0; i < splitTable.Length; i+=multi)
            {
                if (i + multi >= splitTable.Length)
                {
                    multi = splitTable.Length - i;
                }
                AAConvThread[] at = new AAConvThread[multi];

                for (int j = 0; j < at.Length; j++)
                {
                    at[j] = new AAConvThread(cm, charm, splitTable[i + j], lstSplitToneTable[i + j]);
                    at[j].DoThread();
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
            //char[] aa = sbAA.ToString().ToCharArray();//copy?
            //IntPtr aaPtr = Marshal.AllocCoTaskMem(aa.Length * sizeof(char));    //TODO AllocHGlobalとの違い
            //Marshal.Copy(aa, 0, aaPtr, aa.Length);  //copy
            //GC.KeepAlive(aaPtr);
            //Console.WriteLine("4");
            return Marshal.StringToCoTaskMemUni(sbAA.ToString()); //aaPtr;
        }
        [RGiesecke.DllExport.DllExport]
        public static void freeAA(IntPtr aa)
        {
            Marshal.FreeCoTaskMem(aa);
        }

        public static Image ConvertLine(Image i, ConfigManager cm)
        {
            int w = i.Width;
            int h = i.Height;

            if (cm.SizeType != 0)
            {
                w = cm.SizeImage.Width;
                h = cm.SizeImage.Height;
            }

            Image lapImage = null;

            using (Image newImage = ImageUtil.ZoomImage(i, new Size(w, h), false))
            {
                //newImage.Save("c:\\aaa.png", ImageFormat.Png);

                int ac = cm.Accuracy;
                int lr = cm.LapRange;
                int nl = cm.NoizeLen;
                int cr = cm.ConnectRange;

                using (Image tmpImage = ImageUtil.GetLineImage(newImage, ac, lr, nl, cr, cm))
                {
                    lapImage = ImageUtil.ZoomImage(tmpImage, new Size(w, h), true);
                }
            }

            return lapImage;

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
