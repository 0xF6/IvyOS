using System;
using Ivy.Framework.Core;
using Ivy.HAL;

namespace Ivy.Framework.Graphic
{
    using System.Threading;

    public class BootScreen
    {
        public static UInt16 Rows => 25; // Вниз
        public static UInt16 Cols => 80; // Вбок


        public static void Boot()
        {
            Thread.Sleep(4000);
            var pb = new ProgressBar();
            do
            {
                pb.Increment(5);
                Thread.Sleep(1000);
            }
            while (pb.Value < 100);
            Thread.Sleep(1000);
            pb.DrawComplete();
            Thread.Sleep(5000);
            Console.Clear();
            Console.CursorVisible = false;
            RenderUI();
        }

        private static void RenderUI()
        {
            string box = $"[{UserSystem.ActiveUser.Name}]({UserSystem.ActiveUser.Rank.ToName()})";
            Console.CursorLeft = (80 - (box.Length + 2));
            Terminal.WrapRed(UserSystem.ActiveUser.Name)
                .White("(")
                .Cyan(UserSystem.ActiveUser.Rank.ToName())
                .White(")");
            Console.CursorLeft = 0;
            Terminal.WrapRed("Ivy").White("Shell kernel booted!").newLine();
            Terminal.WrapRed("Ivy").White("All system component is ").Green("stable").White("!").newLine();
        }

        public class ProgressBar
        {
            private int value = 0;
            public int Value
            {
                get { return value; }
                set
                {
                    if (value >= 0 && value <= 100)
                    {
                        this.value = value;
                    }
                }
            }

            /// <summary>
            /// Initialize a new ProgressBar
            /// </summary>
            /// <param name="startValue">Value</param>
            public ProgressBar(int startValue = 0)
            {
                this.Value = startValue;
                this.Refresh();
            }
            public void Increment(int i = 1)
            {
                this.Value += i;
                this.Refresh();
            }
            public void Decrement()
            {
                this.Value--;
                this.Refresh();
            }
            /// <summary>
            /// INFO: MaxValue is 100 and MinValue is 0.
            /// </summary>
            /// <param name="value"></param>
            public void Draw()
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.CursorTop = 10;
                Console.CursorLeft = 0;

                int ct = 10;
                int cl = 15;

                string[] bufferTop =
                {                  $"{new string(' ', 13)}   _-------------------= ",$"Ivy OS",$" =-------------------_" };
                string buffer = $"{new string(' ', 13)}-=[                                                  ]=- ";
                string bufferBot = $"{new string(' ', 13)}   -_________________-= Loading... =-_______________-";

                Terminal.White(bufferTop[0]).Cyan(bufferTop[1]).White(bufferTop[2]).newLine();
                Console.Write(buffer);
                Console.CursorLeft = cl + 1;

                for (int i = 0; i < (this.value / 2) - 1; i++)
                    if (this.value / 2 <= 50) Terminal.Red("=");


                Console.CursorLeft = cl + 54;
                Console.WriteLine(" " + this.value + "%");
                Console.Write(bufferBot);
                Console.CursorTop = ct;
                Console.CursorLeft = cl;
            }

            public void DrawComplete()
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.CursorTop = 10;
                Console.CursorLeft = 0;

                int ct = 10;
                int cl = 15;
                string[] bufferTop =
                {                  $"{new string(' ', 13)}   _-------------------= ",$"Ivy OS",$" =-------------------_" };
                string buffer = $"{new string(' ', 13)}-=[                                                  ]=- ";
                string bufferBot = $"{new string(' ', 13)}   -_________________-= Deploy... =-_______________-";
                Terminal.White(bufferTop[0]).Cyan(bufferTop[1]).White(bufferTop[2]).newLine();
                Console.Write(buffer);
                Console.CursorLeft = cl + 1;

                Terminal.Red(new string('=', "                                                 ".Length));
                Console.CursorLeft = cl + 54;
                Console.WriteLine(" " + 100 + "%");
                Console.Write(bufferBot);
            }
            private void Refresh() => this.Draw();
        }
    }
}