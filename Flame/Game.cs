using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame
{
    public class Game
    {
        private List<GameThing> _things;
        private List<GameThing> _cleanedThings;

        public Game()
        {
            _things = new List<GameThing>();
            _cleanedThings = new List<GameThing>();
        }

        public void Add(GameThing thing)
        {
            _things.Add(thing);
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

        public void UpdateThings()
        {
            
        }
    }
}
