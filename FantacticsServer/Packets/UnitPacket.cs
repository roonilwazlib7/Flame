using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantacticsServer.Packets
{
    public class UnitPacket
    {
        public string Name { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public string Uid { get; set; }
    }
}
