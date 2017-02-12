using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FantacticsServer.Packets;

namespace FantacticsServer.Messages
{
    public class Message
    {
        public string Code { get; set; }
        public int Uid;
        public Message(int uid)
        {
            Code = GetType().Name;
            Uid = uid;
        }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class EstablishGame : Message
    {
        public EstablishGame(int uid): base(uid)
        {

        }
    }

    public class GetUnits : Message
    {
        public GetUnits(int uid): base(uid)
        {

        }
    }

    public class CreateUnit : Message
    {
        public UnitPacket UnitPacket { get; set; }
        public CreateUnit(int uid, UnitPacket packet): base(uid)
        {
            UnitPacket = packet;
        }
    }
}
