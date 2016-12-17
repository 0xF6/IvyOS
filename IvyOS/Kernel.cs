using System;
using System.Threading;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Ivy.HAL;
using IvyOS.IO;

namespace IvyOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public VFSBase myVFS;
        protected override void BeforeRun()
        {
            myVFS = new CosmosVFS();
            VFSManager.RegisterVFS(myVFS);
            myVFS.Initialize();
            UserSystem.Login("system", "010101");
        }
        protected override void Run()
        {
            Terminal.Fill(ConsoleColor.Cyan);
            while (true)
            {
                Thread.Sleep(2000);
                Terminal.WrapRed("Threading").White(" sleeping ").WrapCyan("2").White("second").Dot(true);
            }
        }
    }
}