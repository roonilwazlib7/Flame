using System;
using System.Collections.Generic;
using Flame;

namespace Fantactics
{
    class Player
    {
        public int Gold {get; set;}
        public int Uid { get; set; }
        public string Name { get; set; }       
        public bool HasControl { get; set; }

        public GameModes.GameMode GameMode { get; }
        public List<Unit> Units {get;}

        public event FlameMessageHandler AllUnitsHaveMoved;

        private Fantactics _fantactics;

        public Player(string name, int uid, int gold, GameModes.GameMode gameMode, Fantactics fantactics)
        {
            Units = new List<Unit>();
            Name = name;
            Uid = uid;
            Gold = gold;
            GameMode = gameMode;
            _fantactics = fantactics;
        }

        public Player CreateUnit(string unitName, int column, int row)
        {
            Units.Add(Unit.Create(unitName, _fantactics, column, row, this));

            return this;
        }
    }

}
