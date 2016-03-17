using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using AsciiArtConverter08.Manager;

namespace AsciiArtConverter08.Util
{
    class LaplacianUtil
    {
        private unsafe delegate int GetColorDel(int width, int height, int* ptr0, int y, int x, int i);

		/// <summary>
		/// ラプラシアンによる輪郭抽出
		/// </summary>
		/// <param name="img">対象イメージ</param>
		/// <param name="weight">輪郭抽出ウェイト</param>
		/// <returns>変換後のイメージ</returns>
		public static unsafe Image DoLaplacian(Image img, int weight,int lapRange,ConfigManager cm)
		{
			Bitmap bmp = new Bitmap(img);

			int width = bmp.Width;
			int height = bmp.Height;

			int[, ,] f = null;

			BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

			byte[] b = new byte[width * height * 4];
			byte[] resByte1 = new byte[width * height * 4];
			byte[] resByte2 = new byte[width * height * 4];

            //b = Enumerable.Repeat<byte>(0xFF, b.Length).ToArray();
            //resByte1 = Enumerable.Repeat<byte>(0xFF, b.Length).ToArray();
            //resByte2 = Enumerable.Repeat<byte>(0xFF, b.Length).ToArray();

			Marshal.Copy(data.Scan0, b, 0, b.Length);

			f = new int[4, width, height];

			fixed (int* ptr0 = &f[0, 0, 0])
			{
				fixed (byte* ptr1 = &resByte1[0])
				{
					fixed (byte* ptr3 = &resByte2[0])
					{
						//for (int i = resByte2.Length - 1; i >= 0; i--)
						//{
						//    ptr3[i] = 255;
						//}

						fixed (byte* ptr2 = &b[0])
						{
							for (int y = 0; y < height; y++)
							{
                                for (int x = 0; x < width; x++)
                                {
                                    ptr0[x * height + y] = (int)((decimal)ptr2[x * 4 + y * width * 4] * (decimal)0.114478
                                        + (decimal)ptr2[x * 4 + y * width * 4 + 1] * (decimal)0.586611
                                        + (decimal)ptr2[x * 4 + y * width * 4 + 2] * (decimal)0.298912);

                                    //ptr0[x * height + y + width * height * 1] = ptr2[x * 4 + y * width * 4];
                                    //ptr0[x * height + y + width * height * 2] = ptr2[x * 4 + y * width * 4 + 1];
                                    //ptr0[x * height + y + width * height * 3] = ptr2[x * 4 + y * width * 4 + 2];

                                    byte val;
                                    if (cm.Reversal)
                                    {
                                        val = (byte)(255 - ptr0[x * height + y]);
                                    }
                                    else
                                    {
                                        val = (byte)(ptr0[x * height + y]);
                                    }

                                    b[x * 4 + y * width * 4] = val;
                                    b[x * 4 + y * width * 4 + 1] = val;
                                    b[x * 4 + y * width * 4 + 2] = val;
                                    b[x * 4 + y * width * 4 + 3] = 255;

                                }
							}
						}

						int limit = 1;
						//GetColorDel del = null;

						if (lapRange == 3)
						{
							limit = 1;
							//del = new GetColorDel(GetColorLimit1);
						}else	if (lapRange == 5)
						{
							limit = 2;
							//del = new GetColorDel(GetColorLimit2);
                        }
                        else if (lapRange == 7)
                        {
                            limit = 3;
                            //del = new GetColorDel(GetColorLimit3);
                        }
                        else if (lapRange == 9)
                        {
                            limit = 4;
                            //del = new GetColorDel(GetColorLimit4);
                        }

						for (int y = 0; y < height; y++)
						{
							for (int x = 0; x < width; x++)
							{

                                if (y < limit || x < limit || y >= height - limit - 1 || x >= width - limit - 1)
                                {
                                    ptr1[x * 4 + y * width * 4 + 0] = 255;
                                    ptr1[x * 4 + y * width * 4 + 1] = 255;
                                    ptr1[x * 4 + y * width * 4 + 2] = 255;
                                    ptr1[x * 4 + y * width * 4 + 3] = 255;

                                    ptr3[x * 4 + y * width * 4 + 0] = 255;
                                    ptr3[x * 4 + y * width * 4 + 1] = 255;
                                    ptr3[x * 4 + y * width * 4 + 2] = 255;
                                    ptr3[x * 4 + y * width * 4 + 3] = 255;

                                    continue;
                                }

								int color = 0;

								//for (int i = 0; i < 1; i++)
								//{
								//int c = 0;

								//color = del(width, height, ptr0, x, y, 0);

                                color = GetColorLimit(width, height, ptr0, x, y, 0, limit);

								//if (color < c)
								//{
								//    color = c;
								//}
								//}

								//color = -color;

								//if (color > 255)
								//{
								//    color = 255;
								//}
								//if (color < 0)
								//{
								//    color = 0;
								//}

                                if (color > weight)
                                {
                                    color = 0;

                                    //if (!IsBrightToDark(width, height, ptr0, x, y, 0, limit))
                                    //{
                                    //    color = 255;
                                    //}
                                }
                                else
                                {
                                    color = 255;
                                }

								ptr1[x * 4 + y * width * 4 + 0] = (byte)color;
								ptr1[x * 4 + y * width * 4 + 1] = (byte)color;
								ptr1[x * 4 + y * width * 4 + 2] = (byte)color;
								ptr1[x * 4 + y * width * 4 + 3] = 255;

								ptr3[x * 4 + y * width * 4 + 0] = (byte)color;
								ptr3[x * 4 + y * width * 4 + 1] = (byte)color;
								ptr3[x * 4 + y * width * 4 + 2] = (byte)color;
								ptr3[x * 4 + y * width * 4 + 3] = 255;

							}
						}
					}
				}
			}

            Marshal.Copy(resByte2, 0, data.Scan0, width * height * 4);

            bmp.UnlockBits(data);

            //細線化
            ThinLineUtil.DoThinLine(bmp);

            if (cm.Tone && cm.ToneText.Length > 0)
            {
                //BitmapData imgdata = ((Bitmap)img).LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                //byte[] imgbyte = new byte[img.Width * img.Height * 4];

                //Marshal.Copy(imgdata.Scan0, imgbyte, 0, imgbyte.Length);
                
                data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                Marshal.Copy(data.Scan0, resByte2, 0, b.Length);

                int val = 200 / cm.ToneText.Length;

                for (int i = 0; i < b.Length; i += 4)
                {
                    if (resByte2[i] == 255)
                    {
                        int col = (int)(b[i] * (decimal)0.114478 + b[i + 1] * (decimal)0.58661 + b[i + 2] * (decimal)0.298912);

                        if (cm.ToneValue > col)
                        {
                            int t_range = cm.ToneValue / cm.ToneText.Length + 1;
                            col = (int)(col / t_range) * val + val;

                            if (col > 200)
                            {
                                col = 200;
                            }
                            else if (col < 0)
                            {
                                col = 0;
                            }

                            resByte2[i] = (byte)col;
                            resByte2[i + 1] = (byte)col;
                            resByte2[i + 2] = (byte)col;
                        }

                    }
                }

                //((Bitmap)img).UnlockBits(imgdata);

                Marshal.Copy(resByte2, 0, data.Scan0, width * height * 4);

                bmp.UnlockBits(data);
            }


            //using (Graphics g = Graphics.FromImage(bmp))
            //{
            //    //端の方に黒線が残ることがあるので…
            //    g.FillRectangle(Brushes.White, new Rectangle(width-lapRange, 0, lapRange, height));
            //    g.FillRectangle(Brushes.White, new Rectangle(0, height-lapRange, width, lapRange));
            //}

			//bmp.Save("C:\\test.png", ImageFormat.Png);



			return bmp;
		}

