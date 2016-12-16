namespace IvyOS.IO
{
    public unsafe class VideoStream
    {
        private readonly MemoryStream stream;

        public VideoStream()
        {
            stream = new MemoryStream((byte*)0xB8000); // Set adress video point
        }

        public byte ReadByte(uint p) => stream.ReadByte(p);
        public void WriteByte(uint p, byte b) => stream.WriteByte(p, b);
    }
}