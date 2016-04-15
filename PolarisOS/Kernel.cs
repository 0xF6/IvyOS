using System;
using Cosmos.System.Network;
using PolarisOS.Driver.Graphics.VMWare;

namespace PolarisOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public static string CurrentUser = "";
        public static bool connected = false;
        public static UdpClient client = new UdpClient();
        public static bool nonchoix = false;
        private static SVGADriver vga;

        protected override void Run()
        {
            vga = new SVGADriver();
            vga.SetMode(ScreenSize.v1024x768, 256);
            
            while (true)
            {
                Console.WriteLine("->");
                string s = Console.ReadLine();
                Console.WriteLine(s);
            }
        }
    }
}