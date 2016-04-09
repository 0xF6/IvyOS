namespace PolarisOS.Driver.IO
{
    public sealed class IOPort : Cosmos.Core.IOPortBase
    {
        private byte Byte
        {
            get { return Read8(this.Port); }
            set { Write8(this.Port, value); }
        }

        private ushort Word
        {
            get { return Read16(this.Port); }
            set { Write16(this.Port, value); }
        }

        private uint DWord
        {
            get { return Read32(this.Port);  }
            set { Write32(this.Port, value); }
        }

        private IOPort(ushort aPort) : base(aPort) { }

        /// <summary>
        /// Write to an unused port.
        /// </summary>
        /// <remarks>
        /// This assures whatever we were waiting on for a previous
        /// IO read/write has completed.
        /// Port 0x80 is unused after BIOS POST.
        /// 0x22 is just a random byte.
        /// Since IO is slow - its just a dummy sleep to wait long enough for the previous operation
        /// to have effect on the target.
        /// </remarks>
        public static void Wait() => Write8(0x80, 0x22);


        /// <summary>
        /// Reads a byte
        /// </summary>
        public static byte inb(ushort port) => new IOPort(port).Byte;
        /// <summary>
        /// Reads a 16 bit word
        /// </summary>
        public static ushort inw(ushort port) => new IOPort(port).Word;
        /// <summary>
        /// Reads a 32 bit word
        /// </summary>
        public static uint inl(ushort port) => new IOPort(port).DWord;
        /// <summary>
        /// Writes a byte
        /// </summary>
        public static void outb(ushort port, byte data) => new IOPort(port).Byte = data;
        /// <summary>
        /// Writes a 16 bit word
        /// </summary>
        public static void outw(ushort port, ushort data) => new IOPort(port).Word = data;
        /// <summary>
        /// Writes a 32 bit word
        /// </summary>
        public static void outl(ushort port, uint data) => new IOPort(port).DWord = data;
    }
}