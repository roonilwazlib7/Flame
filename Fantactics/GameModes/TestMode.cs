using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Debug;
using FantacticsServer.Packets;
using Newtonsoft.Json;

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
            Network.Client.SetUpConnections(null, 11000);

            AddPlayer("Player1", 100);
            AddPlayer("Player2", 100);

            string response = Network.Client.Send(new FantacticsServer.Messages.EstablishGame(0));
            int playerUid = response == "" ? 0: int.Parse(response);

            LocalPlayer = _players[playerUid];

            if (playerUid == 0)
            {
                LocalPlayer.CreateUnit("Knight", 5, 5);
            }
            else
            {
                LocalPlayer.CreateUnit("Bruiser", 10, 8);
            }

            LocalPlayer.HasControl = true;
        }

        public void Update()
        {
            string response = Network.Client.Send(new FantacticsServer.Messages.GetUnits(LocalPlayer.Uid));
            Dictionary<int, List<UnitPacket>> packets = JsonConvert.DeserializeObject<Dictionary<int, List<UnitPacket>>>(response);

            if (packets != null)
            {
                foreach (KeyValuePair<int, List<UnitPacket>> p in packets)
                {
                    if (p.Key != LocalPlayer.Uid)
                    {
                        foreach (UnitPacket pk in p.Value)
                        {
                            if (!Unit.UnitExists(pk.Uid))
                            {
                                _players[p.Key].CreateUnit(pk.Name, pk.Column, pk.Row, false);
                            }
                        }
                    }
                }
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
