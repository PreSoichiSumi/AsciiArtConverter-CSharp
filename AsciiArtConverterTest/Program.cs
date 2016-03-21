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
using AsciiArtConverter08.Manager;

namespace AsciiArtConverterTest
{
    class Program
    {
        unsafe static void Main(String[] args)
        {
            /*Image image = Image.FromFile("test08.png");
            ConfigManager cm = AAUtilForDebug.initCM(new ConfigManager());
            
            AAUtilForDebug.setAAZoom(cm, 6);
            CharManager charm = new CharManager(cm);
            Image newimg= AAUtilForDebug.ConvertLine(image,cm);
            String[] aa = AAUtilForDebug.Convert((Bitmap)newimg, cm, charm);
            using (var writer = new StreamWriter("a.txt"))
            {
                writer.Write(aa[0]);
            }*/
            Bitmap image = new Bitmap("test08.png");
            int[] buffer = new int[image.Width * image.Height];
            ConfigManagerStruct config = CreateConfigManager(image);
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            
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

        static ConfigManagerStruct CreateConfigManager(Bitmap i)
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
            config.fontName = Marshal.StringToHGlobalUni("ＭＳ ゴシック");
            config.toneTxt = Marshal.StringToHGlobalUni(":＠: ＠:  ＠. ");
            config.fontSize = 12;
            config.sizeImageH = i.Size.Height;
            config.sizeImageW = i.Size.Width;
            setAAZoom(ref config, 5);
            config.matchCnt = 2;
            return config;
        }
        public static ConfigManager initCM(ConfigManager cm)
        {
            cm.accuracy = 50;
            cm.angle = 2;
            cm.canvsColor = Color.White;
            cm.charSet = 5;
            cm.config = null;
            cm.connectRange = 1;
            cm.font = new Font("ＭＳ ゴシック", 12, FontStyle.Regular);
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

        public static void setAAZoom(ref ConfigManagerStruct cm, int sizeType)
        {
            if (!(0 <= sizeType && sizeType <= 6))
                return;
            cm.sizeType = sizeType;
            switch (sizeType)
            {
                case 0:
                    break;
                case 1:
                    cm.sizeImageW = 640;
                    cm.sizeImageH = 480;
                    break;
                case 2:
                    cm.sizeImageW = 800;
                    cm.sizeImageH = 600;
                    break;
                case 3:
                    cm.sizeImageW = 960;
                    cm.sizeImageH = 540;
                    break;
                case 4:
                    cm.sizeImageW = 1024;
                    cm.sizeImageH = 768;
                    break;
                case 5:
                    cm.sizeImageW = 1280;
                    cm.sizeImageH = 720;
                    break;
                case 6:
                    cm.sizeImageW = 1920;
                    cm.sizeImageH = 1080;
                    break;
                default:
                    break;

            }
        }
    }
}
