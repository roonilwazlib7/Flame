using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Geometry
{
    public class Vector
    {
        double _x;
        double _y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
            Resolve();
        }
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                Resolve();
            }
        }       
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                Resolve();
            }
        }
        public double Magnitude { get; set; }
        public double Angle { get; set; }

        public Vector Resolve()
        {
            Magnitude = Math.Sqrt(_x * _x + _y * _y);
            Angle = Math.Atan2(_y, _x);
            return this;
        }

        public Vector Set(double x, double y)
        {
            _x = x;
            _y = y;
            return Resolve();
        }

        public Vector Normalize()
        {
            _x /= Magnitude;
            _y /= Magnitude;
            return Resolve();
        }

        public double DotProduct(Vector vector)
        {
            return 0;
        }

        public double DistanceTo(Vector v)
        {
            Vector dif = this - v;
            dif.Resolve();
            return dif.Magnitude;
        }

        public static bool operator== (Vector v1, Vector v2)
        {
            if (Object.Equals(v1, null) || Object.Equals(v2, null)) return false;
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public static bool operator!= (Vector v1, Vector v2)
        {
            if (Object.Equals(v1, null) && Object.Equals(v2, null))
            {
                return false;
            }
            if (Object.Equals(v1, null) || Object.Equals(v2, null))
            {
                return true;
            }
            return v1.X != v2.X || v1.Y != v2.Y;
        }

        public static Vector operator+ (Vector v1, Vector v2)
        {
            return new Vector(v2.X + v1.X, v2.Y + v1.Y);
        }

        public static Vector operator- (Vector v1, Vector v2)
        {
            return new Vector(v2.X - v1.X, v2.Y - v1.Y);
        }

        public static double operator* (Vector v1, Vector v2)
        {
            return v1.DotProduct(v2);
        }

        public static Vector operator *(Vector v1, double n)
        {
            v1.X *= n;
            v1.Y *= n;

            return v1;
        }
    }
}
