using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using Newtonsoft.Json;
using UCS.Logic;
using UCS.Core;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 24101
    class OwnHomeDataMessage : Message
    {

        private byte[] m_vSerializedVillage { get; set; }

        public OwnHomeDataMessage(Client client, Level level) : base (client)
        {
            SetMessageType(24101);
            this.Player = level;
        }

        public override void Encode()
        {
            List<Byte> data = new List<Byte>();

            ClientHome ch = new ClientHome(Player.GetPlayerAvatar().GetId());

            data.AddRange(ch.Encode());
            data.AddRange(Player.GetPlayerAvatar().Encode());
            data.Add(0);//2017189);

            SetData(data.ToArray());
        }

        public Level Player { get; set; }
    }
}
