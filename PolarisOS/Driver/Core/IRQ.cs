namespace PolarisOS.Driver.Core
{
    public class IRQ
    {
        public static void SetMask(byte IRQline)
        {
            ushort port;

            if (IRQline < 8)
            {
                port = 0x20 + 1;
            }
            else
            {
                port = 0xA0 + 1;
                IRQline -= 8;
            }
            var value = (byte)(IO.IOPort.inb(port) | (1 << IRQline));
            IO.IOPort.outb(port, value);
        }
        public static void ClearMask(byte IRQline)
        {
            ushort port;

            if (IRQline < 8)
            {
                port = 0x20 + 1;
            }
            else
            {
                port = 0xA0 + 1;
                IRQline -= 8;
            }
            var value = (byte)(IO.IOPort.inb(port) & ~(1 << IRQline));
            IO.IOPort.outb(port, value);
        }
    }
}
}