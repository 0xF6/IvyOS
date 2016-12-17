namespace IvyOS.Core
{
    using Ivy.Framework.Driver;
    using Ivy.Framework.Core;
    using Cosmos.IL2CPU.Plugs;
    using CPUx86 = Cosmos.Assembler.x86;
    using Cosmos.Core;
    using IRQContext = Cosmos.Core.INTs.IRQContext;
    [Plug(Target = typeof(Cosmos.Core.INTs))]
    public class INTs
    {
        public static void HandleInterrupt_00(ref IRQContext aContext) => RSoD.Push("Divide by zero Exception", "YOU JUST CREATED A BLACK HOLE!", true);
        public static void HandleInterrupt_01(ref IRQContext aContext) => RSoD.Push("Debug Exception", " ", true);
        public static void HandleInterrupt_02(ref IRQContext aContext) => RSoD.Push("Non Maskable Interrupt Exception", " ", true);
        public static void HandleInterrupt_03(ref IRQContext aContext) => RSoD.Push("Breakpoint Exception", " ", true);
        public static void HandleInterrupt_04(ref IRQContext aContext) => RSoD.Push("Into Detected Overflow Exception", "", true);
        public static void HandleInterrupt_05(ref IRQContext aContext) => RSoD.Push("Out of Bounds Exception", " ", true);
        //public static void HandleInterrupt_06(ref IRQContext aContext) => RSoD.Push("Invalid Opcode", " ", true);
        //public static void HandleInterrupt_07(ref IRQContext aContext) => RSoD.Push("No Coprocessor Exception", " ", true);
        public static void HandleInterrupt_08(ref IRQContext aContext) => RSoD.Push("Double Fault Exception", " ", true);
        public static void HandleInterrupt_09(ref IRQContext aContext) => RSoD.Push("Coprocessor Segment Overrun Exception", " ", true);
        public static void HandleInterrupt_10(ref IRQContext aContext) => RSoD.Push("x87 Floating Point Exception", " ", true);
        public static void HandleInterrupt_11(ref IRQContext aContext) => RSoD.Push("Alignment Exception", " ", true);
        public static void HandleInterrupt_12(ref IRQContext aContext) => RSoD.Push("Machine Check Exception", " ", true);
        public static void HandleInterrupt_13(ref IRQContext aContext) => RSoD.Push("SIMD Floating Point Exception", " ", true);

        public static void HandleInterrupt_0A(ref IRQContext aContext) => RSoD.Push("Bad TSS Exception", " ", true);
        public static void HandleInterrupt_0B(ref IRQContext aContext) => RSoD.Push("Segment not present", " ", true);
        public static void HandleInterrupt_0C(ref IRQContext aContext) => RSoD.Push("Stack Fault Exception", " ", true);
        public static void HandleInterrupt_0D(ref IRQContext aContext) => RSoD.Push("General Protection Fault", "GPF", true);
        public static void HandleInterrupt_0E(ref IRQContext aContext) => RSoD.Push("Page Fault Exception", " ", true);
        public static void HandleInterrupt_0F(ref IRQContext aContext) => RSoD.Push("Unknown Interrupt Exception", " ", true);
        // IRQ0
        public static void HandleInterrupt_20(ref IRQContext aContext)
        {
            Global.PIC.EoiMaster();
            AXP.Outb(0x20, 0x20);
            PIT.called = true;
        }
    }
    [Plug(Target = typeof(STIEnabler))]
    public class Enable : AssemblerMethod
    {
        public override void AssembleNew(Cosmos.Assembler.Assembler aAssembler, object aMethodInfo)
        {
            new CPUx86.Sti();
        }
    }
}