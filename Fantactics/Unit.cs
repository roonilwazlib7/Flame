using System;
using System.IO;
using System.Collections.Generic;
using Flame.Games;
using Flame.Debug;
using Flame.Sprites;
using Flame.Geometry;
using Newtonsoft.Json;

namespace Fantactics
{
    class Unit: Sprite
    {
        public static Dictionary<string, UnitDef> Defs = new Dictionary<string, UnitDef>();
        public static void Load(Game game)
        {
            game.Assets.LoadTexture("Assets/Units/default.png", "unit-default");
            foreach(FileInfo file in new DirectoryInfo("Assets/Defs/Units").EnumerateFiles("*.json", SearchOption.AllDirectories))
            {
                string unitName = file.Name.Replace(".json", "");
                string unitDef = "";

                StreamReader r = new StreamReader(file.FullName);
                unitDef = r.ReadToEnd();
                r.Close();

                if (!Defs.ContainsKey(unitName))
                {
                    UnitDef d = JsonConvert.DeserializeObject<UnitDef>(unitDef);
                    BlendDef(d, game);
                    Defs.Add(unitName, d);
                }                

                DebugConsole.Output("Fantactics", "Loaded Unit: " + unitName);
            }
        }

        public static void BlendDef(UnitDef def, Game game)
        {
            string mainImage = "Assets/Units/" + def.UnitType + "/" + def.Name + "-Main.png";
            if (File.Exists(mainImage))
            {
                game.Assets.LoadTexture(mainImage, def.Name + "-Main");
                def.MainImage = def.Name + "-Main";
            }
            else
            {
                def.MainImage = "unit-default";
            }
        }

        public string Name { get; set; }
        public string UnitType { get; set; }
        public int BuildCost { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public static Unit Create(string name, Game game, int column, int row)
        {
            Unit u = new Unit(game);
            UnitDef def = Defs[name];
            Vector position = (game as Fantactics).GameGrid.GetPositionFromCell(column, row);

            u.Name = def.Name;
            u.UnitType = def.UnitType;
            u.BuildCost = def.BuildCost;
            u.Attack = def.Attack;
            u.Defense = def.Defense;
            u.Speed = def.Speed;

            u.Position.Set(position.X, position.Y);
            u.BindToTexture(def.MainImage);
            

            game.Add(u);

            return u;
        }
        public Unit(Game game):base(game, 0, 0)
        {

        }
    }

    class UnitDef
    {
        public string Name { get; set; }
        public string UnitType { get; set; }
        public int BuildCost { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public string MainImage { get; set; }
    }
}
