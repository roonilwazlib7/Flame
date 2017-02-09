using System;
using Newtonsoft.Json;

namespace Fantactics.Server.Messages
{
    class Base
    {
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
