namespace IvyOS.Core
{
    using Cosmos.HAL;
    using Cosmos.IL2CPU.Plugs;

    [Plug(Target = typeof(System.Threading.Thread))]
    public class Thread
    {
        private static readonly Thread th = new Thread();
        public string get_Name() => "IVY_ROOT_THREAD";
        public static Thread get_CurrentThread() => th;

        public static void Sleep(int ms)
        {
            int second = ms / 1000;

            int start = RTC.Second; int end;
            if (start + second > 59) end = 0;
            else end = start + second;
            while (RTC.Second != end) { }
        }
    }
}