using PolarisOS.Driver.Graphics;

namespace PolarisOS.HAL
{
    using Driver.Graphics.VMWare;
    using System;

    public static class Display
    {
        public struct SetupInfo
        {
            public ushort depth;
            public bool isClear;
            public ushort depthColor;
            public ScreenSize resolution;
        }

        /// <summary>
        /// Available text sizes.
        /// </summary>
        public enum TextSize
        { Size40x25, Size40x50, Size80x25, Size80x50, Size90x30, Size90x60 };

        private static SVGADriver vga;
        private static ushort depth;
        private static ScreenSize CurrentRes;


        public static void Setup(SetupInfo inf)
        {
            vga = new SVGADriver();
            vga.SetMode(inf.resolution, depth:inf.depth, isClear:inf.isClear, colorClear:inf.depthColor);
            depth = inf.depth;
            CurrentRes = inf.resolution;

            VGAColors.ClearEntys();
            //Now add in our colors based on resolution.
            if (depth == 256)
            {
                //Set all of our needed colors for our 256 colors.
                //These are contain the basics, and the advanced, use them.
                VGAColors.AddEntry(240, 248, 255);
                VGAColors.AddEntry(250, 235, 215);
                VGAColors.AddEntry(0, 255, 255);
                VGAColors.AddEntry(127, 255, 212);
                VGAColors.AddEntry(240, 255, 255);
                VGAColors.AddEntry(245, 245, 220);
                VGAColors.AddEntry(255, 228, 196);
                VGAColors.AddEntry(0, 0, 0);
                VGAColors.AddEntry(255, 235, 205);
                VGAColors.AddEntry(0, 0, 255);
                VGAColors.AddEntry(138, 43, 226);
                VGAColors.AddEntry(165, 42, 42);
                VGAColors.AddEntry(222, 184, 135);
                VGAColors.AddEntry(95, 158, 160);
                VGAColors.AddEntry(127, 255, 0);
                VGAColors.AddEntry(210, 105, 30);
                VGAColors.AddEntry(255, 127, 80);
                VGAColors.AddEntry(100, 149, 237);
                VGAColors.AddEntry(255, 248, 220);
                VGAColors.AddEntry(220, 20, 60);
                VGAColors.AddEntry(0, 255, 255);
                VGAColors.AddEntry(0, 0, 139);
                VGAColors.AddEntry(0, 139, 139);
                VGAColors.AddEntry(184, 134, 11);
                VGAColors.AddEntry(169, 169, 169);
                VGAColors.AddEntry(0, 100, 0);
                VGAColors.AddEntry(189, 183, 107);
                VGAColors.AddEntry(139, 0, 139);
                VGAColors.AddEntry(85, 107, 47);
                VGAColors.AddEntry(255, 140, 0);
                VGAColors.AddEntry(153, 50, 204);
                VGAColors.AddEntry(139, 0, 0);
                VGAColors.AddEntry(233, 150, 122);
                VGAColors.AddEntry(143, 188, 143);
                VGAColors.AddEntry(72, 61, 139);
                VGAColors.AddEntry(47, 79, 79);
                VGAColors.AddEntry(0, 206, 209);
                VGAColors.AddEntry(148, 0, 211);
                VGAColors.AddEntry(255, 20, 147);
                VGAColors.AddEntry(0, 191, 255);
                VGAColors.AddEntry(105, 105, 105);
                VGAColors.AddEntry(30, 144, 255);
                VGAColors.AddEntry(178, 34, 34);
                VGAColors.AddEntry(255, 250, 240);
                VGAColors.AddEntry(34, 139, 34);
                VGAColors.AddEntry(255, 0, 255);
                VGAColors.AddEntry(220, 220, 220);
                VGAColors.AddEntry(248, 248, 255);
                VGAColors.AddEntry(255, 215, 0);
                VGAColors.AddEntry(218, 165, 32);
                VGAColors.AddEntry(128, 128, 128);
                VGAColors.AddEntry(0, 128, 0);
                VGAColors.AddEntry(173, 255, 47);
                VGAColors.AddEntry(240, 255, 240);
                VGAColors.AddEntry(255, 105, 180);
                VGAColors.AddEntry(205, 92, 92);
                VGAColors.AddEntry(75, 0, 130);
                VGAColors.AddEntry(255, 255, 240);
                VGAColors.AddEntry(240, 230, 140);
                VGAColors.AddEntry(230, 230, 250);
                VGAColors.AddEntry(255, 240, 245);
                VGAColors.AddEntry(124, 252, 0);
                VGAColors.AddEntry(255, 250, 205); //LemonChiffon
            }
            else if (depth == 16)
            {
                //Set our 16 colors. Now since we only have 16 colors, lets
                //add the most common ones.
                VGAColors.AddEntry(0, 255, 255);
                VGAColors.AddEntry(0, 0, 0);
                VGAColors.AddEntry(0, 0, 255);
                VGAColors.AddEntry(255, 0, 255);
                VGAColors.AddEntry(128, 128, 128);
                VGAColors.AddEntry(0, 128, 0);
                VGAColors.AddEntry(0, 255, 0);
                VGAColors.AddEntry(128, 0, 0);
                VGAColors.AddEntry(0, 0, 128);
                VGAColors.AddEntry(128, 128, 0);
                VGAColors.AddEntry(128, 0, 128);
                VGAColors.AddEntry(255, 0, 0);
                VGAColors.AddEntry(192, 192, 192);
                VGAColors.AddEntry(0, 128, 128);
                VGAColors.AddEntry(255, 255, 255);
                VGAColors.AddEntry(255, 255, 0);
            }
            //No matter what Resolution, still set all the colors in the entrys.
            for (int i = 0; i < VGAColors.PalletteEntrys.Count; i++)
            {
                SetPaletteEntry(VGAColors.PalletteEntrys[i].Index, VGAColors.PalletteEntrys[i]);
            }
            //Now lets clear the screen to black, since 16 colors or 256 colors, they all contain black.
            Clear(VGAColors.FindIndex(0, 0, 0));
            //Now draw our test square.
            if (vga.GetPixel(1, 1) == VGAColors.FindIndex(0, 0, 0))
            {
                int c = VGAColors.FindIndex(255, 0, 0);
                for (uint y = 8; y < ScreenHeight - 8; y++)
                {
                    for (uint x = 8; x < ScreenWidth - 8; x++)
                    {
                        SetPixel(x, y, (uint)c);
                    }
                }
            }


        }

