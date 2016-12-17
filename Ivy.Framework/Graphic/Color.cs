namespace Ivy.Framework.Graphic
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

        public class HtmlColors
        {
            #region Html Colors
            public static byte[] Black = { 0, 0, 0 };
            public static byte[] White = { 255, 255, 255 };
            public static byte[] Gray = { 128, 128, 128 };
            public static byte[] Magenta = { 255, 0, 255 };
            public static byte[] Purple = { 160, 32, 240 };
            public static byte[] DarkPurple = { 57, 48, 88 };
            public static byte[] Red = { 255, 0, 0 };
            public static byte[] LightGray = { 192, 192, 192 };
            public static byte[] Green = { 34, 177, 76 };
            public static byte[] Lime = { 0, 255, 0 };
            public static byte[] DarkGray = { 90, 90, 90 };
            public static byte[] Yellow = { 255, 255, 0 };
            public static byte[] AliceBlue = { 240, 248, 255 };
            public static byte[] AntiqueWhite = { 250, 235, 215 };
            public static byte[] Aquamarine = { 127, 255, 212 };
            public static byte[] Azure = { 240, 255, 255 };
            public static byte[] Beige = { 245, 245, 220 };
            public static byte[] Bisque = { 255, 228, 196 };
            public static byte[] BlanchedAlmond = { 255, 235, 205 };
            public static byte[] Blue = { 0, 0, 255 };
            public static byte[] BlueViolet = { 138, 43, 226 };
            public static byte[] Brown = { 165, 42, 42 };
            public static byte[] BurlyWood = { 222, 184, 135 };
            public static byte[] CadetBlue = { 95, 158, 160 };
            public static byte[] Chartreuse = { 127, 255, 0 };
            public static byte[] Chocolate = { 210, 105, 30 };
            public static byte[] Coral = { 255, 127, 80 };
            public static byte[] CornflowerBlue = { 100, 149, 237 };
            public static byte[] Cornsilk = { 255, 248, 220 };
            public static byte[] Crimson = { 220, 20, 60 };
            public static byte[] Cyan = { 0, 255, 255 };
            public static byte[] DarkBlue = { 0, 0, 139 };
            public static byte[] DarkCyan = { 0, 139, 139 };
            public static byte[] DarkGrey = { 169, 169, 169 };
            public static byte[] DarkGreen = { 0, 100, 0 };
            public static byte[] DarkKhaki = { 189, 183, 107 };
            public static byte[] DarkMagneta = { 139, 0, 139 };
            public static byte[] DarkOliveGreen = { 85, 107, 47 };
            public static byte[] DarkOrange = { 255, 140, 0 };
            public static byte[] DarkOrchid = { 153, 50, 204 };
            public static byte[] DarkRed = { 139, 0, 0 };
            public static byte[] DarkSalmon = { 233, 150, 122 };
            public static byte[] DarkSeaGreen = { 143, 188, 143 };
            public static byte[] DarkSlateBlue = { 72, 61, 139 };
            public static byte[] DarkSlateGrey = { 47, 79, 79 };
            public static byte[] DarkTurquiose = { 0, 206, 209 };
            public static byte[] DarkViolet = { 148, 0, 211 };
            public static byte[] DeepPink = { 255, 20, 147 };
            public static byte[] DeepSkyBlue = { 0, 191, 255 };
            public static byte[] DimGrey = { 105, 105, 105 };
            public static byte[] DodgerBlue = { 30, 144, 255 };
            public static byte[] FireBrick = { 178, 34, 34 };
            public static byte[] FloralWhite = { 255, 250, 240 };
            public static byte[] ForestGreen = { 34, 139, 34 };
            public static byte[] Fuchsia = { 255, 0, 255 };
            public static byte[] Gainsboro = { 220, 220, 220 };
            public static byte[] GhostWhite = { 248, 248, 255 };
            public static byte[] Gold = { 255, 215, 0 };
            public static byte[] GoldenRod = { 218, 165, 32 };
            public static byte[] Web_Green = { 0, 128, 0 };
            public static byte[] GreenYellow = { 173, 255, 47 };
            public static byte[] HoneyDew = { 240, 255, 240 };
            public static byte[] HotPink = { 255, 105, 180 };
            public static byte[] IndianRed = { 205, 92, 92 };
            public static byte[] Indigo = { 75, 0, 130 };
            public static byte[] Ivory = { 255, 255, 240 };
            public static byte[] Khaki = { 240, 230, 140 };
            public static byte[] Lavender = { 230, 230, 250 };
            public static byte[] LavenderBlush = { 255, 240, 245 };
            public static byte[] LawnGreen = { 124, 252, 0 };
            public static byte[] LemonChiffon = { 255, 250, 205 };
            public static byte[] LightBlue = { 173, 216, 230 };
            public static byte[] LightCoral = { 240, 128, 128 };
            public static byte[] LightCyan = { 224, 255, 255 };
            public static byte[] LightGoldenRodYellow = { 250, 250, 210 };
            public static byte[] Web_LightGrey = { 211, 211, 211 };
            public static byte[] LightGreen = { 144, 238, 144 };
            public static byte[] LightPink = { 255, 160, 122 };
            public static byte[] LightSalmon = { 255, 160, 122 };
            public static byte[] LightSeaGreen = { 32, 178, 170 };
            public static byte[] LightSkyBlue = { 135, 206, 250 };
            public static byte[] LightSlateGrey = { 119, 136, 153 };
            public static byte[] LightSteelBlue = { 176, 196, 222 };
            public static byte[] LightYellow = { 255, 255, 224 };
            public static byte[] LimeGreen = { 50, 205, 50 };
            public static byte[] Linen = { 250, 240, 230 };
            public static byte[] Maroon = { 128, 0, 0 };
            public static byte[] MediumAquaMarine = { 102, 205, 170 };
            public static byte[] MediumBlue = { 0, 0, 205 };
            public static byte[] MediumOrchid = { 186, 85, 211 };
            public static byte[] MediumPurple = { 147, 112, 219 };
            public static byte[] MediumSeaGreen = { 60, 179, 113 };
            public static byte[] MediumSlateBlue = { 123, 104, 238 };
            public static byte[] MediumSpringGreen = { 0, 250, 154 };
            public static byte[] MediumTurquoise = { 72, 209, 204 };
            public static byte[] MediumVioletRed = { 199, 21, 133 };
            public static byte[] MidnightBlue = { 25, 25, 112 };
            public static byte[] MintCream = { 245, 255, 250 };
            public static byte[] MintyRose = { 255, 228, 225 };
            public static byte[] Moccasin = { 255, 228, 181 };
            public static byte[] NavajoWhite = { 255, 222, 173 };
            public static byte[] Navy = { 0, 0, 128 };
            public static byte[] OldLace = { 253, 245, 230 };
            public static byte[] Olive = { 128, 128, 0 };
            public static byte[] OliveDrab = { 107, 142, 35 };
            public static byte[] Orange = { 255, 165, 0 };
            public static byte[] OrangeRed = { 255, 69, 0 };
            public static byte[] Orchid = { 218, 112, 214 };
            public static byte[] PaleGoldenRod = { 238, 232, 170 };
            public static byte[] PaleGreen = { 152, 251, 152 };
            public static byte[] PaleTurquoise = { 175, 238, 238 };
            public static byte[] PaleVioletRed = { 219, 112, 147 };
            public static byte[] PapayaWhip = { 255, 239, 213 };
            public static byte[] PeachPuff = { 255, 218, 185 };
            public static byte[] Peru = { 205, 133, 63 };
            public static byte[] Pink = { 255, 192, 203 };
            public static byte[] Plum = { 221, 160, 221 };
            public static byte[] PowderBlue = { 176, 224, 230 };
            public static byte[] Web_Purple = { 128, 0, 128 };
            public static byte[] RosyBrown = { 188, 143, 143 };
            public static byte[] RoyalBlue = { 65, 105, 225 };
            public static byte[] SaddleBrown = { 139, 69, 19 };
            public static byte[] Salmon = { 250, 128, 114 };
            public static byte[] SandyBrown = { 244, 164, 96 };
            public static byte[] SeaGreen = { 46, 139, 87 };
            public static byte[] SeaShell = { 255, 245, 238 };
            public static byte[] Sienna = { 160, 82, 45 };
            public static byte[] Silver = { 192, 192, 192 };
            public static byte[] SkyBlue = { 135, 206, 235 };
            public static byte[] SlateBlue = { 106, 90, 205 };
            public static byte[] SlateGrey = { 112, 128, 144 };
            public static byte[] Snow = { 255, 250, 250 };
            public static byte[] SpringGreen = { 0, 255, 127 };
            public static byte[] SteelBlue = { 70, 130, 180 };
            public static byte[] Tan = { 210, 180, 140 };
            public static byte[] Teal = { 0, 128, 128 };
            public static byte[] Thistle = { 216, 191, 216 };
            public static byte[] Tomato = { 255, 99, 71 };
            public static byte[] Turquoise = { 64, 224, 208 };
            public static byte[] Violet = { 238, 130, 238 };
            public static byte[] Wheat = { 245, 222, 179 };
            public static byte[] WhiteSmoke = { 245, 245, 245 };
            public static byte[] YellowGreen = { 154, 205, 50 };
            public static byte[] HenrysRandom = { 89, 36, 255 };
            #endregion
        }
        public enum SystemColors
        {
            White = 1,
            Gray = 2,
            Magenta = 3,
            Purple = 4,
            DarkPurple = 5,
            Red = 6,
            LightGray = 7,
            Green = 8,
            Lime = 9,
            DarkGray = 10,
            Yellow = 11,
            AliceBlue = 12,
            AntiqueWhite = 13,
            Aquamarine = 14,
            Azure = 15,
            Beige = 16,
            Bisque = 17,
            BlanchedAlmond = 18,
            Blue = 19,
            BlueViolet = 20,
            Brown = 21,
            BurlyWood = 22,
            CadetBlue = 23,
            Chartreuse = 24,
            Chocolate = 25,
            Coral = 26,
            CornflowerBlue = 27,
            Cornsilk = 28,
            Crimson = 29,
            Cyan = 30,
            DarkBlue = 31,
            DarkCyan = 32,
            DarkGrey = 33,
            DarkGreen = 34,
            DarkKhaki = 35,
            DarkMagneta = 36,
            DarkOliveGreen = 37,
            DarkOrange = 38,
            DarkOrchid = 39,
            DarkRed = 40,
            DarkSalmon = 41,
            DarkSeaGreen = 42,
            DarkSlateBlue = 43,
            DarkSlateGrey = 44,
            DarkTurquiose = 45,
            DarkViolet = 46,
            DeepPink = 47,
            DeepSkyBlue = 48,
            DimGrey = 49,
            DodgerBlue = 50,
            FireBrick = 51,
            FloralWhite = 52,
            ForestGreen = 53,
            Fuchsia = 54,
            Gainsboro = 55,
            GhostWhite = 56,
            Gold = 57,
            GoldenRod = 58,
            Web_Green = 59,
            GreenYellow = 60,
            HoneyDew = 61,
            HotPink = 62,
            IndianRed = 63,
            Indigo = 64,
            Ivory = 65,
            Khaki = 66,
            Lavender = 67,
            LavenderBlush = 68,
            LawnGreen = 69,
            LemonChiffon = 70,
            LightBlue = 71,
            LightCoral = 72,
            LightCyan = 73,
            LightGoldenRodYellow = 74,
            Web_LightGrey = 75,
            LightGreen = 76,
            LightPink = 77,
            LightSalmon = 78,
            LightSeaGreen = 79,
            LightSkyBlue = 80,
            LightSlateGrey = 81,
            LightSteelBlue = 82,
            LightYellow = 83,
            LimeGreen = 84,
            Linen = 85,
            Maroon = 86,
            MediumAquaMarine = 87,
            MediumBlue = 88,
            MediumOrchid = 89,
            MediumPurple = 90,
            MediumSeaGreen = 91,
            MediumSlateBlue = 92,
            MediumSpringGreen = 93,
            MediumTurquoise = 94,
            MediumVioletRed = 95,
            MidnightBlue = 96,
            MintCream = 97,
            MintyRose = 98,
            Moccasin = 99,
            NavajoWhite = 100,
            Navy = 101,
            OldLace = 102,
            Olive = 103,
            OliveDrab = 104,
            Orange = 105,
            OrangeRed = 106,
            Orchid = 107,
            PaleGoldenRod = 108,
            PaleGreen = 109,
            PaleTurquoise = 110,
            PaleVioletRed = 111,
            PapayaWhip = 112,
            PeachPuff = 113,
            Peru = 114,
            Pink = 115,
            Plum = 116,
            PowderBlue = 117,
            Web_Purple = 118,
            RosyBrown = 119,
            RoyalBlue = 120,
            SaddleBrown = 121,
            Salmon = 122,
            SandyBrown = 123,
            SeaGreen = 124,
            SeaShell = 125,
            Sienna = 126,
            Silver = 127,
            SkyBlue = 128,
            SlateBlue = 129,
            SlateGrey = 130,
            Snow = 131,
            SpringGreen = 132,
            SteelBlue = 133,
            Tan = 134,
            Teal = 135,
            Thistle = 136,
            Tomato = 137,
            Turquoise = 138,
            Violet = 139,
            Wheat = 140,
            WhiteSmoke = 141,
            YellowGreen = 142,
            Black = 144,
        };
    }
}