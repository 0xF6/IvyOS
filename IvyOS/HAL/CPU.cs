namespace IvyOS.HAL
{
    public static class CPU
    {
        public static void Reboot() => Cosmos.HAL.Power.Reboot();
        public static void Wait(uint milliseconds)
        {
            Cosmos.HAL.PIT pit = new Cosmos.HAL.PIT();
            pit.Wait(milliseconds);
        }
        public static void WaitSecs(int seconds) => Wait((uint)seconds * 1000);
    }
}