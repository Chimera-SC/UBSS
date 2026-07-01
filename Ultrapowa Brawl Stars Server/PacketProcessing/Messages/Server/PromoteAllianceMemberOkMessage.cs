using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UBSS.Logic;
using UBSS.Core;
using UBSS.Helpers;

namespace UBSS.PacketProcessing
{
    //Packet 24306
    class PromoteAllianceMemberOkMessage : Message
    {
        public PromoteAllianceMemberOkMessage(Client client, Level level, PromoteAllianceMemberMessage pam) : base(client)
        {
            SetMessageType(24306);
            m_vId = pam.m_vId;
            m_vRole = pam.m_vRole;
        }

        public override void Encode()
        {
            List<Byte> data = new List<Byte>();

            data.AddInt64(m_vId);
            data.AddInt32(m_vRole);

            SetData(data.ToArray());
        }

        readonly long m_vId;
        readonly int m_vRole;
    }
}
