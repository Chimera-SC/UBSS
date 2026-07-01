using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBSS.Logic;
using UBSS.Core;
using UBSS.Helpers;

namespace UBSS.PacketProcessing
{
    //Packet 24331
    class AllianceWarDataMessage : Message
    {
        public AllianceWarDataMessage(Client client) : base (client)
        {
            SetMessageType(24331);
        }

        public override void Encode()
        {
            List<Byte> data = new List<Byte>();

            data.AddInt32(0);
            data.AddInt32(0);

            data.AddInt32(0); // TeamHighID
            data.AddInt32(1); // TeamLowID
            data.AddString("Ultrapower");
            data.AddInt32(0);
            data.AddInt32(1);
            data.Add(0);
            data.AddRange(new List<byte> { 1, 2, 3, 4 });
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);

            data.AddInt32(0); // TeamHighID
            data.AddInt32(1); // TeamLowID
            data.AddString("Ultrapower");
            data.AddInt32(0);
            data.AddInt32(1);
            data.Add(0);
            data.AddRange(new List<byte> { 1, 2, 3, 4 });
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);

            data.AddInt32(0);
            data.AddInt32(11); // 11

            data.AddInt32(0);
            data.AddInt32(0);

            data.AddInt32(1);
            data.AddInt32(3600);
            data.AddInt32(0);
            data.AddInt32(1); // 1
            data.AddInt32(0);
            data.AddInt32(1); // 1
            data.AddInt32(0);
            data.AddInt32(2); // 2
            data.AddInt32(0);
            data.AddInt32(2); // 2

            data.AddString("Ultra");
            data.AddString("Powa");

            data.AddInt32(2);
            data.AddInt32(1);
            data.AddInt32(50);

            data.AddInt32(0);

            data.AddInt32(8);
            data.AddInt32(0);
            data.AddInt32(0);
            data.Add(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);

            SetData(data.ToArray());
        }

        public static implicit operator AllianceWarDataMessage(AllianceWarHistoryMessage v)
        {
            throw new NotImplementedException();
        }
    }
}
