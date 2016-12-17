namespace IvyOS.Core
{
    using Cosmos.HAL;
    using Cosmos.IL2CPU.Plugs;

    [Plug(Target = typeof(System.Threading.Thread))]
    public class Thread
    {
        public string Name
        {
            get { return "IVY_ROOT_THREAD"; }
            set { }
        }

        public static Thread CurrentThread { get; } = new Thread();

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