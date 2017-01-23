using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Games;
using Flame.Sprites;

namespace Cogad
{
    class TopMenu: Sprite
    {
        public TopMenu(Game game): base(game, 0, 0)
        {
            BindToTexture("topmenu-background");
            Rectangle.Width = game.ClientSize.Width;

            game.Add(this);
        }

        public static void LoadAssets(Game game)
        {
            game.Assets.LoadTexture("Assets/UI/panel-brown.png", "topmenu-background");
        }
    }
}
