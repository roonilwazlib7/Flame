using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantacticsServer.Packets
{
    class UnitPacket
    {
        public string Name { get; set; }
        public string Column { get; set; }
        public string Row { get; set; }
        public int Uid { get; set; }
    }
}
