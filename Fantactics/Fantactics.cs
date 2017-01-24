using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame;
using Flame.Games;
using Flame.Debug;

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
        }
    }
}
