using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AsciiArtConverter08.Patturn;
using System.Diagnostics;
using AsciiArtConverter08.Util;

namespace AsciiArtConverter08.Manager.Data
{
    public class CharData
    {
        private int width = 0;
        private int height = 0;

        private bool isTarget = false;

        private char character = '\0';

        private char[,] patturn = null;

        private Dictionary<char, List<Point>> dirPoint = null;

        private int pcount = 0;

        private int pcountNormal = 0;

        private char[,] normalPatturn = null;

        //ほぼ横線
        private bool isHLine = false;

        public CharData(char c, Font f, int pitch)
        {
            this.character = c;

            this.isTarget = c == '.' || c == '　' ? false : true;

            PatturnBuilder pb = new PatturnBuilder(c, f, pitch);

            this.patturn = pb.Patturn;

            this.width = this.patturn.GetLength(0);
            this.height = this.patturn.GetLength(1);

            this.dirPoint = new Dictionary<char, List<Point>>();

            for (int dir = 0; dir < 10; dir++)
            {
                this.dirPoint[Convert.ToString(dir)[0]] = new List<Point>();
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (this.patturn[x, y] != ' ' && this.patturn[x, y] != '0')
                    {
                        pcount++;

                        char dir = Convert.ToString(this.patturn[x, y])[0];

                        this.dirPoint[dir].Add(new Point(x, y));
                    }
                }
            }

            foreach (char key in this.dirPoint.Keys)
            {
                this.dirPoint[key].Sort(new Sorter());
            }

            int normalCount = 0;

            for (int y = 0; y < pb.NormalData.GetLength(1); y++)
            {
                for (int x = 0; x < pb.NormalData.GetLength(0); x++)
                {
                    if (pb.NormalData[x, y] == '■')
                    {
                        normalCount++;
                    }
                }
            }

            int thinCount = 0;
            for (int y = 0; y < pb.ThinData.GetLength(1); y++)
            {
                for (int x = 0; x < pb.ThinData.GetLength(0); x++)
                {
                    if (pb.ThinData[x, y] == '■')
                    {
                        thinCount++;
                    }
                }
            }

            if (thinCount <= 2 || normalCount <= 2)
            {
                //AAUtil.DebugTable(pb.NormalData);
                //AAUtil.DebugTable(pb.ThinData);

                this.isTarget = false;
            }
            else if (thinCount * 100 / normalCount < 70)
            {
                //AAUtil.DebugTable(pb.NormalData);
                //AAUtil.DebugTable(pb.ThinData);

                this.isTarget = false;
            }

            this.pcountNormal = normalCount;

            int addY = -1;
            int addX = -1;
            this.normalPatturn = new char[pb.NormalData.GetLength(0) - 2, pb.NormalData.GetLength(1) - 2];

            List<Point> lst = new List<Point>();
            for (int y = 0; y < pb.NormalData.GetLength(1); y++)
            {
                for (int x = 0; x < pb.NormalData.GetLength(0); x++)
                {
                    if (x + addX < 0 || y + addY < 0 || x + addX >= this.normalPatturn.GetLength(0) || y + addY >= this.normalPatturn.GetLength(1))
                    {
                        continue;
                    }

                    if (pb.NormalData[x, y] == '■')
                    {
                        this.normalPatturn[x + addX, y + addY] = '■';
                        lst.Add(new Point(x + addX, y + addY));
                    }
                    else
                    {
                        this.normalPatturn[x + addX, y + addY] = '□';

                    }
                }
            }

            this.dirPoint['N'] = lst;
                        
            if (this.dirPoint['N'].Count>0 && this.dirPoint['1'].Count * 100 / this.dirPoint['N'].Count > 75)
            {
                this.isHLine = true;
            }
        }

        public bool IsHorizonLine
        {
            get
            {
                return this.isHLine;
            }
        }

        public char Character
        {
            get
            {
                return this.character;
            }
        }

        public char[,] Patturn
        {
            get
            {
                return this.patturn;
            }
        }

        public bool IsTarget
        {
            get
            {
                return this.isTarget;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
        }

        public int PointCount
        {
            get
            {
                return this.pcount;
            }
        }

        public int PointNormalCount
        {
            get
            {
                return this.pcountNormal;
            }
        }

        public char[,] NormalPatturn
        {
            get
            {
                return this.normalPatturn;
            }
        }

        public List<Point> GetDirPoint(char dir)
        {
            return new List<Point>(this.dirPoint[dir]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(this.character + "\r\n");

            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    sb.Append(this.patturn[x, y]);
                }

                sb.Append("\r\n");
            }

            sb.Append("\r\n");

            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    if (this.patturn[x, y] == ' ')
                    {
                        sb.Append('□');
                    }
                    else
                    {
                        sb.Append('■');
                    }
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }
    }

    class Sorter : IComparer<Point>
    {

        public int Compare(Point x, Point y)
        {
            if (x.Y == y.Y)
            {
                return x.X.CompareTo(y.X);
            }

            return x.Y.CompareTo(y.Y);
        }
    }
}
