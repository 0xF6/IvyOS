namespace IvyOS.Threading
{
    using Cosmos.HAL;

    public class Threasd
    {
        public static void SleepTicks(int value)
        {
            for (int i = 0; i < value; i++) {; }
            return;
        }
        public static void SleepSeconds(int value)
        {
            int start = RTC.Second; int end;
            if (start + value > 59) end = 0;
            else end = start + value;
            while (RTC.Second != end) {; }
        }
    }
}