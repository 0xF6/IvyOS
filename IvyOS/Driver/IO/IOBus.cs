namespace IvyOS.Driver.IO
{
    using System;

    public abstract class IOBus
    {
        //TODO: Reads and writes can use this to get port instead of argument
        protected static void Write8(UInt16 aPort, byte aData) { } // Plugged
        protected static void Write16(UInt16 aPort, UInt16 aData) { } // Plugged
        protected static void Write32(UInt16 aPort, UInt32 aData) { } // Plugged

        protected static byte Read8(UInt16 aPort) { return 0; } // Plugged
        protected static UInt16 Read16(UInt16 aPort) { return 0; } // Plugged
        protected static UInt32 Read32(UInt16 aPort) { return 0; } // Plugged
    }
}