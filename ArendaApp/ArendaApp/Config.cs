using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArendaApp
{
    public class Config
    {
        public static string ServiceURL ;
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
}
