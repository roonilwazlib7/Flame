using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame;
using Flame.Games;
using Flame.Sprites;
using Flame.Debug;
using Flame.Geometry;

namespace Cogad
{
    class Cogad:Game
    {
        public Cogad():base()
        {
            Title = "Cogad";
        }
        public GameGrid GameGrid { get; set; }
        public override void LoadAssets()
        {
            Assets.LoadTexture("Assets/Units/unit1.png", "unit1");
            GameGrid.LoadAssets(this);
            Terrain.LoadAssets(this);
            Building.LoadAssets(this);

            Building.CreateDefs(this);
        }
        public override void Initialize()
        {
            base.Initialize();
            Unit u = new Unit(this, 50, 50);
            AddOns = new
            {
                GameGrid = new GameGrid(this, 64, 64)
            };
            DebugConsole.AddChannel("GameGrid", ConsoleColor.Cyan, ConsoleColor.Black);
            AddOns.GameGrid.Seed(5, 5, "stone", 2);
            AddOns.GameGrid.Seed(18, 6, "forest-full", 8);
            AddOns.GameGrid.DebugCells();

            Building.Generate(this, "castle", 8, 4);
            Building.Generate(this, "house", 6, 7);
            Building.Generate(this, "barn", 4, 2);
            Building.Generate(this, "barracks", 4, 6);
        }
    }
}
