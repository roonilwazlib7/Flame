using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Geometry;

namespace Flame.Assets
{
    public class TextureMap
    {
        public TextureMap()
        {
            DrawShape = new Rectangle(0, 0, 0, 0);
            U = 0;
            V = 0;
            Width = 1;
            Height = 1;
        }
        public Shape DrawShape { get; set; }
        public double U { get; set; }
        public double V { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
