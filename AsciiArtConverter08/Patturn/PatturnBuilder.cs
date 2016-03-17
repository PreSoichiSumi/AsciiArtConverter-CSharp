using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;
using AsciiArtConverter08.Util;

namespace AsciiArtConverter08.Patturn
{
    public class PatturnBuilder
    {
        private Font f;
        private char c;
        private char[,] normalData;
        private char[,] thinData;
        private char[,] patturn;

        private List<PointLink> linkData = null;

        //private List<PatturnInfo> infoData = null;

        public PatturnBuilder(char c, Font f, int pitch)
        {
            this.c = c;
            this.f = f;

            BuildThinData(pitch);
            Init();
        }

        public PatturnBuilder(char[,] thinData)
        {
            this.normalData = thinData;
            this.thinData = thinData;

            Init();
        }

        //public PatturnInfo[] Info
        //{
        //    get
        //    {
        //        return this.infoData.ToArray();
        //    }
        //}

        //public PointLink[] LinkData
        //{
        //    get
        //    {
        //        return this.linkData.ToArray();
        //    }
        //}

        public char[,] Patturn
        {
            get
            {
                return this.patturn;
            }
        }

        private void BuildThinData(int pitch)
        {
            Size size;

            using (Bitmap bm = new Bitmap(64, 64, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bm))
                {
                    size = TextRenderer.MeasureText(g, this.c.ToString(), this.f, new Size(), TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
                    g.Clear(Color.White);

                    int y = 0;

                    if (pitch == 2)
                    {
                        size.Height+=2;
                        y = 1;
                    }

                    if (this.f.Size < 10)
                    {
                        g.DrawString(Convert.ToString(this.c), this.f, Brushes.Black, -2, y);
                    }
                    else
                    {
                        g.DrawString(Convert.ToString(this.c), this.f, Brushes.Black, -3, y);
                    }
                }

                this.normalData = new char[size.Width + 2, size.Height + 2];

                for (int y = 0; y < this.normalData.GetLength(1); y++)
                {
                    for (int x = 0; x < this.normalData.GetLength(0); x++)
                    {
                        if (y == 0 || x == 0 || y == this.normalData.GetLength(1) - 1 || x == this.normalData.GetLength(0) - 1)
                        {
                            this.normalData[x, y] = '□';
                            continue;
                        }

                        this.normalData[x, y] = bm.GetPixel(x - 1, y - 1).R == 0 ? '■' : '□';
                    }
                }

                ThinLineUtil.DoThinLine(bm);

                this.thinData = new char[size.Width + 2, size.Height + 2];

                for (int y = 0; y < this.thinData.GetLength(1); y++)
                {
                    for (int x = 0; x < this.thinData.GetLength(0); x++)
                    {
                        if (y == 0 || x == 0 || y == this.thinData.GetLength(1) - 1 || x == this.thinData.GetLength(0) - 1)
                        {
                            this.thinData[x, y] = '□';
                            continue;
                        }

                        this.thinData[x, y] = bm.GetPixel(x - 1, y - 1).R == 0 ? '■' : '□';
                    }
                }
            }
           // Console.Out.WriteLine(showtables(this.c));
        }

        private void Init()
        {

            InitPointLink();
            ConnectPointLink();
            SetDirection();
            
            //SplitPointLink();

            BuildPatturn();

            //BuildPatturnInfo();

            //Debug.WriteLine(this.ToString());


        }

        private void BuildPatturn()
        {
            this.patturn = new char[this.thinData.GetLength(0) - 2, this.thinData.GetLength(1) - 2];

            for (int y = 0; y < this.patturn.GetLength(1); y++)
            {
                for (int x = 0; x < this.patturn.GetLength(0); x++)
                {
                    this.patturn[x, y] = ' ';
                }
            }

            foreach (PointLink pl in this.linkData)
            {
                PointLink p = pl;

                while (p != null)
                {
                    this.patturn[p.Point.X - 1, p.Point.Y - 1] = p.LineType.ToString()[0];
                    p = p.Next;
                }
            }
        }

