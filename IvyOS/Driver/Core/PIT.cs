namespace IvyOS.Driver.Core
{
    using IvyOS.IO;
    public class PIT
    {
        public static void Mode0(uint frequency)
        {
            IDT.Remap();
            IRQ.ClearMask(0);
            IRQ.ClearMask(15);
            uint divisor = 1193180 / frequency;
            IOPorts.Outb(0x43, 0x30);
            IOPorts.Outb(0x40, (byte)(divisor & 0xFF));
            IOPorts.Outb(0x40, (byte)((divisor >> 8) & 0xFF));
        }
        public static void Mode2(uint frequency)
        {
            IDT.Remap();
            IRQ.ClearMask(0);
            IRQ.ClearMask(15);
            uint divisor = 1193180 / frequency;
            IOPorts.Outb(0x43, 0x36);
            IOPorts.Outb(0x40, (byte)(divisor & 0xFF));
            IOPorts.Outb(0x40, (byte)((divisor >> 8) & 0xFF));
        }
        public static void Beep(uint frequency)
        {
            uint divisor = 1193180 / frequency;
            IOPorts.Outb(0x43, 0xB6);
            IOPorts.Outb(0x42, (byte)(divisor & 0xFF));
            IOPorts.Outb(0x42, (byte)((divisor >> 8) & 0xFF));
        }
        public static bool called = false;
    }
}