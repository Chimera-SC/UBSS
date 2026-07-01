using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using UBSS.Logic;
using UBSS.Helpers;

namespace UBSS.PacketProcessing
{
    //Command list: LogicCommand::createCommand
    static class CommandFactory
    {
        private static Dictionary<uint, Type> m_vCommands;

        static CommandFactory()
        {
            m_vCommands = new Dictionary<uint, Type>();
            m_vCommands.Add(0x0001, typeof(JoinAllianceCommand));
            m_vCommands.Add(0x0002, typeof(LeaveAllianceCommand));
            m_vCommands.Add(0x021F, typeof(KickAllianceMemberCommand));
        }

        public static object Read(BinaryReader br)
        {
            uint cm = br.ReadUInt32WithEndian();
            if (m_vCommands.ContainsKey(cm))
            {
                return Activator.CreateInstance(m_vCommands[cm], br);
            }
            else
            {
                Console.Write("\t");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Unhandled");
                Console.ResetColor();
                Console.WriteLine(" Command " + cm.ToString() + " (ignored)");
                return null;
            }
        }
    }
}