        //private void BuildPatturnInfo()
        //{
        //    List<PointLink> backPl = new List<PointLink>(this.linkData);
        //    Dictionary<int, int> dic = new Dictionary<int, int>();

        //    this.infoData = new List<PatturnInfo>();

        //    int index = 0;

        //    for (int i = 0; i < backPl.Count; i++)
        //    {
        //        bool f = false;

        //        for (int j = 0; j < backPl.Count; j++)
        //        {
        //            bool flg = false;

        //            if (i == j)
        //            {
        //                continue;
        //            }

        //            PointLink p1 = backPl[i];

        //            while (p1 != null)
        //            {
        //                PointLink p2 = backPl[j];

        //                while (p2 != null)
        //                {
        //                    if (Math.Abs(p1.Point.X - p2.Point.X) <= 1 && Math.Abs(p1.Point.Y - p2.Point.Y) <= 1)
        //                    {

        //                        flg = true;
        //                        f = true;

        //                        break;
        //                    }

        //                    p2 = p2.Next;
        //                }

        //                if (flg)
        //                {
        //                    if (!dic.ContainsKey(i) && !dic.ContainsKey(j))
        //                    {
        //                        dic[i] = index;
        //                        dic[j] = dic[i];
        //                        index++;
        //                    }
        //                    else if (dic.ContainsKey(i) && dic.ContainsKey(j))
        //                    {
        //                        int from = 0;
        //                        int to = 0;

        //                        if (dic[i] < dic[j])
        //                        {
        //                            from = dic[j];
        //                            to = dic[i];
        //                            dic[j] = dic[i];

        //                        }
        //                        else
        //                        {
        //                            from = dic[i];
        //                            to = dic[j];
        //                            dic[i] = dic[j];
        //                        }

        //                        int[] keys = dic.Keys.ToArray();
        //                        foreach (int key in keys)
        //                        {
        //                            if (dic[key] == from)
        //                            {
        //                                dic[key] = to;
        //                            }
        //                        }
        //                    }
        //                    else if (!dic.ContainsKey(i))
        //                    {
        //                        dic[i] = dic[j];
        //                    }
        //                    else
        //                    {
        //                        dic[j] = dic[i];
        //                    }
        //                    break;
        //                }

        //                p1 = p1.Next;
        //            }

        //        }

        //        if (!f)
        //        {
        //            dic[i] = index;
        //            index++;
        //        }
        //    }

        //    this.infoData = new List<PatturnInfo>();

        //    if (dic.Count > 0)
        //    {
        //        index = 0;

        //        int[] keys = dic.Keys.ToArray();

        //        while (true)
        //        {

        //            List<PointLink> l = new List<PointLink>();

        //            for (int i = 0; i < keys.Length; i++)
        //            {
        //                if (dic[i] == index)
        //                {
        //                    l.Add(this.linkData[i]);
        //                }
        //            }

        //            if (l.Count == 0)
        //            {
        //                break;
        //            }

        //            PatturnInfo pi = new PatturnInfo(l);

        //            this.infoData.Add(pi);

        //            index++;
        //        }
        //    }
        //}

        private void SplitPointLink()
        {
            List<PointLink> lnk = new List<PointLink>(this.linkData);

            foreach (PointLink pl in lnk)
            {
                if (pl.Length < 3)
                {
                    continue;
                }

                int d = pl.LineType;
                PointLink p = pl;

                while (p != null)
                {
                    if (p.LineType != d)
                    {
                        //分割
                        this.linkData.Add(p.Split()[1]);

                        d = p.LineType;
                    }

                    p = p.Next;
                }

            }
        }


