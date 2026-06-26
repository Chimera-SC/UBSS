using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCS.Helpers;
using UCS.Logic;
using UCS.Core;

namespace UCS.PacketProcessing
{
    //Packet 24402
    class LocalAlliancesMessage : Message
    {

        public LocalAlliancesMessage(Client client) : base(client)
        {
            SetMessageType(24402);
        }

        public override void Encode()
        {
            var packet = new List<byte>();
            var data = new List<byte>();

            var i = 1;
            foreach (var alliance in ObjectManager.GetInMemoryAlliances().OrderByDescending(t => t.GetScore()))
            {
                var all = alliance.GetAllianceId();
                data.AddInt64(all);
                data.AddString(alliance.GetAllianceName()/* + " #" + alliance.GetAllianceId()*/);
                data.AddInt32(i);
                data.AddInt32(alliance.GetScore());
                data.AddInt32(i);
                data.AddInt32(alliance.GetAllianceBadgeData());
                data.AddInt32(alliance.GetAllianceMembers().Count);
                data.AddInt32(0);
                data.AddInt32(alliance.GetAllianceLevel());
                if (i >= 101)
                    break;
                i++;
            }
            packet.AddInt32(i - 1);
            packet.AddRange(data.ToArray());

            SetData(data.ToArray());
        }
    }
}
