using ArendaRESTLib;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using System;

namespace ReserveApp
{
    public class Config
    {
        public static string ServiceURL;
        public static string Server;
        public static string Base;
        public static string User;
        public static string Pass;
        public static void Load()
        {
            XDocument doc = XDocument.Load("config.xml");
            var data = doc.Element("config").Element("service");
            ServiceURL = data.Element("url").Value;
            data = doc.Element("config").Element("database");
            Server = data.Element("server").Value;
            Base = data.Element("base").Value;
            User = data.Element("user").Value;
            Pass = data.Element("pass").Value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Config.Load();

            ReserveService.DM = new DataModule();
            if (!ReserveService.DM.Connect(Config.Server, Config.Base, Config.User, Config.Pass))
            {
                Console.WriteLine("Cant connect to MSSQL by {0} {1} {2} {3}",
                    Config.Server, Config.Base, Config.User, Config.Pass);
                Console.ReadLine();
            }
            else
            {
                ReserveService srv = new ReserveService();
                ServiceHost _serviceHost = new ServiceHost(typeof(ReserveService), new Uri(Config.ServiceURL));
                _serviceHost.AddServiceEndpoint(typeof(IReserveREST), new BasicHttpBinding(), "");
                _serviceHost.Open();
                Console.WriteLine("Service reserve start");

                Console.ReadLine();
                _serviceHost.Close();
            }
        }
    }
}