		unsafe private static void NoizeClear3(int noize, int width, int height, byte* ptr1, byte* ptr3)
		{
			for (int y = 1; y < height - 1; y++)
			{
				for (int x = 1; x < width - 1; x++)
				{
					if (ptr1[x * 4 + y * width * 4 + 0] == 0)
					{
						int i = ptr1[(x - 1) * 4 + (y - 1) * width * 4 + 0] + ptr1[(x - 0) * 4 + (y - 1) * width * 4 + 0] + ptr1[(x + 1) * 4 + (y - 1) * width * 4 + 0]
							  + ptr1[(x - 1) * 4 + (y - 0) * width * 4 + 0] + ptr1[(x - 0) * 4 + (y - 0) * width * 4 + 0] + ptr1[(x + 1) * 4 + (y - 0) * width * 4 + 0]
							  + ptr1[(x - 1) * 4 + (y + 1) * width * 4 + 0] + ptr1[(x - 0) * 4 + (y + 1) * width * 4 + 0] + ptr1[(x + 1) * 4 + (y + 1) * width * 4 + 0];

						if (i >= 255 * (9 - noize))
						{
							ptr3[x * 4 + y * width * 4 + 0] = 255;
							ptr3[x * 4 + y * width * 4 + 1] = 255;
							ptr3[x * 4 + y * width * 4 + 2] = 255;
						}
					}

				}
			}
		}

