using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using AsciiArtConverter08.Patturn;
using System.Runtime.InteropServices;
using AsciiArtConverter08.Manager;

namespace AsciiArtConverter08.Util
{
    public class ImageUtil
    {
        public static Image ZoomImage(Image img, Size size, bool fraction)
        {
            
            double mx = 0;
            double my = 0;
            double m = 0;

            mx = (double)size.Width / (double)img.Width;
            my = (double)size.Height / (double)img.Height;

            m = mx < my ? mx : my;

            Size newSize = new Size((int)(img.Width * m), (int)(img.Height * m));

            Image newImg;

            if (fraction)
            {
                newImg = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            }
            else
            {
                newImg = new Bitmap((int)(img.Width * m), (int)(img.Height * m), PixelFormat.Format32bppArgb);
            }

            size = newImg.Size;

            using (Graphics g = Graphics.FromImage(newImg))
            {
                int left = (size.Width - newSize.Width) / 2;
                int top = (size.Height - newSize.Height) / 2;

                g.Clear(Color.White);
                g.DrawImage(img, new Rectangle(left, top, newSize.Width, newSize.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            }

            return newImg;
        }

        public static Image GetLineImage(Image img, int weight, int lapRange,int noizeLen,int connectRange,ConfigManager cm)
        {
            Image lineImage = LaplacianUtil.DoLaplacian(img, weight, lapRange, cm);

            //ThinLineUtil.DoThinLine((Bitmap)lineImage);

            //lineImage.Save("c:\\aa.png", ImageFormat.Png);

            if (noizeLen > 0)
            {
                RemoveNoize(lineImage, noizeLen, connectRange);
            }

            return lineImage;
        }


        private static void RemoveNoize(Image img, int noizeLen, int connectRange)
        {
            int width = img.Width;
            int height = img.Height;

            BitmapData data = ((Bitmap)img).LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            byte[] b = new byte[width * height * 4];

            Marshal.Copy(data.Scan0, b, 0, b.Length);

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (b[(x) * 4 + (y) * width * 4] == 0)
                    {
                        int val = 0;

                        for (int y1 = -1; y1 < 2; y1++)
                        {
                            for (int x1 = -1; x1 < 2; x1++)
                            {
                                val += b[(x + x1) * 4 + (y + y1) * width * 4];
                            }
                        }

                        if (val == 255 * 8)
                        {
                            b[x * 4 + y * width * 4] = 255;
                            b[x * 4 + y * width * 4 + 1] = 255;
                            b[x * 4 + y * width * 4 + 2] = 255;
                            b[x * 4 + y * width * 4 + 3] = 255;
                        }
                        else if (val <= 255 * 7 && noizeLen > 1)
                        {
                            HashSet<Point> h = new HashSet<Point>();

                            GetConnectPoint(new Point(x, y), h, b, width, height, noizeLen, connectRange);

                            if (h.Count <= noizeLen)
                            {
                                foreach (Point p in h)
                                {
                                    b[p.X * 4 + p.Y * width * 4] = 255;
                                    b[p.X * 4 + p.Y * width * 4 + 1] = 255;
                                    b[p.X * 4 + p.Y * width * 4 + 2] = 255;
                                    b[p.X * 4 + p.Y * width * 4 + 3] = 255;
                                }
                            }
                        }

                    }
                }
            }

            Marshal.Copy(b, 0, data.Scan0, width * height * 4);

            ((Bitmap)img).UnlockBits(data);
        }

        private static void GetConnectPoint(Point p, HashSet<Point> h, byte[] b, int width, int height, int noizeLen,int connectRange)
        {
            if (h.Count > noizeLen || h.Contains(p))
            {
                return;
            }

            h.Add(p);

            for (int y = -connectRange; y < connectRange+1; y++)
            {
                for (int x = -connectRange; x < connectRange + 1; x++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    if (p.X + x >= 0 && p.X + x < width && p.Y + y >= 0 && p.Y + y < height && b[(p.X + x) * 4 + (p.Y + y) * width * 4] == 0)
                    {
                        GetConnectPoint(new Point(p.X + x, p.Y + y), h, b, width, height, noizeLen, connectRange);
                    }
                }
            }

        }
       
    }
}
