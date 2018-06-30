using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portal
{
    class Config
    {
        public static string ArendaURL ;
        public static string ReserveURL;
        public static string BillingURL;
        public static int PortalPort;
        public static string Server;
        public static string Base;
        public static string User;
        public static string Pass;
        public static void Load()
        {
            XDocument doc = XDocument.Load("config.xml");
            foreach(var data in doc.Element("config").Element("services").Elements("service")) {
                if (data.Element("type").Value.Equals("arenda")) ArendaURL = data.Element("url").Value;
                if (data.Element("type").Value.Equals("reserve")) ReserveURL = data.Element("url").Value;
                if (data.Element("type").Value.Equals("billing")) BillingURL = data.Element("url").Value;
            }
            PortalPort = Int32.Parse(doc.Element("config").Element("portal").Element("port").Value) ;
            var datab = doc.Element("config").Element("database");
            Server = datab.Element("server").Value;
            Base = datab.Element("base").Value;
            User = datab.Element("user").Value;
            Pass = datab.Element("pass").Value;
        }
    }
}
