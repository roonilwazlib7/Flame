using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Sprites.Modules
{
    public abstract class Module
    {
        protected Sprite _sprite;

        public Module(Sprite sprite)
        {
            _sprite = sprite;
        }

        public Sprite Sprite
        {
            get
            {
                return _sprite;
            }
        }
    }
}
