using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Geometry;

namespace Flame.Sprites.Modules
{
    public class Body: Module
    {
        public Body(Sprite sprite): base(sprite)
        {
            Velocity = new Vector(0, 0);
            Acceleration = new Vector(0, 0);
        }
        public Vector Velocity { get; set; }
        public Vector Acceleration { get; set; }

        public void Update()
        {
            Sprite.Position.X += Velocity.X * Sprite.Game.Delta;
            Sprite.Position.Y += Velocity.Y * Sprite.Game.Delta;

            Velocity.X += Acceleration.X * Sprite.Game.Delta;
            Velocity.Y += Acceleration.Y * Sprite.Game.Delta;
        }
    }
}
