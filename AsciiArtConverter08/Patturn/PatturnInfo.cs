using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsciiArtConverter08.Patturn
{
    public class PatturnInfo
    {
        private int len;
        private List<PointLink> pl;
        
        public PatturnInfo(List<PointLink> pl)
        {
            this.pl = new List<PointLink>(pl);

            int len = 0;
            foreach (PointLink p in pl)
            {
                len += p.Length;
            }

            this.len = len;
        }


    }
}
