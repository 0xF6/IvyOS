namespace PolarisOS.Core
{
    using System;
    using Cosmos.HAL.SolarisGroup;
    using Exception;

    // Red Screen of Death :D
    public class RSoD
    {
         /// <summary>
        /// Initiates a Redscreen.
        /// </summary>
        /// <param name="error">Error title or exception name</param>
        /// <param name="description">Error description</param>
        /// <param name="critical">Critical error?</param>
        public static void Init(
            string error = "Something went wrong!",
            string description = "Unknown exception",
            bool critical = false)
        {
            DrawOOPS();
            if (description.Length + 33 < Console.WindowHeight)
            {
                Console.CursorTop = 2; Console.CursorLeft = 33;
                ConsoleColor errcolor = ConsoleColor.White;
                if (critical) errcolor = ConsoleColor.Black;
                Terminal.Blue(error);
                Terminal.CursorTop = 4; Console.CursorLeft = 70;
                Terminal.White(description);
            }
            else
            {
                Console.CursorTop = 12; Console.CursorLeft = 2;
                ConsoleColor errcolor = ConsoleColor.White;
                if (critical) errcolor = ConsoleColor.Black;
                Terminal.Blue(error);
                Terminal.CursorTop = 14; Console.CursorLeft = 2;
                Terminal.White(description);
            }
            if (!critical)
            {
                Terminal.CursorTop = Terminal.WindowHeight - 1;
                Terminal.White("Press the [Enter]-key to resume").newLine();
                Terminal.CursorTop++;
                Terminal.ReadLine();
                Terminal.Cls();
            }
            else
            {
                Terminal.CursorTop = Terminal.WindowHeight - 4;
                Terminal.White("Press the [Enter]-key to shutdown").newLine();
                Terminal.CursorTop++;
                Terminal.White("If it doesn't work, press the RESET-button on your computer.").newLine();
                Terminal.CursorTop++;
                Terminal.ReadLine();
                // ACPI.Shutdown();
            }
        }
        public static void Init(KernelException ex, bool critical = false)
        {
            DrawOOPS();
            if (ex.Message.Length + 27 < Terminal.WindowHeight)
            {
                Terminal.CursorTop = 2; Terminal.CursorLeft = 27;
                ConsoleColor errcolor = ConsoleColor.White;
                if (critical) errcolor = ConsoleColor.Black;
                Terminal.White(ex.MethodBase);
                Terminal.CursorTop = 4; Terminal.CursorLeft = 70;
                Terminal.White(ex.Message);
            }
            else
            {
                Terminal.CursorTop = 12; Terminal.CursorLeft = 2;
                ConsoleColor errcolor = ConsoleColor.White;
                if (critical) errcolor = ConsoleColor.Black;
                Terminal.White(ex.MethodBase);
                Terminal.CursorTop = 14; Terminal.CursorLeft = 2;
                Terminal.White(ex.Message);
            }
            if (!critical)
            {
                Terminal.CursorTop = Terminal.WindowHeight - 3;
                Terminal.White("Press the [Enter]-key to resume");
                Terminal.CursorTop++;
                Terminal.ReadLine();
                Terminal.Cls();
            }
            else
            {
                Terminal.CursorTop = Terminal.WindowHeight - 4;
                Terminal.White("").AtColor("Press the [Enter]-key to shutdown.", ConsoleColor.White, true, false).newLine();
                Terminal.CursorTop++;
                Terminal.White("").AtColor("If it doesn't work, press the RESET-button on your computer.", ConsoleColor.White, true, false).newLine();
                Terminal.CursorTop++;
                Terminal.ReadLine();
                // ACPI.Shutdown();
            }
        }

        private static string[] arrOOPS = {
                "======  ======  =====  =====  =",
                "=    =  =    =  =   =  =      =",
                "=    =  =    =  =====  =====  =",
                "=    =  =    =  =          =   ",
                "======  ======  =      =====  ="};

        private static void DrawOOPS()
        {
            Terminal.Fill(ConsoleColor.DarkRed).setBackgroundColor(ConsoleColor.DarkRed);
           
            Terminal.CursorTop = 2;
            for (int i = 0; i != arrOOPS.Length; i++)
            {
                Terminal.CursorLeft += 2;
                Terminal.White(arrOOPS[i]).newLine();
            }
        }

        /// <summary>
        /// Kernel Panic
        /// </summary>
        public static void Panic()
        {
            Terminal.Cls().Fill(ConsoleColor.DarkRed);
            Terminal.CursorTop = 2;
            Terminal.White("KERNEL PANIC").newLine().
                White("CRITICAL KERNEL EXCEPTION").newLine().
                White("PLEASE CONTACT YOUR SOFTWARE MANUFACTURER");
            // Enter an infinite loop
            while (true) { }
        }
    }
}