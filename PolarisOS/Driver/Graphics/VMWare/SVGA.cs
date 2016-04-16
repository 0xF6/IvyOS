﻿namespace PolarisOS.Driver.Graphics.VMWare
{
    using Cosmos.Core;
    using System;
    using Cosmos.HAL;

    public enum ScreenSize
    {
        v800x600,
        v1024x768,
        v1280x720,
        v1360x768,
        v1360x1024,
        v1366x768,
        v1440x900,
        v1600x1200,
        v1680x1050,
        v1920x1080
    }


    public class SVGADriver
    {
        private readonly IOPort IndexPort;
        private readonly IOPort ValuePort;
        private IOPort BiosPort;
        private IOPort IRQPort;

        private readonly MemoryBlock Video_Memory;
        private MemoryBlock FIFO_Memory;

        private readonly PCIDeviceNormal device;
        private ushort height;
        private ushort width;
        private uint depth;
        private readonly uint capabilities;

        public SVGADriver()
        {
            device = (PCIDeviceNormal)PCI.GetDevice(0x15AD, 0x0405);
            device.EnableMemory(true);
            uint basePort = device.BaseAddresses[0].BaseAddress();
            IndexPort = new IOPort((ushort)(basePort + (uint)IOPortOffset.Index));
            ValuePort = new IOPort((ushort)(basePort + (uint)IOPortOffset.Value));
            BiosPort = new IOPort((ushort)(basePort + (uint)IOPortOffset.Bios));
            IRQPort = new IOPort((ushort)(basePort + (uint)IOPortOffset.IRQ));

            WriteRegister(VGARegister.ID, (uint)ID.V2);
            if (ReadRegister(VGARegister.ID) != (uint)ID.V2)
                return;

            Video_Memory = new MemoryBlock(ReadRegister(VGARegister.FrameBufferStart), ReadRegister(VGARegister.VRamSize));

            capabilities = ReadRegister(VGARegister.Capabilities);
            InitializeFIFO();
        }


        protected void InitializeFIFO()
        {
            FIFO_Memory = new MemoryBlock(ReadRegister(VGARegister.MemStart), ReadRegister(VGARegister.MemSize))
            {
                [(uint) FIFO.Min] = (uint) VGARegister.FifoNumRegisters * sizeof(uint)
            };
            FIFO_Memory[(uint)FIFO.Max] = FIFO_Memory.Size;
            FIFO_Memory[(uint)FIFO.NextCmd] = FIFO_Memory[(uint)FIFO.Min];
            FIFO_Memory[(uint)FIFO.Stop] = FIFO_Memory[(uint)FIFO.Min];
            WriteRegister(VGARegister.ConfigDone, 1);
        }

        public void SetMode(ScreenSize size, ushort depth = 32, bool isClear = true, ushort colorClear = 255)
        {
            switch (size)
            {
                case ScreenSize.v800x600:
                    SetMode(800, 600, depth);
                    break;
                case ScreenSize.v1024x768:
                    SetMode(1024, 768, depth);
                    break;
                case ScreenSize.v1280x720:
                    SetMode(1280, 720, depth);
                    break;
                case ScreenSize.v1360x768:
                    SetMode(1360, 768, depth);
                    break;
                case ScreenSize.v1360x1024:
                    SetMode(1360, 1024, depth);
                    break;
                case ScreenSize.v1366x768:
                    SetMode(1366, 768, depth);
                    break;
                case ScreenSize.v1440x900:
                    SetMode(1440, 900, depth);
                    break;
                case ScreenSize.v1600x1200:
                    SetMode(1600, 1200, depth);
                    break;
                case ScreenSize.v1680x1050:
                    SetMode(1680, 1050, depth);
                    break;
                case ScreenSize.v1920x1080:
                    SetMode(1920, 1080, depth);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
            if(isClear)
            Clear(colorClear);
        }


        public void SetMode(ushort width, ushort height, ushort depth = 32)
        {
            // Depth is color depth in bytes.
            this.depth = (uint) (depth/8);
            this.width = width;
            this.height = height;
            WriteRegister(VGARegister.Width, width);
            WriteRegister(VGARegister.Height, height);
            WriteRegister(VGARegister.BitsPerPixel, depth);
            WriteRegister(VGARegister.Enable, 1);
            InitializeFIFO();
        }

        protected void WriteRegister(VGARegister register, uint value)
        {
            IndexPort.DWord = (uint) register;
            ValuePort.DWord = value;
        }

        protected uint ReadRegister(VGARegister register)
        {
            IndexPort.DWord = (uint) register;
            return ValuePort.DWord;
        }

        protected uint GetFIFO(FIFO cmd)
        {
            return FIFO_Memory[(uint) cmd];
        }

        protected uint SetFIFO(FIFO cmd, uint value)
        {
            return FIFO_Memory[(uint) cmd] = value;
        }

        protected void WaitForFifo()
        {
            WriteRegister(VGARegister.Sync, 1);
            while (ReadRegister(VGARegister.Busy) != 0)
            {
            }
        }

        protected void WriteToFifo(uint value)
        {
            if (((GetFIFO(FIFO.NextCmd) == GetFIFO(FIFO.Max) - 4) && GetFIFO(FIFO.Stop) == GetFIFO(FIFO.Min)) || (GetFIFO(FIFO.NextCmd) + 4 == GetFIFO(FIFO.Stop)))
                WaitForFifo();

            SetFIFO((FIFO) GetFIFO(FIFO.NextCmd), value);
            SetFIFO(FIFO.NextCmd, GetFIFO(FIFO.NextCmd) + 4);

            if (GetFIFO(FIFO.NextCmd) == GetFIFO(FIFO.Max))
                SetFIFO(FIFO.NextCmd, GetFIFO(FIFO.Min));
        }

        public void Update(ushort x, ushort y, ushort width, ushort height)
        {
            WriteToFifo((uint) FIFOCommand.Update);
            WriteToFifo(x);
            WriteToFifo(y);
            WriteToFifo(width);
            WriteToFifo(height);
            WaitForFifo();
        }

        public void SetPixel(int x, int y, int color) => Video_Memory[(uint) ((y*width + x)*depth)] = (ushort) color;
        public uint GetPixel(ushort x, ushort y) => Video_Memory[(uint) ((y*width + x)*depth)];

        public void Clear(uint color)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    SetPixel((ushort) x, (ushort) y, (ushort) color);
                }
            }
        }

        public void Copy(ushort x, ushort y, ushort newX, ushort newY, ushort width, ushort height)
        {
            if ((capabilities & (uint) Capability.RectCopy) != 0)
            {
                WriteToFifo((uint) FIFOCommand.RECT_COPY);
                WriteToFifo(x);
                WriteToFifo(y);
                WriteToFifo(newX);
                WriteToFifo(newY);
                WriteToFifo(width);
                WriteToFifo(height);
                WaitForFifo();
            }
            else throw new NotImplementedException("VMWareSVGAII Copy()");
        }

        public void Fill(ushort x, ushort y, ushort width, ushort height, uint color)
        {
            if ((capabilities & (uint) Capability.RectFill) != 0)
            {
                WriteToFifo((uint) FIFOCommand.RECT_FILL);
                WriteToFifo(color);
                WriteToFifo(x);
                WriteToFifo(y);
                WriteToFifo(width);
                WriteToFifo(height);
                WaitForFifo();
            }
            else
            {
                if ((capabilities & (uint) Capability.RectCopy) != 0)
                {
                    // fill first line and copy it to all other
                    ushort xTarget = (ushort) (x + width);
                    ushort yTarget = (ushort) (y + height);

                    for (ushort xTmp = x; xTmp < xTarget; xTmp++)
                        SetPixel(xTmp, y, (ushort)color);
                    // refresh first line for copy process
                    Update(x, y, width, 1);
                    for (ushort yTmp = (ushort) (y + 1); yTmp < yTarget; yTmp++)
                        Copy(x, y, x, yTmp, width, 1);
                }
                else
                {
                    ushort xTarget = (ushort) (x + width);
                    ushort yTarget = (ushort) (y + height);
                    for (ushort xTmp = x; xTmp < xTarget; xTmp++)
                        for (ushort yTmp = y; yTmp < yTarget; yTmp++)
                            SetPixel(xTmp, yTmp, (ushort)color);
                    Update(x, y, width, height);
                }
            }
        }

        public void DefineCursor()
        {
            WaitForFifo();
            WriteToFifo((uint) FIFOCommand.DEFINE_CURSOR);
            WriteToFifo(1);
            WriteToFifo(0);
            WriteToFifo(0);
            WriteToFifo(2);
            WriteToFifo(2);
            WriteToFifo(1);
            WriteToFifo(1);
            for (int i = 0; i < 4; i++) WriteToFifo(0);
            for (int i = 0; i < 4; i++) WriteToFifo(0xFFFFFF);
            WaitForFifo();
        }

        public void SetCursor(bool visible, uint x, uint y)
        {
            WriteRegister(VGARegister.CursorID, 1);
            if (visible)
            {
                WaitForFifo();
                WriteToFifo((uint) FIFOCommand.MOVE_CURSOR);
                WriteToFifo(x);
                WriteToFifo(y);
            }
            WriteRegister(VGARegister.CursorOn, (uint) (visible ? 1 : 0));
        }


        private static int i = 0;
        private static int t = 0;
        private static int count = 0;
        private static int bb, cc;

        public void DrawFrame(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            count = 0;
            for (i = 0; i < length; i++)
            for (t = 0; t < width; t++, count++)
            if (Arr[count] != 0xFF00FF)
            SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), (int)Arr[count]);
        }
        public void DrawFrameColor(uint[] Arr, int width, int length, int xpixel, int ypixel, int color)
        {
            count = 0;
            for (i = 0; i < length; i++)
            for (t = 0; t < width; t++, count++)
            if (Arr[count] == 1)
            SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), color);
        }
        public void DrawFrameBlackAndWhite(uint[] Arr, int width, int length, int xpixel, int ypixel)
        {
            count = 0;
            for (i = 0; i < length; i++)
            for (t = 0; t < width; t++, count++)
            {
                if (Arr[count] == 1)
                    SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), 0x000000);
                if (Arr[count] == 2)
                    SetPixel((int)(0 + (uint)t + 0 + (uint)xpixel), (int)(0 + (uint)i + 0 + (uint)ypixel), 0xffffff);
            }
        }
        public void DrawRectangle(int x, int y, int width, int length, int color)
        {
            for (bb = x; bb < x + width; bb++)
            for (cc = y; cc < y + length; cc++)
            SetPixel(bb, cc, color);
        }

    }
}