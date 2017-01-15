using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Assets
{
    public class Texture
    {
        public Texture(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
