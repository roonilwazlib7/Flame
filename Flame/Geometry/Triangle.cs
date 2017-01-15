using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Geometry
{
    public class Triangle
    {
        private Vector _vertex1;
        private Vector _vertex2;
        private Vector _vertex3;

        public double X { get; set; }
        public double Y { get; set; }

        public Triangle(Vector v1, Vector v2, Vector v3, double x = 0, double y = 0)
        {
            Vertex1 = v1;
            Vertex2 = v2;
            Vertex3 = v3;
            X = x;
            Y = y;
        }
        public Triangle(double v1X, double v1Y, double v2X, double v2Y, double v3X, double v3Y, double x = 0, double y = 0)
        {
            Vertex1 = new Vector(v1X, v1Y);
            Vertex2 = new Vector(v2X, v2Y);
            Vertex3 = new Vector(v3X, v3Y);
            X = x;
            Y = y;
        }
        public Vector Vertex1
        {
            get
            {
                return new Vector(_vertex1.X + X, _vertex1.Y + Y);
            }
            set
            {
                _vertex1 = value;
            }
        }

        public Vector Vertex2
        {
            get
            {
                return new Vector(_vertex2.X + X, _vertex2.Y + Y);
            }
            set
            {
                _vertex2 = value;
            }
        }

        public Vector Vertex3
        {
            get
            {
                return new Vector(_vertex3.X + X, _vertex3.Y + Y);
            }
            set
            {
                _vertex3 = value;
            }
        }
    }
}
