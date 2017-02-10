using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FantacticsServer.Messages
{
    public class Message
    {
        public string Code = "";
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
}
