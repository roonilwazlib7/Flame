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
            _testSprite = new Sprite(this, 500, 500);
            _testSprite.BindToTexture("test");
            _testSprite.Pivot.X = _testSprite.Rectangle.HalfWidth;
            _testSprite.Pivot.Y = _testSprite.Rectangle.HalfHeight;
            Add(_testSprite);

            Sprite t2 = new Sprite(this, 500, 500);
            t2.BindToTexture("test");
            //Add(t2);
            _testSprite.On("MouseLeave", Clicked);
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
}
