using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Sprites;
using Flame.Games;
using Flame;

namespace Cogad
{
    class Terrain: Sprite
    {
        public Terrain(Game game, int x, int y): base(game, x, y)
        {
            BindToTexture("grass");
  
            Game.Add(this);

            On("Click", delegate (Message m)
            {
                if (Unit.SelectedUnits.Count > 0)
                {
                    throw new Exception();
                }
                return m;
            });
        }
        public static void LoadAssets(Game game)
        {
            game.Assets.LoadTexture("Assets/Terrain/grass.png", "grass");
            game.Assets.LoadTexture("Assets/Terrain/stone.png", "stone");
            game.Assets.LoadTexture("Assets/Terrain/gold.png", "gold");
            game.Assets.LoadTexture("Assets/Terrain/forest-full.png", "forest-full");
        }
    }
}
