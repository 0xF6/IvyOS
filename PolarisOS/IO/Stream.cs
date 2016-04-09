using System;

namespace PolarisOS.IO
{
    public abstract class Stream
    {
        public bool canRead;
        public bool canWrite;
        public uint Position;


        public byte Read()
        {
            if (!canRead) throw new Exception("Can not read!");
            uint tmp = Position;
            Position++;
            return ReadByte(tmp);
        }
        public void Write(byte b)
        {
            if (!canWrite) throw new Exception("Can not Write!");
            uint tmp = Position;
            Position++;
            WriteByte(tmp, b);
            throw new Exception("Can not Write!");
        }

        public abstract void Flush();

        public void Close() => Flush();

        internal abstract byte ReadByte(uint p);
        internal abstract void WriteByte(uint p, byte b);
    }
}