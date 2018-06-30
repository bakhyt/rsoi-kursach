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

    [ServiceContract(Name = "BillingREST")]
    public interface IBillingREST
    {
        [OperationContract]
        [WebGet(UriTemplate = BillingRouting.GetAccountRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        int GetAccountID(string userid);
        [OperationContract]
        [WebInvoke(UriTemplate = BillingRouting.IncSumRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        bool IncSum(string userid, int sum);
        [WebInvoke(UriTemplate = BillingRouting.DecSumRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        bool DecSum(string userid, int sum);
    }

    public static class BillingRouting
    {
        public const string GetAccountRoute = "/Accounts/{id}";
        public const string IncSumRoute = "/IncSum";
        public const string DecSumRoute = "/DecSum";
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true,
        UseSynchronizationContext = false)]
    public class BillingService : IBillingREST
    {
        public static DataModule DM;

        public BillingService()
        {
        }

        public int GetAccountID(string userid)
        {
            return DM.getAccountSum(userid);
        }
        public bool IncSum(string userid, int sum)
        {
            return DM.incAccountSum(userid, sum);
        }
        public bool DecSum(string userid, int sum)
        {
            return DM.decAccountSum(userid, sum);
        }        
    }
}
