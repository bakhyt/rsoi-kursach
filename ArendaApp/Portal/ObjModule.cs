using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ServiceModel;
using ArendaRESTLib;

namespace Portal
{
    class ObjModule
    {
        public static MyHttpServer httpServer;
        public static Thread thread;
        public static ChannelFactory<IArendaREST> factory;
        public static IArendaREST channel;
        public static ChannelFactory<IBillingREST> factory2;
        public static IBillingREST channel2;
        public static ChannelFactory<IReserveREST> factory3;
        public static IReserveREST channel3;
        
        public static string errmsg;
        public static bool OpenChannel()
        {
            try
            {
                channel = factory.CreateChannel();
                return true;
            }
            catch(Exception e) {
                errmsg = e.Message;
                return false;
            }
        }
        public static void CloseChannel()
        {
            factory.Close();
        }
        public static bool OpenChannel2()
        {
            try
            {
                channel2 = factory2.CreateChannel();
                return true;
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return false;
            }
        }
        public static void CloseChannel2()
        {
            factory2.Close();
        }

        public static bool OpenChannel3()
        {
            try
            {
                channel3 = factory3.CreateChannel();
                return true;
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return false;
            }
        }
        public static void CloseChannel3()
        {
            factory3.Close();
        }
    }
}
