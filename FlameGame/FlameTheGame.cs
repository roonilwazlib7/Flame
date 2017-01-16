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

namespace FlameGame
{
    class FlameTheGame: Game
    {
        Sprite _testSprite;
        public override void Update()
        {
            base.Update();
            _testSprite.Position.X = Mouse.X;
            _testSprite.Position.Y = Mouse.Y;
        }

        public override void Initialize()
        {
            _testSprite = new Sprite(this, 0, 0);
            _testSprite.BindToTexture("test");
            _testSprite.TextureMap.U = 0.5;
            _testSprite.TextureMap.Width = 0.5;
            Add(_testSprite);
        }

        public override void LoadAssets()
        {
            Assets.LoadTexture("Assets/Images/test2.png", "test");
        }
    }
}
