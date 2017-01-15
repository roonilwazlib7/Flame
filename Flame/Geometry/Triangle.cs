using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Geometry
{
    public class Triangle
    {
        public Vector Vertex1 { get; set; }
        public Vector Vertex2 { get; set; }
        public Vector Vertex3 { get; set; }

        public Triangle(Vector v1, Vector v2, Vector v3)
        {
            Vertex1 = v1;
            Vertex2 = v2;
            Vertex3 = v3;
        }
        public Triangle(double v1X, double v1Y, double v2X, double v2Y, double v3X, double v3Y)
        {
            Vertex1 = new Vector(v1X, v1Y);
            Vertex2 = new Vector(v2X, v2Y);
            Vertex3 = new Vector(v3X, v3Y);
        }
    }
}
