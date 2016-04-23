using System;
using Cosmos.Core;
using Cosmos.HAL.PolarisGroup;
using Cosmos.System.Network;
using PolarisOS.Core;
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
        public static int nl = 0;
        private static readonly CPUExceptionHandler hLD = new CPUExceptionHandler();

        protected override void BeforeRun()
        {
            KeyLayout.SwitchKeyLayout(KeyLayout.KeyLayouts.QWERTZ);
            CPUExceptionHandler.Handle();
        }

        protected override void Run()
        {
            //Terminal.WrapRed($"{nameof(ITRMx86)}").White(":: ").Red($"{new ITRMx86().Open()}").newLine();
            nl = 0;
            nl = 0x5 / nl;
            Terminal.WrapRed("HALF");

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