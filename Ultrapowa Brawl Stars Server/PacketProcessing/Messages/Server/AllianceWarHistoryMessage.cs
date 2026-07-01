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
    //Packet 24338
    class AllianceWarHistoryMessage : Message
    {
        public AllianceWarHistoryMessage(Client client) : base (client)
        {
            SetMessageType(24338);
        }

        public override void Encode()
        {
            List<Byte> data = new List<Byte>();

            data.AddInt32(1);

            data.AddLong(0, 1); // 1 Alliance ID
            data.AddString("Clashers"); // 1 Alliance Name
            data.AddInt32(13000000); // 1 Alliance Badge
            data.AddInt32(10); // 1 Alliance Level

            data.AddLong(9999); // 2 Alliance ID
            data.AddString("Bug Stars"); // 2 Alliance Name
            data.AddInt32(0); // 2 Alliance Badge
            data.AddInt32(1); // 2 Alliance Level

            data.AddInt32(9999); // 1 Stars
            data.AddInt32(0); // 2 Stars

            data.AddInt32(0); // 1 Village Destroyed
            data.AddInt32(100); // 2 Village Destroyed

            data.AddInt32(50); // 1 Unknown
            data.AddInt32(50); // 2 Unknown

            data.AddInt32(100); // Attack Used
            data.AddInt32(4000); // XP Earned

            data.AddLong(515631654); // War ID
            data.AddLong(40); // Members Count

            data.AddInt32(1); // War Won Count

            data.Add(99);
            data.AddInt32((int)TimeSpan.FromDays(1).TotalSeconds);
            data.AddLong((int)(TimeSpan.FromDays(1).TotalSeconds - TimeSpan.FromDays(0.5).TotalSeconds));

            SetData(data.ToArray());
        }
    }
}
