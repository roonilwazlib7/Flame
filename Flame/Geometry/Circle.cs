using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Geometry
{
    public class Circle
    {
        public Circle(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }
        public double Radius { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
