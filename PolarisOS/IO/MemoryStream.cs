namespace PolarisOS.IO
{
    public unsafe class MemoryStream
    {
        private bool eof;
        private uint length;

        private readonly byte* data;

        public bool EOF => eof;

        public MemoryStream(byte* dat)
        {
            eof = false;
            length = sizeof(uint);
            data = dat;
        }
        public MemoryStream(byte[] dat)
        {
            eof = false;
            length = (uint)dat.Length;
            fixed (byte* ptr = dat)
            {
                data = ptr;
            }
        }
        internal byte ReadByte(uint p)
        {
            eof = p > length;
            return !eof ? data[p] : byte.MinValue;
        }

        internal void WriteByte(uint p, byte b)
        {
            eof = p > length;
            if (!eof) data[p] = b;
        }
    }
}