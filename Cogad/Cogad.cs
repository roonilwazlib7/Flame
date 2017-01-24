using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Flame;
using Flame.Games;
using Flame.Sprites;
using Flame.Debug;
using Flame.Geometry;

namespace Cogad
{
    class Cogad:Game
    {
        TextBox _fps;
        public Cogad():base()
        {
            Title = "Cogad";
        }
        public GameGrid GameGrid { get; set; }
        public TopMenu TopMenu { get; set; }
        public override void LoadAssets()
        {
            Assets.LoadFile("Assets/Fonts/impact.json", "impact");
            Assets.LoadTexture("Assets/Fonts/impact.png", "impact");
            Assets.LoadFile("Assets/Maps/samplemap.txt", "sample-map");
            TopMenu.LoadAssets(this);
            GameGrid.LoadAssets(this);
            Terrain.LoadAssets(this);
            Building.LoadAssets(this);
            Unit.LoadAssets(this);

            Unit.CreateDefs(this);
        }
        public override void Initialize()
        {
            base.Initialize();
            TopMenu = new TopMenu(this);

            AddOns = new
            {
                GameGrid = new GameGrid(this, 64, 64)   
            };
            GameGrid = AddOns.GameGrid;
            DebugConsole.AddChannel("GameGrid", ConsoleColor.Cyan, ConsoleColor.Black);
            DebugConsole.AddChannel("Map", ConsoleColor.Gray, ConsoleColor.Black);

            Map.Create(Assets.GetFile("sample-map"), this);

            AddOns.GameGrid.DebugCells();
            _fps = new TextBox(this, "impact", "impact", Color.Black);

        }

        public override void Update()
        {
            base.Update();
            _fps.Text = ((int)(1 / RenderTime) ).ToString();
        }
    }
}
