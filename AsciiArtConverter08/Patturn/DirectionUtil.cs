using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace AsciiArtConverter08.Patturn
{
    class DirectionUtil
    {
        private const int MAX = 8;

        private static double[] range;

        static DirectionUtil()
        {
            double p = Math.PI / MAX;
            double p1 = 0;

            p1 = -p / 2;

            range = new double[MAX + 2];


            for (int i = 0; i < MAX + 2; i++)
            {
                range[i] = p1;
                p1 += p;
            }
        }

        public static int GetDirection(Point p1, Point p2)
        {
            if (p1.X == p2.X)
            {
                return (MAX + 2) / 2;
            }

            if (p1.Y == p2.Y)
            {
                return 1;
            }

            double r = Math.Atan(((double)p1.Y - (double)p2.Y) / ((double)p2.X - (double)p1.X));

            if (r < 0)
            {
                r += Math.PI;
            }

            //Debug.WriteLine(r);

            for (int i = 0; i < range.Length - 1; i++)
            {
                if (range[i] <= r && range[i + 1] > r)
                {
                    if (i == range.Length - 2)
                    {
                        return 1;
                    }
                    return i + 1;
                }
            }

            return 0;


        }
       
    }
}
