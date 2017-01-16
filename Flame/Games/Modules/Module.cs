using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame.Games.Modules
{
    public class Module
    {
        Game _game;

        public Module(Game game)
        {
            _game = game;
        }

        public Game Game
        {
            get
            {
                return _game;
            }
        }
    }
}
