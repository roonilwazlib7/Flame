using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame;
using Flame.Games;
using Flame.Sprites;

namespace Cogad
{
    class Cogad:Game
    {
        public Cogad():base()
        {
            Title = "Cogad";
        }
        public override void LoadAssets()
        {
            Assets.LoadTexture("Assets/Units/unit1.png", "unit1");
        }
        public override void Initialize()
        {
            base.Initialize();
            Unit u = new Unit(this, 50, 50);
        }
    }
}