		unsafe private static void NoizeClear5(int noize, int width, int height, byte* ptr1, byte* ptr3)
		{
			for (int y = 2; y < height - 2; y++)
			{
				for (int x = 2; x < width - 2; x++)
				{
					if (ptr1[x * 4 + y * width * 4 + 0] == 0)
					{
						int i = ptr1[(x - 2) * 4 + (y - 2) * width * 4 + 0] + ptr1[(x - 1) * 4 + (y - 2) * width * 4 + 0] + ptr1[(x - 0) * 4 + (y - 2) * width * 4 + 0] + ptr1[(x + 1) * 4 + (y - 2) * width * 4 + 0] + ptr1[(x + 2) * 4 + (y - 2) * width * 4 + 0]
							  + ptr1[(x - 2) * 4 + (y - 1) * width * 4 + 0] + ptr1[(x - 1) * 4 + (y - 1) * width * 4 + 0] + ptr1[(x - 0) * 4 + (y - 1) * width * 4 + 0] + ptr1[(x + 1) * 4 + (y - 1) * width * 4 + 0] + ptr1[(x + 2) * 4 + (y - 1) * width * 4 + 0]
							  + ptr1[(x - 2) * 4 + (y - 0) * width * 4 + 0] + ptr1[(x - 1) * 4 + (y - 0) * width * 4 + 0] + ptr1[(x - 0) * 4 + (y - 0) * width * 4 + 0] + ptr1[(x + 1) * 4 + (y - 0) * width * 4 + 0] + ptr1[(x + 2) * 4 + (y - 0) * width * 4 + 0]
							  + ptr1[(x - 2) * 4 + (y + 1) * width * 4 + 0] + ptr1[(x - 1) * 4 + (y + 1) * width * 4 + 0] + ptr1[(x - 0) * 4 + (y + 1) * width * 4 + 0] + ptr1[(x + 1) * 4 + (y + 1) * width * 4 + 0] + ptr1[(x + 2) * 4 + (y + 1) * width * 4 + 0]
							  + ptr1[(x - 2) * 4 + (y + 2) * width * 4 + 0] + ptr1[(x - 1) * 4 + (y + 2) * width * 4 + 0] + ptr1[(x - 0) * 4 + (y + 2) * width * 4 + 0] + ptr1[(x + 1) * 4 + (y + 2) * width * 4 + 0] + ptr1[(x + 2) * 4 + (y + 2) * width * 4 + 0];


						if (i >= 255 * (25 - noize))
						{
							ptr3[x * 4 + y * width * 4 + 0] = 255;
							ptr3[x * 4 + y * width * 4 + 1] = 255;
							ptr3[x * 4 + y * width * 4 + 2] = 255;
						}
					}

				}
			}
		}

        unsafe private static int GetColorLimit(int width, int height, int* ptr0, int x, int y, int i,int range)
        {
            return ptr0[(x - range) * height + y - range + (width * height * i)] +
                ptr0[x * height + y - range + (width * height * i)] +
                ptr0[(x + range) * height + y - range + (width * height * i)] +
                ptr0[(x - range) * height + y + (width * height * i)] - 8 *
                ptr0[x * height + y + (width * height * i)] +
                ptr0[(x + range) * height + y + (width * height * i)] +
                ptr0[(x - range) * height + y + range + (width * height * i)] +
                ptr0[x * height + y + range + (width * height * i)] +
                ptr0[(x + range) * height + y + range + (width * height * i)];
        }

