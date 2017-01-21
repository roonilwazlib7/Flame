using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Sprites;
using Flame.Games;
using Flame.Geometry;
using Newtonsoft.Json;

namespace Cogad
{
    class Building: Sprite
    {
        public static Dictionary<string, BuildingDef> Defs = new Dictionary<string, BuildingDef>();

        public Building(Game game, int x, int y, int widthOverlap = 0, int heightOverlap = 0): base(game, x - widthOverlap, y - heightOverlap)
        {

            game.Add(this);

            LayerIndex = 5;
        }

        public static void LoadAssets(Game game)
        {
            game.Assets.LoadTexture("Assets/Buildings/castle.png", "castle");
            game.Assets.LoadTexture("Assets/Buildings/house.png", "house");
            game.Assets.LoadTexture("Assets/Buildings/barn.png", "barn");
            game.Assets.LoadTexture("Assets/Buildings/barracks.png", "barracks");
            game.Assets.LoadFile("Assets/Defs/buildings.json", "buildings");
        }

        public static void CreateDefs(Game game)
        {
            BuildingDef[] defs = JsonConvert.DeserializeObject<BuildingDef[]>(game.Assets.GetFile("buildings"));

            foreach(BuildingDef def in defs)
            {
                Defs.Add(def.Id, def);
            }
        }

        public static void Generate(Game game, string id, int column, int row)
        {
            BuildingDef def = Defs[id];
            Vector v = game.AddOns.GameGrid.GetPositionFromCell(column, row);
            Building b = new Building(game, (int)v.X, (int)v.Y);

            b.BindToTexture(id);

            b.Position.Y -= (b.Rectangle.Height - game.AddOns.GameGrid.CellSize);
        }
    }

    class BuildingDef
    {
        public string Id { get; set; }
    }
}
