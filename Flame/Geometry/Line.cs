using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Geometry
{
    public class Line
    {
        public Vector Start { get; set; }
        public Vector End { get; set; }

        public Line(Vector start, Vector end)
        {
            Start = start;
            End = end;
        }

        public Line(double sX, double sY, double eX, double eY)
        {
            Start = new Vector(sX, sY);
            End = new Vector(eX, eY);
        }
    }
}
