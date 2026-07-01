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
    class Deco : GameObject
    {
        private Level m_vLevel;

        public override int ClassId
        {
            get { return 6; }
        }

        public Deco(Data data, Level l) : base(data, l)
        {
            m_vLevel = l;
        }

        public DecoData GetDecoData()
        {
            return (DecoData)GetData();
        }

        public new JObject Save(JObject jsonObject)
        {
            base.Save(jsonObject);
            return jsonObject;
        }

        public new void Load(JObject jsonObject)
        {
            base.Load(jsonObject);
        }
    }
}
