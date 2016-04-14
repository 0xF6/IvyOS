using Cosmos.System.FileSystem;
using Cosmos.System.Network;

namespace PolarisOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public static string CurrentUser = "";
        public static bool connected = false;
        public static UdpClient client = new UdpClient();
        public static bool nonchoix = false;

        protected override void Run()
        {
            
        }
    }
}