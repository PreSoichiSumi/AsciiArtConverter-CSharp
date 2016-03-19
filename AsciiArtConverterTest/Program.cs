using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AsciiArtConverter08.Exports;
using AsciiArtConverter08.Util;

namespace AsciiArtConverterTest
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            var image = new Bitmap("test08.png");
            int[] buffer = new int[image.Width * image.Height];
            var data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            var config = CreateConfigManager(image);
            fixed (int* p = buffer)
            {
                var result = AAConvUtilForJNA.getAA(p, image.Width, image.Height, new IntPtr(&config));
                var aa = Marshal.PtrToStringUni(result);
                using (var writer = new StreamWriter("a.txt"))
                {
                    writer.Write(aa);
                }
            }
        }

        static ConfigManagerStruct CreateConfigManager(Image i)
        { 
            ConfigManagerStruct config = new ConfigManagerStruct();
            config.accuracy = 50;
            config.angle = 2;
            config.canvsColor = Color.White.ToArgb();
            config.textColor = Color.Black.ToArgb();
            config.charSet = 2;
            config.connectRange = 1;
            config.noizeLen = 20;
            config.lapRange = 9;
            config.useNotDir = true;
            config.score1 = 85;
            config.score2 = 100;
            config.score3 = 80;
            config.score4 = 100;
            config.sizeType = 1;
            config.pitch = 0;
            config.match = 2;
            config.toneValue = 222;
            config.reversal = false;
            config.fontName = Marshal.StringToHGlobalUni("ＭＳ Ｐゴシック");
            config.toneTxt = Marshal.StringToHGlobalUni(":＠: ＠:  ＠. ");
            config.fontSize = 12;
            config.sizeImageH = i.Size.Height;
            config.sizeImageW = i.Size.Width;
            config.matchCnt = 2;
            return config;
        }
    }
}
