using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Sprites;
using Flame.Games;

namespace Cogad
{
    class Terrain: Sprite
    {
        private static int seed = 5;
        public Terrain(Game game, int x, int y): base(game, x, y)
        {
            BindToTexture("grass");
  
            Game.Add(this);
        }
        public static void LoadAssets(Game game)
        {
            game.Assets.LoadTexture("Assets/Terrain/grass.png", "grass");
            game.Assets.LoadTexture("Assets/Terrain/stone.png", "stone");
            game.Assets.LoadTexture("Assets/Terrain/dirt.png", "dirt");
            game.Assets.LoadTexture("Assets/Terrain/sand.png", "sand");
            game.Assets.LoadTexture("Assets/Terrain/forest-full.png", "forest-full");
        }
    }
}
