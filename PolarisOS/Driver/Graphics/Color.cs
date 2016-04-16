namespace PolarisOS.Driver.Graphics
{
    public class Color
    {
        /// <summary>
        /// Empty color
        /// </summary>
        public static readonly Color Null = new Color(0, 0, 0, 0);
        /// <summary>
        /// The Index of this Color.
        /// </summary>
        public int Index = 0;
        /// <summary>
        /// The Alpha Value Of This Color
        /// </summary>
        public byte Alpha = 0;
        /// <summary>
        /// The Red Value Of This Color
        /// </summary>
        public byte Red = 0;
        /// <summary>
        /// The Green Value Of This Color
        /// </summary>
        public byte Green = 0;
        /// <summary>
        /// The Blue Value Of This Color
        /// </summary>
        public byte Blue = 0;
        /// <summary>
        /// A Blank new Color.
        /// </summary>
        public Color()
        {
            this.Alpha = 0;
            this.Red = 0;
            this.Green = 0;
            this.Blue = 0;
        }
        /// <summary>
        /// A New Color With Alpha
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public Color(byte A, byte R, byte G, byte B)
        {
            this.Alpha = A;
            this.Red = R;
            this.Green = G;
            this.Blue = B;
            //This color has no index, find it.
        }
        /// <summary>
        /// A New Color Without Alpha
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public Color(byte R, byte G, byte B)
        {
            this.Alpha = 255;
            this.Red = R;
            this.Green = G;
            this.Blue = B;
            //This color has no index, find it.
        }
        /// <summary>
        /// A New Color With Alpha
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public Color(int Index, byte A, byte R, byte G, byte B)
        {
            this.Alpha = A;
            this.Red = R;
            this.Green = G;
            this.Blue = B;
            this.Index = Index;
        }
        /// <summary>
        /// A New Color Without Alpha
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        public Color(int Index, byte R, byte G, byte B)
        {
            this.Alpha = 255;
            this.Red = R;
            this.Green = G;
            this.Blue = B;
            this.Index = Index;
        }
    }
}