﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Flame.Sprites;
using Flame.Geometry;

namespace FlameGame
{
    class FlameTheGame
    {
        Flame.Game _game;
        public FlameTheGame()
        {
            _game = new Flame.Game();
            _game.Assets.LoadTexture("Assets/Images/test.png", "test");
            _game.Assets.LoadTexture("Assets/Images/test2.png", "test2");

            Sprite t = new Sprite(_game, 0, 0);
            t.BindToTriangle(new Triangle(0, 0, 500, 0, 0, 500));
            _game.Add(t);
        }
        public void Start()
        {
            _game.Run(30);
        }
    }
}
