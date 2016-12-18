using Ivy.Framework.Driver;

namespace IvyOS.Core
{
    using Cosmos.IL2CPU.Plugs;
    //[Plug(Target = typeof(Cosmos.HAL.TextScreen))]
    public class TextScreenImpl
    {
        public static void RemoveCursor()
        {
            AXP.Outb(0x3D4, 14);
            AXP.Outb(0x3D5, 0x07);
            AXP.Outb(0x3D4, 15);
            AXP.Outb(0x3D5, 0xD0);
        }
    }
}