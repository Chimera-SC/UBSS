using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UCS.Logic;
using UCS.Core;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 24340
    class BookmarksListMessage : Message
    {
        public BookmarksListMessage(Client client) : base(client)
        {
            SetMessageType(24340);
        }

        public override void Encode()
        {
            List<Byte> data = new List<Byte>();



            SetData(data.ToArray());
        }
    }
}
