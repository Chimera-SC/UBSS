using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24324
    class AllianceFullEntryUpdateMessage : Message
    {

        public AllianceFullEntryUpdateMessage(Client client) : base(client)
        {
            SetMessageType(24324);
        }

        public override void Encode()
        {
            List<Byte> pack = new List<Byte>();
            SetData(pack.ToArray());
        }
    }
}
