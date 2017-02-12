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
        public BottomMenu BottomMenu { get; set; }
        GameModes.TestMode _testMode;
        public Fantactics():base("Fantactics")
        {
        }

        public override void LoadAssets()
        {
            base.LoadAssets();

            // set up our debug channels
            DebugConsole.AddChannel("Fantactics", ConsoleColor.DarkCyan, ConsoleColor.Black);
            DebugConsole.AddChannel("Fantactics-Server", ConsoleColor.DarkBlue, ConsoleColor.Black);

            Assets.LoadTexture("Assets/Fonts/impact-18.png", "impact-18");
            Assets.LoadFile("Assets/Fonts/impact-18.json", "impact-18");

            GameGrid.LoadAssets(this);
            Unit.Load(this);
            Ability.Load(this);
            BottomMenu.Load(this);
        }

        public override void Initialize()
        {
            base.Initialize();


            GameGrid = new GameGrid(this, 64, 64);
            BottomMenu = new BottomMenu(this);
            BottomMenu.Hide();

            _testMode = new GameModes.TestMode(this);
        }

        public override void Update()
        {
            base.Update();
            _testMode.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
