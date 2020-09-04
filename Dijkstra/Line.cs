using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    class Line
    {
        public Point start, end;
        public string weight,p1,p2;
        public Line(Point x1, Point y1, string w, string p1, string p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            start = x1;
            end = y1;
            weight = w;
        }
    }
}
