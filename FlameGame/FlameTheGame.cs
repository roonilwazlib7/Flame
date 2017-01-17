using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using Flame.Sprites;
using Flame.Geometry;
using Flame.Games;
using Flame;

namespace FlameGame
{
    class FlameTheGame: Game
    {
        Sprite _testSprite;
        public override void Update()
        {
            base.Update();
        }

        public override void Initialize()
        {
            _testSprite = Factory.Sprite(500, 500);
            _testSprite.BindToTexture("test");
            _testSprite.Pivot.X = _testSprite.Rectangle.HalfWidth;
            _testSprite.Pivot.Y = _testSprite.Rectangle.HalfHeight;
            _testSprite.Rotation.Value = 45;
            _testSprite.State.AddState("Enter", new EnterState());
            _testSprite.State.Switch("Enter");
        }

        public override void LoadAssets()
        {
            Assets.LoadTexture("Assets/Images/test2.png", "test");
        }

        Message Clicked(Message m)
        {
            m.Sender<Sprite>().Position.X = 0;
            return m;
        }
    }
    class EnterState: State<Sprite>
    {
        public override void Start(Sprite controlObject)
        {
            controlObject.Game.Tween.CreateTween(controlObject.Opacity, 1)
                .From(new SpriteOpacity(0))
                .To(new SpriteOpacity(1));
        }
    }
}