        public static void Clear(int Color) => vga.Clear((uint)Color);

        public static void SetPixel(int x, int y, int Color) => vga.SetPixel((ushort)x, (ushort)y, Color);

        public static void Update()
        {
            switch (CurrentRes)
            {
                case ScreenSize.v800x600:
                    vga.Update(0, 0, 800, 600);
                    break;
                case ScreenSize.v1024x768:
                    vga.Update(0, 0, 1024, 768);
                    break;
                case ScreenSize.v1280x720:
                    vga.Update(0, 0, 1280, 720);
                    break;
                case ScreenSize.v1360x768:
                    vga.Update(0, 0, 1360, 768);
                    break;
                case ScreenSize.v1360x1024:
                    vga.Update(0, 0, 1360, 1024);
                    break;
                case ScreenSize.v1366x768:
                    vga.Update(0, 0, 1366, 768);
                    break;
                case ScreenSize.v1440x900:
                    vga.Update(0, 0, 1440, 900);
                    break;
                case ScreenSize.v1600x1200:
                    vga.Update(0, 0, 1600, 1200);
                    break;
                case ScreenSize.v1680x1050:
                    vga.Update(0, 0, 1680, 1050);
                    break;
                case ScreenSize.v1920x1080:
                    vga.Update(0, 0, 1920, 1080);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}