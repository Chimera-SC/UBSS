using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Configuration;
using UBSS.PacketProcessing;
using UBSS.Core;
using UBSS.GameFiles;
using Newtonsoft.Json;

namespace UBSS.Logic
{
    class MovementComponent : Component
    {
        private const int m_vType = 0x01AB3F00;

        public MovementComponent()
        {
            //Deserialization
        }

        public override int Type
        {
            get { return 4; }
        }
    }
}
