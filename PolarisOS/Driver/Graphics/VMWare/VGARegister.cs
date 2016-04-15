namespace PolarisOS.Driver.Graphics.VMWare
{
    public enum VGARegister : ushort
    {
        ID = 0,
        Enable = 1,
        Width = 2,
        Height = 3,
        MaxWidth = 4,
        MaxHeight = 5,
        Depth = 6,
        BitsPerPixel = 7,
        PseudoColor = 8,
        RedMask = 9,
        GreenMask = 10,
        BlueMask = 11,
        BytesPerLine = 12,
        FrameBufferStart = 13,
        FrameBufferOffset = 14,
        VRamSize = 15,
        FrameBufferSize = 16,

        Capabilities = 17,
        MemStart = 18,
        MemSize = 19,
        ConfigDone = 20,
        Sync = 21,
        Busy = 22,
        GuestID = 23,
        CursorID = 24,
        CursorX = 25,
        CursorY = 26,
        CursorOn = 27,
        HostBitsPerPixel = 28,
        ScratchSize = 29,
        MemRegs = 30,
        NumDisplays = 31,
        PitchLock = 32,

        /// <summary>
        /// Indicates maximum size of FIFO Registers.
        /// </summary>
        FifoNumRegisters = 293
    }
}