using System;
using Cosmos.Core;
using Cosmos.Debug.Kernel;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Ivy.HAL;
using Cosmos.System.Network;
using IvyOS.Core;
using IvyOS.Driver.Graphics.VMWare;
using IvyOS.HAL;
using IvyOS.HAL.KeyBoard;
using IvyOS.IO;

namespace IvyOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public static Debugger dKernel = new Debugger("User", "Kernel");
        public static string CurrentUser = "";
        public static bool connected = false;
        public static UdpClient client = new UdpClient();
        public static bool nonchoix = false;
        public static int nl = 0;
        public VFSBase myVFS;
        protected override void BeforeRun()
        {
            KeyLayout.SwitchKeyLayout(KeyLayout.KeyLayouts.QWERTZ);
            myVFS = new CosmosVFS();
            VFSManager.RegisterVFS(myVFS);
            myVFS.Initialize();
            UserSystem.Login("system", "010101");
        }
        
        private uint t = 0;
        protected override void Run()
        {
            int o = 1;

            int p = 8 - (9 -1);

            int u = o/p;
            Display.Setup(new Display.SetupInfo() { depth = DepthValue.x256, resolution = ScreenSize.v1600x768});
            DrawFrame(Resources.SuIco, 64, 64, 0/*(1024 / 2) - 64*/, 0/*(768 / 2) - 64*/);
            while (true)
            {

            }
        }

        private static int i = 0;
        private static int ts = 0;
        private static int count = 0;

        public void DrawFrame(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            count = 0;
            for (i = 0; i < length; i++)
                for (ts = 0; ts < width; ts++, count++)
                    if (Arr[count] != 0xff00ff || Arr[count] != 0x0)
                        Display.SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), (int)Arr[count]);
        }
    }
}