        private void SetDirection()
        {
            foreach (PointLink pl in this.linkData)
            {
                if (pl.Length == 2)
                {
                    Point p1 = pl.Start.Point;
                    Point p2 = pl.End.Point;

                    int type = DirectionUtil.GetDirection(p1, p2);

                    pl.Start.LineType = type;
                    pl.End.LineType = type;
                }
                else if (pl.Length > 2)
                {
                    PointLink p = pl.Next;
                    while (p != null)
                    {
                        if (p.Next != null)
                        {
                            Point p1 = p.Prev.Point;
                            Point p2 = p.Next.Point;

                            int type = DirectionUtil.GetDirection(p1, p2);

                            p.LineType = type;
                            //p.Next.LineType = type;
                            //p.Next.Next.LineType = type;

                            p = p.Next;
                        }
                        else
                        {
                            p = null;
                        }
                    }

                    pl.Start.LineType = pl.Start.Next.LineType;
                    pl.End.LineType = pl.End.Prev.LineType;
                }
            }
        }

        private void ConnectPointLink()
        {
            while (true)
            {
                bool flg = false;

                //縦のみ,横のみ,斜めまででチェック
                Point[] ps = new Point[] { new Point(0, 1), new Point(1, 0), new Point(1, 1) };

                foreach (Point p in ps)
                {
                    for (int i = 0; i < this.linkData.Count; i++)
                    {
                        for (int j = 0; j < this.linkData.Count; j++)
                        {
                            if (i == j)
                            {
                                continue;
                            }

                            PointLink p1 = this.linkData[i];
                            PointLink p2 = this.linkData[j];



                            if (Math.Abs(p1.End.Point.X - p2.Start.Point.X) <= p.X && Math.Abs(p1.End.Point.Y - p2.Start.Point.Y) <= p.Y)
                            {
                                //EndとStartの連結
                                flg = true;
                            }
                            else if (Math.Abs(p1.Start.Point.X - p2.Start.Point.X) <= p.X && Math.Abs(p1.Start.Point.Y - p2.Start.Point.Y) <= p.Y)
                            {
                                //StartとStartの連結
                                PointLink pl = p1.Reverse();
                                this.linkData.Remove(p1);
                                p1 = pl;
                                this.linkData.Add(p1);
                                flg = true;
                            }
                            else if (Math.Abs(p1.End.Point.X - p2.End.Point.X) <= p.X && Math.Abs(p1.End.Point.Y - p2.End.Point.Y) <= p.Y)
                            {
                                //EndとEndの連結
                                PointLink pl = p2.Reverse();
                                this.linkData.Remove(p2);
                                p2 = pl;
                                this.linkData.Add(p2);
                                flg = true;
                            }

                            if (flg)
                            {
                                p1.End.Link(p2.Start);
                                this.linkData.Remove(p2);
                                flg = true;
                                goto ReTry;
                            }
                        }
                    }
                }

            ReTry:

                if (!flg)
                {
                    break;
                }
            }

            for (int i = 0; i < this.linkData.Count; i++)
            {
                PointLink pl = this.linkData[i];

                if (pl.Start.Point.Y > pl.End.Point.Y
                    || (pl.Start.Point.Y == pl.End.Point.Y && pl.Start.Point.X > pl.End.Point.X))
                {
                    this.linkData[i] = pl.Reverse();
                }
            }

            this.linkData.Sort(new Sorter());
        }

