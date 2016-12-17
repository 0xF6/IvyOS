namespace Ivy.Framework.Driver
{
    public class PIT
    {
        public static void Mode0(uint frequency)
        {
            IDT.Remap();
            IRQ.ClearMask(0);
            IRQ.ClearMask(15);
            uint divisor = 1193180 / frequency;
            AXP.Outb(0x43, 0x30);
            AXP.Outb(0x40, (byte)(divisor & 0xFF));
            AXP.Outb(0x40, (byte)((divisor >> 8) & 0xFF));
        }
        public static void Mode2(uint frequency)
        {
            IDT.Remap();
            IRQ.ClearMask(0);
            IRQ.ClearMask(15);
            uint divisor = 1193180 / frequency;
            AXP.Outb(0x43, 0x36);
            AXP.Outb(0x40, (byte)(divisor & 0xFF));
            AXP.Outb(0x40, (byte)((divisor >> 8) & 0xFF));
        }
        public static void Beep(uint frequency)
        {
            uint divisor = 1193180 / frequency;
            AXP.Outb(0x43, 0xB6);
            AXP.Outb(0x42, (byte)(divisor & 0xFF));
            AXP.Outb(0x42, (byte)((divisor >> 8) & 0xFF));
        }
        public static bool called = false;
    }
}