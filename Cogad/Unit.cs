using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using Flame;
using Flame.Sprites;
using Flame.Games;

namespace Cogad
{
    class Unit: Sprite
    {
        public Unit(Game game, int x, int y): base(game, x, y)
        {
            BindToTexture("unit1");
            State.AddState("idle", new IdleState());
            State.AddState("selected", new SelectedState());
            game.Add(this);

            On("Click", OnClick);
            On("ClickAway", OnClickAway);
        }
        public Message OnClick(Message m)
        {
            State.Switch("selected");
            return m;
        }
        public Message OnClickAway(Message m)
        {
            State.Switch("idle");
            return m;
        }
    }

    class IdleState: State<Sprite>
    {
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
        public override void Start(Sprite controlObject)
        {
            controlObject.Opacity.Value = 0.5;
           // controlObject.Game.Tween.CreateTween(controlObject.Rotation, 1).To(new SpriteRotation(360));
        }
        public override void Update(Sprite controlObject)
        {

        }
        public override void End(Sprite controlObject)
        {
            controlObject.Opacity.Value = 1;
            controlObject.Position.X = controlObject.Game.Mouse.X;
            controlObject.Position.Y = controlObject.Game.Mouse.Y;
        }
    }
}
