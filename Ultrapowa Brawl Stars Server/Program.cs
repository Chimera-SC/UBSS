using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using UBSS.Network;
using UBSS.PacketProcessing;
using UBSS.Core;

namespace UBSS
{
    class Program
    {
        static void Main(string[] args)
        {
Console.WriteLine(
@"
88        88  88888888ba    ad88888ba    ad88888ba   
88        88  88      ""8b  d8""     ""8b  d8""     ""8b  
88        88  88      ,8P  Y8,          Y8,          
88        88  88aaaaaa8P'  `Y8aaaaa,    `Y8aaaaa,    
88        88  88""""""8b,       `""""""8b,      `""""""8b,  
88        88  88      `8b          `8b          `8b  
Y8a.    .a8P  88      a8P  Y8a     a8P  Y8a     a8P  
 `""Y8888Y""'   88888888P""    ""Y88888P""    ""Y88888P""   
");
            Console.WriteLine("Ultrapowa Brawl Stars Server");
            Console.WriteLine("version 0.1");
            Console.WriteLine("www.ultrapowa.com");
            Console.WriteLine("");
            Console.WriteLine("Server starting...");
            Gateway g = new Gateway();
            PacketManager ph = new PacketManager();
            MessageManager dp = new MessageManager();
            ResourcesManager rm = new ResourcesManager();
            ObjectManager pm = new ObjectManager();
            dp.Start();
            ph.Start();
            g.Start();
            ApiManager api = new ApiManager();
            Debugger.SetLogLevel(Int32.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
            Logger.SetLogLevel(Int32.Parse(ConfigurationManager.AppSettings["loggingLevel"]));
            Console.WriteLine("Server started, let's play Brawl Stars!");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
