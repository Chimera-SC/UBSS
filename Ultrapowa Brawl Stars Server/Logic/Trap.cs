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
using Newtonsoft.Json.Linq;

namespace UBSS.Logic
{
    class Trap : ConstructionItem
    {
        public override int ClassId
        {
            get { return 4; }
        }

        public Trap(Data data, Level l) : base(data, l)
        {
            AddComponent(new TriggerComponent());
        }

        public TrapData GetTrapData()
        {
            return (TrapData)GetData();
        }
    }
}
