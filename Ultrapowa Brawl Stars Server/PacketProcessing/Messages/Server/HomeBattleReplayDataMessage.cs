using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBSS.Helpers;
using UBSS.Logic;

namespace UBSS.PacketProcessing
{
    //Packet 24114
    class HomeBattleReplayDataMessage : Message
    {

        public HomeBattleReplayDataMessage(Client client) : base(client)
        {
            SetMessageType(24114);
        }

        public override void Encode()
        {
            List<Byte> pack = new List<Byte>();
            SetData(pack.ToArray());
        }
    }
}
