using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AsciiArtConverter08.Util
{
    class ThinLineUtil
    {
        public static void DoThinLine(Bitmap lapBmp)//受け取ったデータをコピー
		{
			int nx = lapBmp.Width;
			int ny = lapBmp.Height;
			int[,] f = new int[nx, ny];

			BitmapData data = lapBmp.LockBits(new Rectangle(0, 0, nx, ny), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

			byte[] b = new byte[nx * ny * 4];

			Marshal.Copy(data.Scan0, b, 0, b.Length);

            //byte[] back = (byte[])b.Clone();

			for (int y = 0; y < ny; y++)
			{
				for (int x = 0; x < nx; x++)
				{
					if (b[x * 4 + y * nx * 4] == 255)
					{
						f[x, y] = 0;
					}
					else
					{
						f[x, y] = 1;
					}
				}
			}

			int kaisuu = thinning4(nx, ny, f);

			for (int y = 0; y < ny; y++)
			{
				for (int x = 0; x < nx; x++)
				{
					if (f[x, y] == 0)
					{
						b[x * 4 + y * nx * 4] = 255;
						b[x * 4 + y * nx * 4 + 1] = 255;
						b[x * 4 + y * nx * 4 + 2] = 255;
					}
					else
					{
						b[x * 4 + y * nx * 4] = 0;
						b[x * 4 + y * nx * 4 + 1] = 0;
						b[x * 4 + y * nx * 4 + 2] = 0;
					}
				}
			}

            for (int y = 0; y < ny; y++)
            {
                for (int x = 0; x < nx; x++)
                {
                    if (y + 1 >= ny || x + 1 >= nx || y-1<0 || x-1<0)
                    {
                        continue;
                    }

                    if (f[x,y]!=0 && f[x+1,y]!=0 && f[x,y+1]!=0)
                    {
                        b[x * 4 + y * nx * 4] = 255;
                        b[x * 4 + y * nx * 4 + 1] = 255;
                        b[x * 4 + y * nx * 4 + 2] = 255;
                        f[x, y] = 0;
                    }
                    else if (f[x, y] != 0 && f[x - 1, y] != 0 && f[x, y - 1] != 0)
                    {
                        b[x * 4 + y * nx * 4] = 255;
                        b[x * 4 + y * nx * 4 + 1] = 255;
                        b[x * 4 + y * nx * 4 + 2] = 255;
                        f[x, y] = 0;
                    }
                    else if (f[x, y] != 0 && f[x - 1, y] != 0 && f[x - 1, y - 1] != 0)
                    {
                        b[(x - 1) * 4 + y * nx * 4] = 255;
                        b[(x - 1) * 4 + y * nx * 4 + 1] = 255;
                        b[(x - 1) * 4 + y * nx * 4 + 2] = 255;
                        f[(x - 1), y] = 0;
                    }
                    else if (f[x, y] != 0 && f[x + 1, y] != 0 && f[x + 1, y + 1] != 0)
                    {
                        b[(x + 1) * 4 + y * nx * 4] = 255;
                        b[(x + 1) * 4 + y * nx * 4 + 1] = 255;
                        b[(x + 1) * 4 + y * nx * 4 + 2] = 255;
                        f[(x + 1), y] = 0;
                    }
                }
            }

            for (int y = 0; y < ny-5; y++)
            {
                int cnt = 0;
                for (int x = 0; x < nx-5; x++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (f[x + i, y] != 0)
                        {
                            cnt++;
                        }

                        if (f[x + i, y + 4] != 0)
                        {
                            cnt++;
                        }

                        if (f[x, y + i] != 0)
                        {
                            cnt++;
                        }

                        if (f[x+4, y + i] != 0)
                        {
                            cnt++;
                        }
                    }

                    if (cnt == 0)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            for (int j = 1; j < 3; j++)
                            {
                                b[(x + i) * 4 + (y + j) * nx * 4] = 255;
                                b[(x + i) * 4 + (y + j) * nx * 4 + 1] = 255;
                                b[(x + i) * 4 + (y + j) * nx * 4 + 2] = 255;
                            }
                        }
                    }

                }
            }

            //for (int y = 1; y < ny - 1; y++)
            //{
            //    for (int x = 1; x < nx - 1; x++)
            //    {
            //        if (b[x * 4 + y * nx * 4] == 255 && back[x * 4 + y * nx * 4] == 0)
            //        {
            //            int cnt = b[(x - 1) * 4 + (y) * nx * 4] + b[(x) * 4 + (y - 1) * nx * 4] + b[(x) * 4 + (y + 1) * nx * 4] + b[(x + 1) * 4 + (y) * nx * 4];
            //            if (cnt <= 255 * 2)
            //            {
            //                b[x * 4 + y * nx * 4] = back[x * 4 + y * nx * 4];
            //                b[x * 4 + y * nx * 4 + 1] = back[x * 4 + y * nx * 4 + 1];
            //                b[x * 4 + y * nx * 4 + 2] = back[x * 4 + y * nx * 4 + 2];
            //                b[x * 4 + y * nx * 4 + 3] = back[x * 4 + y * nx * 4 + 3];
            //            }
            //        }
            //    }
            //}

			Marshal.Copy(b, 0, data.Scan0, b.Length);
			lapBmp.UnlockBits(data);

		}

		//4-連結細線化処理ﾙｰﾁﾝ
		private static int thinning4(int nx, int ny, int[,] ff)
		{//pictureBox1に開いた2値化済みの画像を処理
			int i, j, k, c;
			int sum, hanten, kaisu;
			int[,] gg = new int[nx, ny];
			int[] aa = new int[9];//処理前画像周囲8画素
			int[] bb = new int[9];//処理中画像周囲8画素

			for (j = 0; j < ny; j++)
				for (i = 0; i < nx; i++) gg[i, j] = ff[i, j];

			hanten = 1;//反転回数
			kaisu = 0;//処理回数

			while (hanten != 0)//0-反転が０になるまで繰り返す）
			{
				hanten = 0;
				kaisu++;

				for (j = 1; j < ny - 1; j++)
				{
					for (i = 1; i < nx - 1; i++)
					{
						if (ff[i, j] == 0) continue;//1-画素のときだけ調査
						aa[0] = ff[i + 1, j];
						aa[1] = ff[i + 1, j - 1];
						aa[2] = ff[i, j - 1];
						aa[3] = ff[i - 1, j - 1];
						aa[4] = ff[i - 1, j];
						aa[5] = ff[i - 1, j + 1];
						aa[6] = ff[i, j + 1];
						aa[7] = ff[i + 1, j + 1];
						bb[0] = gg[i + 1, j];
						bb[1] = gg[i + 1, j - 1];
						bb[2] = gg[i, j - 1];
						bb[3] = gg[i - 1, j - 1];
						bb[4] = gg[i - 1, j];
						bb[5] = gg[i - 1, j + 1];
						bb[6] = gg[i, j + 1];
						bb[7] = gg[i + 1, j + 1];

						//周囲８－近傍の1-画素の個数                    
						sum = 0;
						for (k = 0; k < 8; k++) sum += aa[k];

						if (sum == 0) gg[i, j] = 0;//noisとみなす

						if (sum >= 3 && sum <= 5)//sum>=6はヒゲや孔を生じやすいので除く
						{                       //sum=2を含めると斜め線が短縮される
							if (connect(aa) == 1 && connect(bb) == 1) //連結性の保存
							{
								for (k = 1; k < 5; k++)//上部３点と左横
								{
									if (bb[k] == 0)
									{
										c = aa[k]; aa[k] = 0; //その点だけを0-反転しても
										if (connect(aa) != 1) { aa[k] = c; break; }//連結性が保存されるか
										aa[k] = c;//元に戻す
									}
									gg[i, j] = 0;//すべて連結性が保存されたならば0-反転
								}
							}
						}

						if (gg[i, j] == 0) hanten++;//反転個数をｲﾝｸﾘﾒﾝﾄ
					}//next i
				}//next j
				for (j = 0; j < ny; j++)
					for (i = 0; i < nx; i++)
						ff[i, j] = gg[i, j];//原画像の更新
			}//while
			return (kaisu);
		}

		//連結数計算ルーチン
		private static int connect(int[] a)
		{
			int i, cnt;

			a[8] = a[0];
			cnt = 0;
			for (i = 1; i < 9; i++)
			{
				if (a[i] == 1 && a[i - 1] == 0) cnt++;
			}
			return (cnt);
		}
	}
}
