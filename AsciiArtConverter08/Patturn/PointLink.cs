using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace AsciiArtConverter08.Patturn
{
    public class PointLink
    {
        private Point point;
        private PointLink next = null;
        private PointLink prev = null;
        private PointLink start = null;
        private PointLink end = null;
        private int length = 0;

        private int type = 0;

        public PointLink(int x, int y)
        {
            this.point = new Point(x, y);
            this.start = this;
            this.end = this;
            this.length = 1;
        }

        public PointLink Reverse()
        {

            PointLink pr;

            pr = this.start;

            //Debug.WriteLine("前");

            while (pr != null)
            {
                //Debug.WriteLine(pr.ToString());
                pr = pr.next;
            }
            
            pr = this.end;

            while (pr != null)
            {
                PointLink p = pr.end;
                pr.end = pr.start;
                pr.start = p;

                p = pr.next;
                pr.next = pr.prev;
                pr.prev = p;

                pr = pr.next;
            }

            //Debug.WriteLine("後");
            pr = this.start;
            while (pr != null)
            {
                //Debug.WriteLine(pr.ToString());
                pr = pr.next;
            }

            return this.start;
        }

        public void Link(PointLink p)
        {
            this.end.next = p;
            p.prev = this.end;
            p.start = this.start;
            this.end = p.end;

            int len = 0;

            PointLink pl = this.start;

            while (pl != null)
            {
                len++;
                pl = pl.next;
            }

            pl = this.start;

            while (pl != null)
            {
                pl.length = len;
                pl.end = this.end;
                pl.start = this.start;

                pl = pl.next;
            }
        }

        public PointLink[] Split()
        {
            PointLink p1 = this.start;
            PointLink p2 = this;

            p1.end = p2.prev;
            p2.prev.end = p2.prev;
            p2.prev.next = null;

            p2.start = p2;
            p2.prev = null;
            p2.end.start = p2;

            PointLink tmp = p1;

            //Debug.WriteLine("");

            //Debug.WriteLine(this);

            //Debug.WriteLine("");

            int len = 0;
            while (tmp != null)
            {
                len++;
                tmp = tmp.next;
            }

            tmp = p1;
            while (tmp != null)
            {
                tmp.length = len;
                tmp.end = tmp.start.end;

                //Debug.WriteLine(tmp);

                tmp = tmp.next;
            }

            tmp = p2;

            //Debug.WriteLine("");

            len = 0;
            while (tmp != null)
            {
                len++;
                tmp = tmp.next;
            }

            tmp = p2;
            while (tmp != null)
            {
                tmp.length = len;
                tmp.start = p2.start;

                //Debug.WriteLine(tmp);

                tmp = tmp.next;
            }


            return new PointLink[] { p1, p2 };

        }

        public Point Point
        {
            get
            {
                return point;
            }
        }

        public int LineType
        {
            get
            {
                return type;
            }
            set
            {
                this.type = value;
            }
        }

        public PointLink Next
        {

            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }

        public PointLink Prev
        {
            get
            {
                return prev;
            }
            set
            {
                prev = value;
            }
        }

        public PointLink Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }

        public PointLink End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append("　座標：" + this.point);

            sb.Append("　方向：" + this.LineType);

            if (this.prev != null)
            {
                sb.Append("　前：" + this.prev.point);
            }else{
                sb.Append("　前：");
            }

            if (this.next != null)
            {
                sb.Append("　次：" + this.next.point);
            }else{
                sb.Append("　次：");
            }

            sb.Append("　開始：" + this.start.point);

            sb.Append("　終了：" + this.end.point);

            sb.Append("　長さ：" + this.length);

            return sb.ToString();
        }
    }
}
