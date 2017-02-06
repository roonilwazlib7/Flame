using System;

namespace Fantactics
{
    class Player
    {
        public int Gold {get; set;}
        public List<Unit> Units {get;}
        public event FlameMessageHandler TurnStart;
        public event FlameMessageHandler TurnEnd;
    }

}
