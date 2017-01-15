using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Geometry
{
    public class Rectangle: Shape
    {
        public Rectangle(double x, double y, double width, double height)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        
        public double HalfWidth
        {
            get
            {
                return Width / 2;
            }
        }

        public double HalfHeight
        {
            get
            {
                return Height / 2;
            }
        }

        public Vector TopLeft
        {
            get
            {
                return new Vector(X, Y);
            }
        }

        public Vector TopRight
        {
            get
            {
                return new Vector(X + Width, Y);
            }
        }

        public Vector BottomLeft
        {
            get
            {
                return new Vector(X, Y + Height);
            }
        }

        public Vector BottomRight
        {
            get
            {
                return new Vector(X + Width, Y + Height);
            }
        }

        public Vector Center
        {
            get
            {
                return new Vector(X + HalfWidth, Y + HalfHeight);
            }
        }
    }
}
