using System;
using System.Threading;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Ivy.Framework.HAL;
using Ivy.HAL;
using IvyOS.IO;

namespace IvyOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public VFSBase VFS;
        protected override void BeforeRun()
        {
            VFS = new IvyVFS();
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize();
            UserSystem.Login("system", "010101");

            ACPI.Enable();
        }
        protected override unsafe void Run()
        {
            return;
            Terminal.Fill(ConsoleColor.Cyan);
            while (true)
            {
                Thread.Sleep(2000);
                Terminal.WrapRed("Threading").White(" sleeping ").WrapCyan("2").White("second").Dot(true);
            }
        }
    }
}