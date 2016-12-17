namespace Ivy.Framework.Driver
{
    using Cosmos.Core;

    public class IRQSafeContext
    {
        public unsafe INTs.MMXContext* MMXContext;
        public uint EDI;
        public uint ESI;
        public uint EBP;
        public uint ESP;
        public uint EBX;
        public uint EDX;
        public uint ECX;
        public uint EAX;
        public uint Interrupt;
        public uint Param;
        public uint EIP;
        public uint CS;
        public INTs.EFlagsEnum EFlags;
        public uint UserESP;
    }
}