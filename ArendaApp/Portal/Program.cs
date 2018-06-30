using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArendaRESTLib;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Portal
{
    class Program
    {
        static void Main(string[] args)
        {

            Config.Load();
                        
            if (!DataModule.Connect(Config.Server, Config.Base, Config.User, Config.Pass))
            {
                Console.WriteLine("Cant connect to MSSQL by {0} {1} {2} {3}",
                    Config.Server, Config.Base, Config.User, Config.Pass);
                Console.ReadLine();
                return;
            }
            
            Uri tcpUri = new Uri(Config.ArendaURL);
            EndpointAddress address = new EndpointAddress(tcpUri);
            BasicHttpBinding binding = new BasicHttpBinding();
            ObjModule.factory = new ChannelFactory<IArendaREST>(binding, address);

            Uri tcpUri2 = new Uri(Config.BillingURL);
            EndpointAddress address2 = new EndpointAddress(tcpUri2);
            BasicHttpBinding binding2 = new BasicHttpBinding();
            ObjModule.factory2 = new ChannelFactory<IBillingREST>(binding2, address2);

            Uri tcpUri3 = new Uri(Config.ReserveURL);
            EndpointAddress address3 = new EndpointAddress(tcpUri3);
            BasicHttpBinding binding3 = new BasicHttpBinding();
            ObjModule.factory3 = new ChannelFactory<IReserveREST>(binding3, address3);

            DataModule.Load();

            ObjModule.httpServer = new MyHttpServer(Config.PortalPort);
            ObjModule.thread = new Thread(new ThreadStart(ObjModule.httpServer.listen));
            ObjModule.thread.Start();

            Console.WriteLine("Portal started... Press Enter to exit");
            Console.ReadLine();

            ObjModule.httpServer.stop();
            ObjModule.thread.Abort();
                       

            /*
            try
            {                
                List<String> ids = channel.GetItems();
                foreach (var id in ids)
                {
                    ArendaItem item = channel.GetItemByID(id);
                    Console.WriteLine("{0} {1} {2} {3} {4} ", item.address, item.s, item.price, item.id, item.elite);
                }


                //String adr = (String)channel.GetAddressByID("23");
                //Console.WriteLine(adr);
                ArendaItem it2 = new ArendaItem();
                it2.address = "Нью-Васюки";
                it2.elite = false;
                it2.id = "33";
                bool res = channel.AddItem(it2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
             */

        }
    }
}
