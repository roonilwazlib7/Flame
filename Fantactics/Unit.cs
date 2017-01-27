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

            if (def.Speed == 0) def.Speed = 1;
        }

        public string Name { get; set; }
        public string UnitType { get; set; }
        public int BuildCost { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        private TextBox _attackText;
        private TextBox _defenseText;

        public static Unit Create(string name, Game game, int column, int row)
        {
            Unit u = new Unit(game, Defs[name], column, row);
            DebugConsole.Output("Fantactics", String.Format("Created Unit: {0} at {1},{2}. A:{3} D:{4} S:{5}", name, column, row, u.Attack, u.Defense, u.Speed));
            return u;
        }
        public Unit(Game game, UnitDef def, int column, int row):base(game, 0, 0)
        {
            Vector position = (game as Fantactics).GameGrid.GetPositionFromCell(column, row);

            Name = def.Name;
            UnitType = def.UnitType;
            BuildCost = def.BuildCost;
            Attack = def.Attack;
            Defense = def.Defense;
            Speed = def.Speed;

            Position.Set(position.X, position.Y);
            BindToTexture(def.MainImage);

            _attackText = new TextBox(Game, "impact-18", "impact-18", System.Drawing.Color.White);
            _defenseText = new TextBox(Game, "impact-18", "impact-18", System.Drawing.Color.White);

            /*
             *             _debugText = new TextBox(Game, "impact", "impact", System.Drawing.Color.Black);
                            _debugText.Text = "Unit";
             */

            Game.Add(this);
        }

        public override void Update()
        {
            base.Update();

            _attackText.SetPosition(Position.X, Position.Y);
            _attackText.Text = Attack.ToString();

            _defenseText.SetPosition(Position.X, Position.Y + Rectangle.Height - 18);
            _defenseText.Text = Defense.ToString();
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
