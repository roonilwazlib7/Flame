using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Sprites;

namespace Flame.Games.Modules
{
    public class Factory: Module
    {
        public Factory(Game game): base(game)
        {

        }

        public Sprite Sprite(int x, int y)
        {
            Sprite sprite = new Sprite(Game, x, y);
            Game.Add(sprite);

            return sprite;
        }
    }
}
