using System;
using System.IO;
using System.Collections.Generic;
using Flame;
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
            Unit.LoadDefs(game);
        }
        public static void LoadDefs(Game game)
        {
            foreach (FileInfo file in new DirectoryInfo("Assets/Defs/Units").EnumerateFiles("*.json", SearchOption.AllDirectories))
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
        public int Column { get; set; }
        public int Row { get; set; }
        public List<Ability> Abilities { get; set; }
        private TextBox _attackText;
        private TextBox _defenseText;
        private int distanceTraveled = 0;

        public static Unit Create(string name, Game game, int column, int row)
        {
            Unit u = new Unit(game, Defs[name], column, row);
            string abilities = "";
            if (Defs[name].Abilities != null)
            {
                foreach (string abl in Defs[name].Abilities)
                {
                    abilities += abl + ",";
                    if (Ability.Defs.ContainsKey(abl))
                    {
                        u.Abilities.Add(Ability.Create(abl));
                    }
                    
                }
            }

            DebugConsole.Output("Fantactics", String.Format("Created Unit: {0} at {1},{2}. A:{3} D:{4} S:{5}\nWith Abilities:{6}", name, column, row, u.Attack, u.Defense, u.Speed, abilities));
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
            Column = column;
            Row = row;

            Abilities = new List<Ability>();

            Position.Set(position.X, position.Y);
            BindToTexture(def.MainImage);

            _attackText = new TextBox(Game, "impact-18", "impact-18", System.Drawing.Color.White);
            _defenseText = new TextBox(Game, "impact-18", "impact-18", System.Drawing.Color.White);

            /*
             *             _debugText = new TextBox(Game, "impact", "impact", System.Drawing.Color.Black);
                            _debugText.Text = "Unit";
             */
            State.AddState("idle", new IdleState());
            State.AddState("selected", new SelectedState());
            State.AddState("spent", new SpentState());

            State.Switch("idle");

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

        public void MoveToCell(object sender, Message m)
        {
            Cell c = sender as Cell;
            int distance = (Math.Abs(c.Column - Column) + Math.Abs(c.Row - Row));
            distanceTraveled += distance;

            Column = c.Column;
            Row = c.Row;
            Position.X = c.Position.X;
            Position.Y = c.Position.Y;

            if (distanceTraveled >= Speed)
            {
                State.Switch("spent");
            }
            else
            {
                State.Switch("idle");
            }          
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

        public string[] Abilities { get; set; }
    }

    #region States
    class SpentState: State<Sprite>
    {
        public override void Start(Sprite controlObject)
        {
            base.Start(controlObject);
            controlObject.Opacity.Value = 0.6;
        }
    }
    class IdleState: State<Sprite>
    {
        public override void Start(Sprite controlObject)
        {
            base.Start(controlObject);

            Unit u = controlObject as Unit;

            u.OnClick += Click;
        }

        public override void End(Sprite controlObject)
        {
            base.End(controlObject);
            Unit u = controlObject as Unit;

            u.OnClick -= Click;
        }

        private void Click(object sender, Message m)
        {
            StateMachine.Switch("selected");
        }
    }

    class SelectedState: State<Sprite>
    {
        List<Cell> _possibleMoveCells = new List<Cell>();
        public override void Start(Sprite controlObject)
        {
            Unit u = controlObject as Unit;
            _possibleMoveCells = (controlObject.Game as Fantactics).GameGrid.GetCellsFromRadius(u.Column, u.Row, u.Speed);

            foreach(Cell c in _possibleMoveCells)
            {
                c.Opacity.Value = 0;
                c.OnClick += u.MoveToCell;
            }
        }
        public override void End(Sprite controlObject)
        {
            Unit u = controlObject as Unit;
            foreach (Cell c in _possibleMoveCells)
            {
                c.Opacity.Value = 1;
                c.OnClick -= u.MoveToCell;
            }
        }
    }
    #endregion
}
