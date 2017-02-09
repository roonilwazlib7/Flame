using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantactics.GameModes
{
    class TestMode: GameMode
    {
        private int playerCounter = 0;
        private List<Player> _players = new List<Player>();
        private Fantactics _fantactics;

        public Player PlayerInControl { get; set; }
        public Player LocalPlayer { get; set; }

        public TestMode(Fantactics fantactics)
        {
            _fantactics = fantactics;

            // server stuff
            Network.Client.StartClient();

            AddPlayer("Player1", 100);
            AddPlayer("Player2", 100);

            int playerUid = int.Parse(Network.Client.Send(new FantacticsServer.Messages.EstablishGame(0)));

            LocalPlayer = _players[playerUid];

            if (playerUid == 0)
            {
                LocalPlayer.CreateUnit("Knight", 5, 5);
            }
            else
            {
                LocalPlayer.CreateUnit("Bruiser", 10, 8);
            }
        }

        public Player AddPlayer(string name, int gold)
        {
            Player p = new Player(name, GetUid(), gold, this, _fantactics);
            _players.Add(p);
            return p;
        }

        public Player GivePlayerControl(int uid)
        {
            if (PlayerInControl != null)
            {
                PlayerInControl.HasControl = false;
            }
            _players[uid].HasControl = true;
            return _players[uid];
        }

        private int GetUid()
        {
            return playerCounter++;
        }
    }
}
