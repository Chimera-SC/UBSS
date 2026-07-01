using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UBSS.Helpers;
using UBSS.Logic;
using UBSS.Network;

namespace UBSS.PacketProcessing
{
    //Packet 14331
    class AskForAllianceWarDataMessage : Message
    {
        public AskForAllianceWarDataMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        public override void Decode()
        {}

        public override void Process(Level level)
        {
            AllianceWarDataMessage san = new AllianceWarDataMessage(this.Client);
            PacketManager.ProcessOutgoingPacket(san);
        }
    }
}
