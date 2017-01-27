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

            // set up our debug channels
            DebugConsole.AddChannel("Fantactics", ConsoleColor.DarkCyan, ConsoleColor.Black);

            GameGrid.LoadAssets(this);
            Unit.Load(this);
            Ability.Load(this);
        }

        public override void Initialize()
        {
            base.Initialize();


            GameGrid = new GameGrid(this, 64, 64);

            Unit u = Unit.Create("Bruiser", this, 5, 5);
            Unit u2 = Unit.Create("GrandWizard", this, 10, 7);
            Unit u3 = Unit.Create("TreeScout", this, 15, 10);
            Unit u4 = Unit.Create("Knight", this, 20, 8);
            Unit u5 = Unit.Create("Warlock", this, 14, 0);

            u5.Add(Unit.Create("Bruiser", this, 0, 0));
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
