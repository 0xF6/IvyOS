namespace PolarisOS.Plugs
{
    using Cosmos.IL2CPU.Plugs;

    [Plug(Target = typeof(Cosmos.Core.INTs))]
    public class INTs
    {
        public static bool already = false;

        public static void HandleInterrupt_Default(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            API.HandleInt0x80(ref aContext);
        }
    }
}