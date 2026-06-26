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
    //Packet 14406
    class TopPreviousGlobalPlayersMessage : Message
    {
        public TopPreviousGlobalPlayersMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        public override void Decode()
        {}

        public override void Process(Level level)
        {
            PreviousGlobalPlayersMessage san = new PreviousGlobalPlayersMessage(this.Client);
            PacketManager.ProcessOutgoingPacket(san);
        }
    }
}
