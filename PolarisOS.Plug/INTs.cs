using PolarisOS.Core;
using PolarisOS.Core.Exception;

namespace PolarisOS.Plugs
{
    using Cosmos.IL2CPU.Plugs;

    [Plug(Target = typeof(Cosmos.Core.INTs))]
    public class INTs
    {
        private static readonly string[] ErrorArray =
        {
            "DIVIDE_BY_ZERO",
            "SINGLE_STEP",
            "NON_MASKABLE_INTERRUPT",
            "BREAK_FLOW",
            "OVERFLOW",
            "NULL",
            "INVALID_OPCODE",
            "",
            "DOUBLE_FAULT_EXCEPTION",
            "INVALID_TSS",
            "SEGMENT_NOT_PRESENT",
            "STACK_EXCEPTION",
            "GENERAL_PROTECTION_FAULT"
        };

        public static bool already = false;

        public static void HandleInterrupt_Default(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            API.HandleInt0x80(ref aContext);
        }

        public static void HandleInterrupt_00(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[0]));
        }
        public static void HandleInterrupt_01(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[1]));
        }
        public static void HandleInterrupt_02(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[2]));
        }
        public static void HandleInterrupt_03(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[3]));
        }
        public static void HandleInterrupt_04(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[4]));
        }
        public static void HandleInterrupt_05(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[5]));
        }
        public static void HandleInterrupt_06(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[6]));
        }
        public static void HandleInterrupt_07(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[7]));
        }
        public static void HandleInterrupt_08(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[8]));
        }
        public static void HandleInterrupt_09(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[9]));
        }
        public static void HandleInterrupt_0A(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[10]));
        }
        public static void HandleInterrupt_0B(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[11]));

        }
        public static void HandleInterrupt_0C(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[12]));
        }
        public static void HandleInterrupt_0D(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[13]));
        }
        public static void HandleInterrupt_0E(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[14]));
        }
        public static void HandleInterrupt_0F(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            RSoD.Init(new KernelException(ErrorArray[15]));
        }
    }
}