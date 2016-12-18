using System;
using System.Threading;
using Cosmos.Core;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Ivy.Framework;
using Ivy.Framework.Core;
using Ivy.Framework.Driver;
using Ivy.Framework.Graphic;
using Ivy.Framework.HAL;
using Ivy.HAL;
using IvyOS.IO;

namespace IvyOS
{
    public class Kernel : Cosmos.System.Kernel
    {



        public VFSBase VFS;



        private void Level_1()
        {
            // Remove Cursor
            AXP.Outb(0x3D4, 14);
            AXP.Outb(0x3D5, 0x07);
            AXP.Outb(0x3D4, 15);
            AXP.Outb(0x3D5, 0xD0);
            //
        }

        private void Level_2()
        {
            ACPI.Enable();
            PIT.Beep(1111);
        }

        private void Level_3()
        {
            VFSManager.RegisterVFS((VFS = new IvyVFS()));
        }
        private void Level_4()
        {
            UserSystem.Login("System", "010101");
            Thread.Sleep(1000);
            try
            {
                //while (true)
                //{
                //    Thread.Sleep(1000);
                //    Terminal.setFAIL().Blue("sss").Red("ssaad").newLine();
                //}
                //Thread.Sleep(2000);
                //BootScreen.Boot();
                //Thread.Sleep(2000);
                throw new Exception("Yuuki pidor!");
            }
            catch (Exception e)
            {
                KernelException ess = new KernelException(e.Message + "\n       IL Exception", new IRQSafeContext());
                RSoD.Push(ess, true);
            }

            while (true)
            { }
        }

        #region Kernel Event
        protected override void BeforeRun()
        {
            Level_1();
            Level_2();
            Level_3();
        }
        protected override unsafe void Run()
        {
            Console.Clear();
            // Fill background
            
            //
            while (true)
            {
                Console.WriteLine("test!");
                Thread.Sleep(1000);
            }

            return;


            Level_4();
            return;
            Terminal.Fill(ConsoleColor.Cyan);
            while (true)
            {
                Thread.Sleep(2000);
                Terminal.WrapRed("Threading").White(" sleeping ").WrapCyan("2").White("second").Dot(true);
            }
        }
        #endregion
    }
}