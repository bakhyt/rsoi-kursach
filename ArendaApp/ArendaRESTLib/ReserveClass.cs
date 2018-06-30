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

    [ServiceContract(Name = "ReserveREST")]
    public interface IReserveREST
    {
        [OperationContract]
        [WebGet(UriTemplate = ReserveRouting.IsRoomReserved, BodyStyle = WebMessageBodyStyle.Bare)]
        bool IsRoomReserved(string userid, string roomid);
        [OperationContract]
        [WebInvoke(UriTemplate = ReserveRouting.DoReserveRoom, BodyStyle = WebMessageBodyStyle.Bare)]
        bool DoReserveRoom(string userid, string roomid);
        
    }

    public static class ReserveRouting
    {
        public const string IsRoomReserved = "/Reserved/{id1}/{id2}";
        public const string DoReserveRoom = "/DoReserve";
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true,
        UseSynchronizationContext = false)]
    public class ReserveService : IReserveREST
    {
        public static DataModule DM;

        public ReserveService()
        {
        }

        public bool IsRoomReserved(string userid, string roomid)
        {
            return DM.IsRoomReserved(userid, roomid);
        }

        public bool DoReserveRoom(string userid, string roomid)
        {
            return DM.DoReserveRoom(userid, roomid);
        }
    }
}
