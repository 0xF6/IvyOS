using System;
using Cosmos.System.Network;
using PolarisOS.Driver.Graphics.VMWare;
using PolarisOS.HAL;
using PolarisOS.HAL.KeyBoard;

namespace PolarisOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public static string CurrentUser = "";
        public static bool connected = false;
        public static UdpClient client = new UdpClient();
        public static bool nonchoix = false;
        

        protected override void BeforeRun()
        {
            KeyLayout.SwitchKeyLayout(KeyLayout.KeyLayouts.QWERTZ);
        }

        protected override void Run()
        {
            Display.Setup(new Display.SetupInfo()
            {
                depth = DepthValue.x256,
                depthColor = 255,
                isClear = true,
                resolution = ScreenSize.v1024x768
            });

            while (true)
            {
                Console.WriteLine("->");
                string s = Console.ReadLine();
                Console.WriteLine(s);
            }
        }
    }
}