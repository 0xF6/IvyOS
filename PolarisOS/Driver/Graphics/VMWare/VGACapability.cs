namespace PolarisOS.Driver.Graphics.VMWare
{
    using System;

    [Flags]
    public enum Capability
    {
        None = 0,
        RectFill = 1,
        RectCopy = 2,
        RectPatFill = 4,
        LecacyOffscreen = 8,
        RasterOp = 16,
        Cursor = 32,
        CursorByPass = 64,
        CursorByPass2 = 128,
        EigthBitEmulation = 256,
        AlphaCursor = 512,
        Glyph = 1024,
        GlyphClipping = 0x00000800,
        Offscreen1 = 0x00001000,
        AlphaBlend = 0x00002000,
        ThreeD = 0x00004000,
        ExtendedFifo = 0x00008000,
        MultiMon = 0x00010000,
        PitchLock = 0x00020000,
        IrqMask = 0x00040000,
        DisplayTopology = 0x00080000,
        Gmr = 0x00100000,
        Traces = 0x00200000,
        Gmr2 = 0x00400000,
        ScreenObject2 = 0x00800000
    }
}