using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Linq;

namespace ArendaRESTLib
{
    public class ArendaItem
    {
        public string id;
        public string city;
        public string address;
        public string roomtype;
        public bool elite;
        public int s;
        public int price;
        public string descr;
        public Guid guid;
    }

    [ServiceContract(Name = "ArendaREST")]
    public interface IArendaREST
    {
        [OperationContract]
        [WebGet(UriTemplate = ArendaRouting.GetItems, BodyStyle = WebMessageBodyStyle.Bare)]
            List<string> GetItems();
        [OperationContract]
        [WebGet(UriTemplate = ArendaRouting.GetItems, BodyStyle = WebMessageBodyStyle.Bare)]
        List<string> GetCityList();
        [OperationContract]
        [WebGet(UriTemplate = ArendaRouting.GetItemRoute, BodyStyle = WebMessageBodyStyle.Bare)]
            ArendaItem GetItemByID(string id);
        [OperationContract]
        [WebInvoke(UriTemplate = ArendaRouting.AddItemRoute, BodyStyle = WebMessageBodyStyle.Bare)]
            bool AddItem(ArendaItem item);
    }

    public static class ArendaRouting
    {
        public const string GetItemRoute = "/Item/{id}";
        public const string GetItems = "/ItemsAll";
        public const string AddItemRoute = "/AddItem";
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, 
        UseSynchronizationContext = false)]
    public class ArendaService : IArendaREST
    {
        //private static string DATAFILE = "datafile.xml";

        public static DataModule DM;

        private List<ArendaItem> store;

        public ArendaService()
        {
            store = DM.getRooms();
        }
        public ArendaItem GetItemByID(string id)
        {
            Console.WriteLine("req="+id);
            return store.Find((r) => r.id.Equals(id));
            
        }
        public bool AddItem(ArendaItem item)
        {
            Console.WriteLine("New item: {0} {1} {2}", item.address, item.id, item.elite);
            return true;
        }
        public List<string> GetItems()
        {
            List<string> res = new List<string>();
            store.ForEach((item) => res.Add(item.id));
            return res;
        }
        public List<string> GetCityList()
        {
            List<string> res = new List<string>();
            store.ForEach((item) => { if (res.IndexOf(item.city) == -1) res.Add(item.city); });
            return res;
        }
    }
}
