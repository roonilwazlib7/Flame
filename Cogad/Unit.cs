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

namespace Cogad
{
    class Unit: Sprite
    {
        public Unit(Game game, int x, int y): base(game, x, y)
        {
            BindToTexture("unit1");
            State.AddState("idle", new IdleState());
            State.AddState("selected", new SelectedState());
            State.AddState("moving", new MoveState());
            State.Switch("idle");
            game.Add(this);

            On("Click", OnClick);
            On("ClickAway", OnClickAway);
            On("ArrivedAtDestination", OnArrivedAtDestination);        

            LayerIndex = 5;
        }
        public override void Update()
        {
            base.Update();
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

    class IdleState: State<Sprite>
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
}
