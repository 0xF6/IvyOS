using System.Text;

namespace PolarisOS.IO
{
    public class BinaryWriter
    {
        private readonly Stream _baseStream;

        public Stream BaseStream => _baseStream;



        public BinaryWriter(Stream stream)
        {
            _baseStream = stream;
        }
        public void Write(byte dat) => BaseStream.Write(dat);

        public void Write(short dat)
        {
            byte[] data = System.BitConverter.GetBytes(dat);
            foreach (byte t in data)
                Write(t);
        }

        public void Write(int dat)
        {
            byte[] data = System.BitConverter.GetBytes(dat);
            foreach (byte t in data)
                Write(t);
        }

        public void Write(long dat)
        {
            byte[] data = System.BitConverter.GetBytes(dat);
            foreach (byte t in data)
                Write(t);
        }

        public void Write(ushort dat)
        {
            byte[] data = System.BitConverter.GetBytes(dat);
            foreach (byte t in data)
                Write(t);
        }

        public void Write(uint dat)
        {
            byte[] data = System.BitConverter.GetBytes(dat);
            foreach (byte t in data)
                Write(t);
        }

        public void Write(ulong dat)
        {
            byte[] data = System.BitConverter.GetBytes(dat);
            foreach (byte t in data)
                Write(t);
        }

        public void Write(byte[] data) { foreach (byte t in data) Write(t); }


        public void Write(bool dat)
        {
            if (dat)
                Write((byte) 1);
            Write((byte)0);
        }


        public void Write(string dat, byte numEncoding)
        {
            Write(numEncoding);
            Write((ushort)dat.Length);
            if (numEncoding == 0x0)
            {
                foreach (byte b in dat)
                    Write(b);
                return;
            }
            if (numEncoding == 0xf0) // utf-8
            {
                byte[] bytes = Encoding.UTF8.GetBytes(dat);
                Write(bytes);
                return;
            }
            if (numEncoding == 0xfa) // utf-32
            {
                byte[] bytes = Encoding.UTF32.GetBytes(dat);
                Write(bytes);
                return;
            }
            if (numEncoding == 0xfb) // ascii
            {
                byte[] bytes = Encoding.ASCII.GetBytes(dat);
                Write(bytes);
                return;
            }
        }
    }
}