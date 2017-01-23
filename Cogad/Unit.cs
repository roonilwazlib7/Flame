using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using Flame;
using Flame.Sprites;
using Flame.Games;
using Flame.Geometry;
using Newtonsoft.Json;

namespace Cogad
{
    class Unit: Sprite
    {
        TextBox _debugText;
        public static Dictionary<string, UnitDef> Defs = new Dictionary<string, UnitDef>();

        #region Attributes (defined in JSON)
        public int MoveSpeed { get; set; }
        #endregion

        public Unit(Game game, int x, int y): base(game, x, y)
        {
            State.AddState("idle", new IdleState());
            State.AddState("selected", new SelectedState());
            State.AddState("moving", new MoveState());
            State.Switch("idle");
            game.Add(this);

            On("Click", OnClick);
            On("ClickAway", OnClickAway);
            On("ArrivedAtDestination", OnArrivedAtDestination);

            _debugText = new TextBox(Game, "impact", "impact", System.Drawing.Color.Black);
            _debugText.Text = "Unit";
            

            LayerIndex = 5;
        }

        public static void LoadAssets(Game game)
        {
            game.Assets.LoadFile("Assets/Defs/units.json", "units");
            game.Assets.LoadTexture("Assets/Units/worker.png", "worker");
            game.Assets.LoadTexture("Assets/Units/warrior.png", "warrior");
            game.Assets.LoadTexture("Assets/Units/archer.png", "archer");
            game.Assets.LoadTexture("Assets/Units/priest.png", "priest");
            game.Assets.LoadTexture("Assets/Units/king.png", "king");
        }

        public static void CreateDefs(Game game)
        {
            UnitDef[] defs = JsonConvert.DeserializeObject<UnitDef[]>(game.Assets.GetFile("units"));

            foreach (UnitDef def in defs)
            {
                Defs.Add(def.Id, def);
            }
        }

        public static void Generate(Game game, string id, int column, int row)
        {
            UnitDef def = Defs[id];
            Vector v = game.AddOns.GameGrid.GetPositionFromCell(column, row);
            Unit b = new Unit(game, (int)v.X, (int)v.Y);

            b.MoveSpeed = def.MoveSpeed;

            b.Path.MoveSpeed = b.MoveSpeed;

            b.BindToTexture(id);
            b.Position.Y -= (b.Rectangle.Height - game.AddOns.GameGrid.CellSize);
        }

        public override void Update()
        {
            base.Update();
            _debugText.X = Position.X;
            _debugText.Y = Position.Y + Rectangle.Height * 1.2;
            _debugText.Text = State.CurrentState.Name + "\n" + String.Format("{0},{1}", (int)Position.X, (int)Position.Y);
        }
        public override void Draw()
        {
            base.Draw();
        }
        public Message OnClick(Message m)
        {
            State.CurrentState.Emit("Click", m);
            return m;
        }
        public Message OnClickAway(Message m)
        {
            State.CurrentState.Emit("ClickAway", m);
            return m;
        }
        public Message OnArrivedAtDestination(Message m)
        {
            State.CurrentState.Emit("Arrived", m);
            return m;
        }
    }

    #region States
    class IdleState : State<Sprite>
    {
        public IdleState():base()
        {
            On("Click", delegate (Message m)
                {
                    StateMachine.Switch("selected");
                    return m;
                }
            );
        }
        public override void Start(Sprite controlObject)
        {
            
        }
        public override void Update(Sprite controlObject)
        {

        }
        public override void End(Sprite controlObject)
        {

        }
    }

    class SelectedState : State<Sprite>
    {
        public SelectedState():base()
        {
            On("ClickAway", delegate (Message m)
            {
                Unit u = m.Sender<Unit>();
                u.Path.SetPath(new Vector[] { new Vector(u.Game.Mouse.X, u.Game.Mouse.Y) });

                StateMachine.Switch("moving");
                return m;
            });
        }
        public override void Start(Sprite controlObject)
        {
            controlObject.Game.Tween.CreateTween(controlObject.Opacity, 1)
                .From(new SpriteOpacity(0))
                .To(new SpriteOpacity(1));
        }
        public override void Update(Sprite controlObject)
        {

        }
        public override void End(Sprite controlObject)
        {
        }
    }

    class MoveState: State<Sprite>
    {
        public MoveState():base()
        {
            On("Arrived", delegate(Message m){
                StateMachine.Switch("idle");
                return m;
            });
        }
        public override void Start(Sprite controlObject)
        {
        }
        public override void Update(Sprite controlObject)
        {

        }
        public override void End(Sprite controlObject)
        {
        }
    }
    #endregion

    #region Defs
    class UnitDef
    {
        public string Id { get; set; }
        public int MoveSpeed { get; set; }
        public Cost Cost { get; set; }
    }
    #endregion
}
