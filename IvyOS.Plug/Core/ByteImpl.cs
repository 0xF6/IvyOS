namespace IvyOS.Core
{
    using Cosmos.IL2CPU.Plugs;

    [Plug(Target = typeof(global::System.Byte))]
    public class ByteImpl
    {
        public byte Parse(string toparse)
        {
            bool success = false;
            int tmp = 0;
            try { tmp = int.Parse(toparse); success = true; }
            catch { }
            if (success)
            {
                return tmp <= (int)byte.MaxValue ? (byte)tmp : (byte)0;
            }
            else return 0;
        }
    }
}