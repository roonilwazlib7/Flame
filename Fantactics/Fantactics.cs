using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame;
using Flame.Games;
using Flame.Debug;
using Flame.Geometry;

namespace Fantactics
{
    class Fantactics: Game
    {
        public GameGrid GameGrid { get; set; }
        public Fantactics():base("Fantactics")
        {

        }

        public override void LoadAssets()
        {
            base.LoadAssets();
            GameGrid.LoadAssets(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            // set up our debug channels
            DebugConsole.AddChannel("Fantactics", ConsoleColor.Black, ConsoleColor.DarkCyan);

            GameGrid = new GameGrid(this, 64, 64);

            Caster.CastRay(new Line(0, 0, 7, 7), delegate (object o)
               {
                   DebugConsole.Output("Flame", "Finished ray cast");
                   return o;
               });
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
