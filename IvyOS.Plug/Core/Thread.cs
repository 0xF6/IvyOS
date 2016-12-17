namespace IvyOS.Core
{
    using Cosmos.HAL;
    using Cosmos.IL2CPU.Plugs;

    [Plug(Target = typeof(System.Threading.Thread))]
    public class Thread
    {
        public static void Sleep(int millisecondsTimeout)
        {
            int second = millisecondsTimeout/1000;

            int start = RTC.Second; int end;
            if (start + second > 59) end = 0;
            else end = start + second;
            while (RTC.Second != end) { }
        }
    }
}