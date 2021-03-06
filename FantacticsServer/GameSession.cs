﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FantacticsServer
{
    class GameSession
    {
        public int Uids { get; set; }

        private Dictionary<int, List<Packets.UnitPacket>> UnitPackets = new Dictionary<int, List<Packets.UnitPacket>>();

        public string HandleMessage(string message)
        {
            Messages.Message m = JsonConvert.DeserializeObject<Messages.Message>(message);

            if (m == null)
            {
                return message;
            }

            switch (m.Code)
            {
                case "EstablishGame":
                    UnitPackets.Add(Uids, new List<Packets.UnitPacket>());
                    return (Uids++).ToString();
                case "CreateUnit":
                    Messages.CreateUnit createUnitMessage = JsonConvert.DeserializeObject<Messages.CreateUnit>(message);
                    UnitPackets[createUnitMessage.Uid].Add(createUnitMessage.UnitPacket);
                    return "";
                case "GetUnits":
                    return JsonConvert.SerializeObject(UnitPackets);
            }

            return message;
        }
    }
}
