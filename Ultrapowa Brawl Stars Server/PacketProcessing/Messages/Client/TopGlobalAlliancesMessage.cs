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
    //Packet 14403
    class TopGlobalAlliancesMessage : Message
    {
        public TopGlobalAlliancesMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        public override void Decode()
        {}

        public override void Process(Level level)
        {
            GlobalAlliancesMessage san = new GlobalAlliancesMessage(this.Client);
            PacketManager.ProcessOutgoingPacket(san);
        }
    }
}
