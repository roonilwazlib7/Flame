using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame;
using Flame.Sprites;
using Flame.Games;
using Flame.Geometry;
using Newtonsoft.Json;

namespace Cogad
{
    class Building: Sprite
    {
        public static Dictionary<string, BuildingDef> Defs = new Dictionary<string, BuildingDef>();
        TextBox _debugText;

        public Building(Game game, int x, int y, int widthOverlap = 0, int heightOverlap = 0): base(game, x - widthOverlap, y - heightOverlap)
        {
            State.AddState("idle", new BuildingIdleState());
            State.Switch("idle");
            game.Add(this);
            _debugText = new TextBox(Game, "impact", "impact", System.Drawing.Color.Black);
            LayerIndex = 5;
        }

        public static void LoadAssets(Game game)
        {
            game.Assets.LoadFile("Assets/Defs/buildings.json", "buildings");
            Building.CreateDefs(game);

            foreach (KeyValuePair<string,BuildingDef> def in Defs)
            {
                game.Assets.LoadTexture("Assets/Buildings/" + def.Value.Id + ".png", def.Value.Id);
            }
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

        public override void Update()
        {
            base.Update();
            _debugText.X = Position.X;
            _debugText.Y = Rectangle.Y;
            _debugText.Text = State.CurrentState.Name;
        }
    }

    class BuildingDef
    {
        public string Id { get; set; }
    }

    #region States
    class BuildingIdleState: State<Sprite>
    {

    }
    #endregion
}