        //unsafe private static int GetColorLimit1(int width, int height, int* ptr0, int x, int y, int i)
        //{
        //    return ptr0[(x - 1) * height + y - 1 + (width * height * i)] +
        //        ptr0[x * height + y - 1 + (width * height * i)] +
        //        ptr0[(x + 1) * height + y - 1 + (width * height * i)] +
        //        ptr0[(x - 1) * height + y + (width * height * i)] - 8 *
        //        ptr0[x * height + y + (width * height * i)] +
        //        ptr0[(x + 1) * height + y + (width * height * i)] +
        //        ptr0[(x - 1) * height + y + 1 + (width * height * i)] +
        //        ptr0[x * height + y + 1 + (width * height * i)] +
        //        ptr0[(x + 1) * height + y + 1 + (width * height * i)];
        //}

        //unsafe private static int GetColorLimit2(int width, int height, int* ptr0, int x, int y, int i)
        //{
        //    return ptr0[(x - 2) * height + y - 2 + (width * height * i)] +
        //        ptr0[x * height + y - 2 + (width * height * i)] +
        //        ptr0[(x + 2) * height + y - 2 + (width * height * i)] +
        //        ptr0[(x - 2) * height + y + (width * height * i)] - 8 *
        //        ptr0[x * height + y + (width * height * i)] +
        //        ptr0[(x + 2) * height + y + (width * height * i)] +
        //        ptr0[(x - 2) * height + y + 2 + (width * height * i)] +
        //        ptr0[x * height + y + 2 + (width * height * i)] +
        //        ptr0[(x + 2) * height + y + 2 + (width * height * i)];
        //}

        //unsafe private static int GetColorLimit3(int width, int height, int* ptr0, int x, int y, int i)
        //{
        //    return ptr0[(x - 3) * height + y - 3 + (width * height * i)] +
        //        ptr0[x * height + y - 3 + (width * height * i)] +
        //        ptr0[(x + 3) * height + y - 3 + (width * height * i)] +
        //        ptr0[(x - 3) * height + y + (width * height * i)] - 8 *
        //        ptr0[x * height + y + (width * height * i)] +
        //        ptr0[(x + 3) * height + y + (width * height * i)] +
        //        ptr0[(x - 3) * height + y + 3 + (width * height * i)] +
        //        ptr0[x * height + y + 3 + (width * height * i)] +
        //        ptr0[(x + 3) * height + y + 3 + (width * height * i)];
        //}

        //unsafe private static int GetColorLimit4(int width, int height, int* ptr0, int x, int y, int i)
        //{
        //    return ptr0[(x - 4) * height + y - 4 + (width * height * i)] +
        //        ptr0[x * height + y - 4 + (width * height * i)] +
        //        ptr0[(x + 4) * height + y -4 + (width * height * i)] +
        //        ptr0[(x - 4) * height + y + (width * height * i)] - 8 *
        //        ptr0[x * height + y + (width * height * i)] +
        //        ptr0[(x + 4) * height + y + (width * height * i)] +
        //        ptr0[(x - 4) * height + y + 4 + (width * height * i)] +
        //        ptr0[x * height + y + 4 + (width * height * i)] +
        //        ptr0[(x + 4) * height + y + 4 + (width * height * i)];
        //}

        unsafe private static bool IsBrightToDark(int width, int height, int* ptr0, int x, int y, int i, int range)
        {
            return (ptr0[(x - range) * height + y - range + (width * height * i)] +
                ptr0[x * height + y - range + (width * height * i)] +
                ptr0[(x + range) * height + y - range + (width * height * i)] +
                ptr0[(x - range) * height + y + (width * height * i)])
                >=
                (ptr0[(x + range) * height + y + (width * height * i)] +
                ptr0[(x - range) * height + y + range + (width * height * i)] +
                ptr0[x * height + y + range + (width * height * i)] +
                ptr0[(x + range) * height + y + range + (width * height * i)]);
        }
	}
}