        private void InitPointLink()
        {
            this.linkData = new List<PointLink>();

            for (int y = 0; y < this.thinData.GetLength(1); y++)
            {
                for (int x = 0; x < this.thinData.GetLength(0); x++)
                {
                    if (this.thinData[x, y] == '■')
                    {
                        PointLink pl = new PointLink(x, y);
                        this.linkData.Add(pl);
                    }
                }
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("オリジナル\r\n");

            for (int y = 0; y < this.normalData.GetLength(1); y++)
            {
                for (int x = 0; x < this.normalData.GetLength(0); x++)
                {
                    sb.Append(this.normalData[x, y]);
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            sb.Append("細線化\r\n");

            for (int y = 0; y < this.thinData.GetLength(1); y++)
            {
                for (int x = 0; x < this.thinData.GetLength(0); x++)
                {
                    sb.Append(this.thinData[x, y]);
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            char[,] c = (char[,])this.thinData.Clone();

            foreach (PointLink pl in this.linkData)
            {
                PointLink p = pl;

                while (p != null)
                {
                    c[p.Point.X, p.Point.Y] = Strings.StrConv(p.LineType.ToString(), VbStrConv.Wide, 0)[0];
                    p = p.Next;
                }
            }

            sb.Append("方向\r\n");

            for (int y = 0; y < c.GetLength(1); y++)
            {
                for (int x = 0; x < c.GetLength(0); x++)
                {
                    sb.Append(c[x, y]);
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            string[,] s = new string[c.GetLength(0), c.GetLength(1)];





            for (int i = 0; i < this.linkData.Count; i++)
            {
                PointLink p = this.linkData[i];

                while (p != null)
                {
                    if (i < 10)
                    {
                        s[p.Point.X, p.Point.Y] = Strings.StrConv(i.ToString(), VbStrConv.Wide, 0);
                    }
                    else
                    {
                        s[p.Point.X, p.Point.Y] = Convert.ToString(i);
                    }
                    p = p.Next;
                }
            }

            sb.Append("インデックス\r\n");

            for (int y = 0; y < c.GetLength(1); y++)
            {
                for (int x = 0; x < c.GetLength(0); x++)
                {
                    if (s[x, y] != null)
                    {
                        sb.Append(s[x, y]);
                    }
                    else
                    {
                        sb.Append("□");
                    }
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            return sb.ToString();
        }

        public string showtables(char tmp)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("オリジナル ");
            sb.Append(tmp);
            sb.Append("\r\n");

            for (int y = 0; y < this.normalData.GetLength(1); y++)
            {
                for (int x = 0; x < this.normalData.GetLength(0); x++)
                {
                    sb.Append(this.normalData[x, y]);
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            sb.Append("細線化\r\n");

            for (int y = 0; y < this.thinData.GetLength(1); y++)
            {
                for (int x = 0; x < this.thinData.GetLength(0); x++)
                {
                    sb.Append(this.thinData[x, y]);
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            char[,] c = (char[,])this.thinData.Clone();

            foreach (PointLink pl in this.linkData)
            {
                PointLink p = pl;

                while (p != null)
                {
                    c[p.Point.X, p.Point.Y] = Strings.StrConv(p.LineType.ToString(), VbStrConv.Wide, 0)[0];
                    p = p.Next;
                }
            }

            sb.Append("方向\r\n");

            for (int y = 0; y < c.GetLength(1); y++)
            {
                for (int x = 0; x < c.GetLength(0); x++)
                {
                    sb.Append(c[x, y]);
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            string[,] s = new string[c.GetLength(0), c.GetLength(1)];





            for (int i = 0; i < this.linkData.Count; i++)
            {
                PointLink p = this.linkData[i];

                while (p != null)
                {
                    if (i < 10)
                    {
                        s[p.Point.X, p.Point.Y] = Strings.StrConv(i.ToString(), VbStrConv.Wide, 0);
                    }
                    else
                    {
                        s[p.Point.X, p.Point.Y] = Convert.ToString(i);
                    }
                    p = p.Next;
                }
            }

            sb.Append("インデックス\r\n");

            for (int y = 0; y < c.GetLength(1); y++)
            {
                for (int x = 0; x < c.GetLength(0); x++)
                {
                    if (s[x, y] != null)
                    {
                        sb.Append(s[x, y]);
                    }
                    else
                    {
                        sb.Append("□");
                    }
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            return sb.ToString();
        }

        public char[,] NormalData
        {
            get
            {
                return normalData;
            }
        }

        public char[,] ThinData
        {
            get
            {
                return thinData;
            }
        }

        public int Width
        {
            get
            {
                return this.thinData.GetLength(0);
            }
        }

        public int Height
        {
            get
            {
                return this.thinData.GetLength(1);
            }
        }

        public class Sorter : IComparer<PointLink>
        {
            public int Compare(PointLink x, PointLink y)
            {
                if (x.Point.Y != y.Point.Y)
                {
                    return x.Point.Y.CompareTo(y.Point.Y);
                }

                return x.Point.X.CompareTo(y.Point.X);
            }
        }
    }
}
