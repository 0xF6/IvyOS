using System;
using System.Drawing;
using System.Threading;
using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Ivy;
using Ivy.Framework;
using Ivy.Framework.Core;
using Ivy.Framework.Driver;
using Ivy.Framework.Graphic;
using Ivy.Framework.HAL;
using Ivy.HAL;
using IvyOS.IO;
using Console = System.Console;
using Pen = Cosmos.System.Graphics.Pen;
using PIT = Ivy.Framework.Driver.PIT;
using VGAScreen = Cosmos.HAL.VGAScreen;

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
            //ACPI.Enable();
        }

        private void Level_3()
        {
            //VFSManager.RegisterVFS((VFS = new IvyVFS()));
        }
        private void Level_4()
        {
            UserSystem.Login("System", "010101");
            Thread.Sleep(1000);
            try
            {
                BootScreen.Boot();
            }
            catch (Exception e)
            {
                KernelException ess = new KernelException(e.Message + "\n       IL Exception", new IRQSafeContext());
                RSoD.Push(ess, true);
            }
        }

        #region Kernel Event
        protected override void BeforeRun()
        {
            Level_1();
            Level_2();
            Level_3();
            screen = new VGAScreen();

            //Level_4();
        }

        private uint index = 0;
        public void WriteLine(string s)
        {
            foreach (var c in s)
            {
                //vbe.SetVRAM(index, (byte)c);
                //vbe.SetVRAM(++index, (byte)((uint)(ConsoleColor.White) | ((uint)(ConsoleColor.Black) << 4)));
                screen.SetPixel720x480x4(0, index++, 200);
            }
        }

        public static VBEDriver vbe;
        public static Cosmos.HAL.VGAScreen screen;
        public static PCSpeaker Speaker = new PCSpeaker();

        public static void Beep()
        {
            bp();
            Thread.Sleep(1000);
            Speaker.nosound();
            Thread.Sleep(1000);
        }
        private static uint x = 1000;

        public static void bp()
        {
            UInt32 div = 1193180;
            Speaker.playSound((x = +2000));
            Terminal.WrapRed("PIT").White($"p42-[0x{(byte)div}, 0x{(byte)div / x}]").newLine();
            BaseIOGroups.PCSpeaker.p42.Byte = (byte)div;
            //PIT.PITFrequency = 1193180;
        }
        
        protected override unsafe void Run()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Thread.Sleep(1000);
            while (true)
            {
                Beep();
            }
            return;
            for(int i = 0; i != 10; i++) Console.WriteLine("test!");
            Thread.Sleep(5000);
            Cosmos.HAL.Global.TextScreen.Background = ConsoleColor.DarkRed;
            Cosmos.HAL.Global.TextScreen.Foreground = ConsoleColor.Black;
            Cosmos.HAL.Global.TextScreen.SetColors(ConsoleColor.Black, ConsoleColor.DarkRed);
            Cosmos.HAL.Global.TextScreen.Clear();
            while (true)
            {
                Console.WriteLine("test!");
                Thread.Sleep(1000);
            }
        }
        #endregion
    }
}