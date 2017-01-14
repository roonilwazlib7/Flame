using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Geometry;
namespace Flame.Sprite
{
    public class Sprite: GameThing
    {
        public Sprite(Game game, int x, int y)
        {
            Game = game;
        }
        public Vector Position { get; set; }
    }
}
