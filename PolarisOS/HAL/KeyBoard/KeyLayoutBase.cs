namespace PolarisOS.HAL.KeyBoard
{
    using Cosmos.HAL;

    public abstract class KeyLayoutBase : ScanMapBase
    {
        private static uint KeyCount;

        protected void AddKeyWithShift(uint p, char p_2, ConsoleKeyEx p_3)
        {
            AddKey(p, p_2, p_3);
            AddKey(p << 16, p_2, p_3);
        }
        protected void AddKey(uint p, char p_2, ConsoleKeyEx p_3)
        {
            _keys.Add(new KeyMapping(p, p_2, p_3));
            KeyCount += 1u;
        }

        protected void AddKey(uint p, ConsoleKeyEx p_3) => AddKey(p, '\0', p_3);
        protected void AddKeyWithShift(uint p, ConsoleKeyEx p_3) => AddKeyWithShift(p, '\0', p_3);
    }
}