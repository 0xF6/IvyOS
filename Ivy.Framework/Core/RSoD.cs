using Cosmos.HAL;

namespace Ivy.Framework.Core
{
    using System;
    using Ivy.HAL;

    // Red Screen of Death :D
    public class RSoD
    {
        
        /// <summary>
        /// Initiates a Redscreen.
        /// </summary>
        /// <param name="error">Error title or exception name</param>
        /// <param name="description">Error description</param>
        /// <param name="critical">Critical error?</param>
        public static void Push(string error = "Something went wrong!",string description = "Unknown exception", bool critical = false)
        {
            DrawSplash();
            if (description.Length + 33 < Console.WindowHeight)
            {
                Console.CursorTop = 2; Console.CursorLeft = 33;
                Terminal.Blue(error);
                Terminal.CursorTop = 4; Console.CursorLeft = 70;
                Terminal.White(description);
            }
            else
            {
                Console.CursorTop = 12; Console.CursorLeft = 2;
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

        public static void newLine()
        {
            Cosmos.System.Global.Console.Y++;
            Cosmos.System.Global.Console.X = 0;
            if (Cosmos.System.Global.Console.Y == Cosmos.HAL.Global.TextScreen.Rows)
            {
                Cosmos.HAL.Global.TextScreen.ScrollUp();
                Cosmos.System.Global.Console.Y = Cosmos.HAL.Global.TextScreen.Rows - 1;
                Cosmos.System.Global.Console.X = 0;
            }
            Cosmos.HAL.Global.TextScreen.SetCursorPos(Cosmos.System.Global.Console.X, Cosmos.System.Global.Console.Y);
        }

        public static void Push(KernelException ex, bool critical = true)
        {
            DrawSplash();
            if (ex.Message.Length + 27 < Terminal.WindowHeight)
            {
                Terminal.CursorTop = 2; Terminal.CursorLeft = 27;
                Terminal.White(ex.MethodBase);
                Terminal.CursorTop = 4; Terminal.CursorLeft = 70;
                Terminal.White(ex.Message);
            }
            else
            {
                Terminal.CursorTop = 12; Terminal.CursorLeft = 2;
                Terminal.White(ex.MethodBase ?? "MethodBase is null");
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
                Terminal.White("").newLine();
                if (ex.isSetContext)
                {
                    Terminal.White("\t\t\t")
                        .Magenta(nameof(ex.context.EAX)).White($" {ex.context.EAX} ")
                        .Magenta(nameof(ex.context.EBX)).White($" {ex.context.EBX} ")
                        .Magenta(nameof(ex.context.EBP)).White($" {ex.context.EBP} ")
                        .Magenta(nameof(ex.context.ECX)).White($" {ex.context.ECX} ")
                        .Magenta(nameof(ex.context.EDI)).White($" {ex.context.EDI} ")
                        .newLine();
                    Terminal.White("\t\t\t")
                        .Magenta(nameof(ex.context.EIP)).White($" {ex.context.EIP} ")
                        .Magenta(nameof(ex.context.ESI)).White($" {ex.context.ESI} ")
                        .Magenta(nameof(ex.context.ESP)).White($" {ex.context.ESP} ")
                        .Magenta(nameof(ex.context.EDX)).White($" {ex.context.EDX} ")
                        .Magenta(nameof(ex.context.CS)).White($" {ex.context.CS} ")
                        .Magenta(nameof(ex.context.EFlags)).White($" {(int)ex.context.EFlags} ")
                        .newLine();
                    Terminal.CursorTop = Terminal.WindowHeight - 2;

                }
                else
                    Terminal.CursorTop = Terminal.WindowHeight - 4;

                Terminal.White("").AtColor("Press the 'RESET'-button on restart u'r controller.", ConsoleColor.White, true, false).newLine();
                Terminal.ReadLine();
                // ACPI.Shutdown();
                while (true)
                {

                }
            }
        }

        /*
         

             */

        private static string[] arrOOPS = {
                "======  ======  =====  =====  =",
                "=    =  =    =  =   =  =      =",
                "=    =  =    =  =====  =====  =",
                "=    =  =    =  =          =   ",
                "======  ======  =      =====  ="};

        private static string[] arrSplash =
        {
            " _____         _              _      _ ",
            "/  ___|       | |            | |    | |",
            "\\ `--.  _ __  | |  __ _  ___ | |__  | |",
            " `--. \\| '_ \\ | | / _` |/ __|| '_ \\ | |",
            "/\\__/ /| |_) || || (_| |\\__ \\| | | ||_|",
            "\\____/ | .__/ |_| \\__,_||___/|_| |_|(_)",
            "       | |                             ",
            "       |_|                             "
        };
        
        private static void DrawOOPS()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Terminal.Fill(ConsoleColor.DarkRed);
            Terminal.CursorTop = 2;
            for (int i = 0; i != arrOOPS.Length; i++)
            {
                Terminal.CursorLeft += 2;
                Terminal.White(arrOOPS[i]).newLine();
            }
        }
        private static void DrawSplash()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Terminal.Fill(ConsoleColor.DarkGreen);

            Terminal.CursorTop = 2;
            for (int i = 0; i != arrSplash.Length; i++)
            {
                Terminal.CursorLeft += 2;
                Terminal.White(arrSplash[i]);
                Terminal.White("\0");
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