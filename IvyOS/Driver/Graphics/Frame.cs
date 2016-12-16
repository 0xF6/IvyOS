namespace IvyOS.Driver.Graphics
{
    public class Frame
    {
        /*
        * Note: frame, is pretty much a Image/Bitmap, and we use it as one. Or you could say it is
        * a mini screen.
        */

        /// <summary>
        /// Makes up the frame.
        /// </summary>
        public uint[] Data;
        public int Height;
        public int Width;
        /// <summary>
        /// A new frame.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Frame(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Data = new uint[(Height + 1) * (Width + 1)];
        }
        public Frame(uint width, uint height)
        {
            this.Width = (int)width;
            this.Height = (int)height;
            this.Data = new uint[(this.Height + 1) * (this.Width + 1)];
        }
        Frame()
        {
            this.Width = 0;
            this.Height = 0;
            this.Data = new uint[(this.Height + 1) * (this.Width + 1)];
        }
        /*
         * This is used for setting and getting pixels. Very confusing don't ask.
         * uint n is the pixel number and there is compicated ways of getting p and
         * anyway ill be using this way when I work with the buffer in the screen handler.
         */

        /// <summary>
        /// GetPixel method.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public uint GetPixel(uint n)
        {
            return this.Data[n];
        }
        public int GetPixel(int n)
        {
            return (int)this.Data[n];
        }
        /// <summary>
        /// SetPixel method.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="n"></param>
        public void SetPixel(uint c, uint n)
        {
            this.Data[n] = c;
        }
        public void SetPixel(int c, int n)
        {
            this.Data[n] = (uint)c;
        }
    }
}