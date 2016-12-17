namespace Ivy.Framework.Driver
{
    public class IDT
    {
        public delegate void ISR();
        public static ISR[] idt = new ISR[0xFF];
        public static void Remap()
        {
            AXP.Outb(0x20, 0x11);
            AXP.Outb(0xA0, 0x11);
            AXP.Outb(0x21, 0x20);
            AXP.Outb(0xA1, 0x28);
            AXP.Outb(0x21, 0x04);
            AXP.Outb(0xA1, 0x02);
            AXP.Outb(0x21, 0x01);
            AXP.Outb(0xA1, 0x01);
            AXP.Outb(0x21, 0x0);
            AXP.Outb(0xA1, 0x0);
        }
        private void idt_handler()
        {
            int num = 0;
            if (idt.Length > num && idt[num] != null) idt[num]();
        }

        public static void SetGate(byte int_num, ISR handler) => idt[int_num] = handler;
    }
}