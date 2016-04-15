namespace PolarisOS.Driver.Graphics.VMWare
{
    public enum FIFOCommand
    {
        Update = 1,
        RECT_FILL = 2,
        RECT_COPY = 3,
        DEFINE_BITMAP = 4,
        DEFINE_BITMAP_SCANLINE = 5,
        DEFINE_PIXMAP = 6,
        DEFINE_PIXMAP_SCANLINE = 7,
        RECT_BITMAP_FILL = 8,
        RECT_PIXMAP_FILL = 9,
        RECT_BITMAP_COPY = 10,
        RECT_PIXMAP_COPY = 11,
        FREE_OBJECT = 12,
        RECT_ROP_FILL = 13,
        RECT_ROP_COPY = 14,
        RECT_ROP_BITMAP_FILL = 15,
        RECT_ROP_PIXMAP_FILL = 16,
        RECT_ROP_BITMAP_COPY = 17,
        RECT_ROP_PIXMAP_COPY = 18,
        DEFINE_CURSOR = 19,
        DISPLAY_CURSOR = 20,
        MOVE_CURSOR = 21,
        DEFINE_ALPHA_CURSOR = 22
    }
}