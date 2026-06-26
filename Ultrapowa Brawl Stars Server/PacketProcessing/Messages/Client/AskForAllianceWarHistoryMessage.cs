using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 14336
    class AskForAllianceWarHistoryMessage : Message
    {
        public AskForAllianceWarHistoryMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        public override void Decode()
        {}

        public override void Process(Level level)
        {
            AllianceWarDataMessage san = new AllianceWarHistoryMessage(this.Client);
            PacketManager.ProcessOutgoingPacket(san);
        }
    }
}
