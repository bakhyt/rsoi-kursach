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

namespace ArendaApp
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Config.Load();

            ArendaService.DM = new DataModule();
            if (!ArendaService.DM.Connect(Config.Server, Config.Base, Config.User, Config.Pass))
            {
                Console.WriteLine("Cant connect to MSSQL by {0} {1} {2} {3}",
                    Config.Server, Config.Base, Config.User, Config.Pass);
                Console.ReadLine();
            }
            else
            {
                ArendaService srv = new ArendaService();
                ServiceHost _serviceHost = new ServiceHost(typeof(ArendaService), new Uri(Config.ServiceURL));
                _serviceHost.AddServiceEndpoint(typeof(IArendaREST), new BasicHttpBinding(), "");
                _serviceHost.Open();
                Console.WriteLine("Service arenda start");
                Console.ReadLine();
                _serviceHost.Close();
            }           
            
        }
    }
}
