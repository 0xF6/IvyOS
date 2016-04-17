using System;
using System.Collections.Generic;
using Cosmos.Core;
using Cosmos.Debug.Kernel;
using PolarisOS.Core.Exception;
using PolarisOS.Driver.Core.Template;

namespace PolarisOS.Core
{
    public class CPUExceptionHandler
    {
        private static readonly Debugger dBg = new Debugger("Kernel", "CPUHandler");
        private static readonly CPUExceptionHandler hLD = new CPUExceptionHandler();

        public static void Handle()
        {
            dBg.Send("Set Handlers!");
            INTs.RegCPUExceptionHandler(Handlerer);
        }
        private static void Handlerer(ref INTs.IRQContext ctxUnsafe, int hex)
        {
            IRQSafeContext ctx = new IRQSafeContext
            {
                EAX = ctxUnsafe.EAX,
                CS = ctxUnsafe.CS,
                EBP = ctxUnsafe.EBP,
                EBX = ctxUnsafe.EBX,
                ECX = ctxUnsafe.ECX,
                EDI = ctxUnsafe.EDI,
                EDX = ctxUnsafe.EDX,
                EIP = ctxUnsafe.EIP,
                ESI = ctxUnsafe.ESI,
                ESP = ctxUnsafe.ESP,
                EFlags = ctxUnsafe.EFlags
            };


            dBg.Send($"Call handler: [{hex} - {(int)ctx.EFlags}]");
            switch (hex)
            {
                case 0x00: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x01: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x02: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x03: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x04: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x05: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x06: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x07: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x08: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x09: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x0A: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x0B: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x0C: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x0D: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x0E: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x0F: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x10: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x11: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x12: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case 0x13: RSoD.Push(new KernelException(ErrorArray[hex], ctx)); break;
                case-0x01: API.HandleInt0x80(ref ctxUnsafe);                     break;
                default  : RSoD.Panic();                                         break;
            }
        }

        public static readonly string[] ErrorArray = 
        {
            "Divide by zero",
            "Debug Exception",
            "Non Maskable Interrupt Exception",
            "Breakpoint Exception",
            "Into Detected Overflow Exception",
            "Out of Bounds Exception",
            "Invalid Opcode",
            "No Coprocessor Exception",
            "Double Fault Exception",
            "Coprocessor Segment Overrun Exception",
            "Bad TSS Exception",
            "Segment Not Present",
            "Stack Fault Exception",
            "General Protection Fault",
            "Page Fault Exception",
            "Unknown Interrupt Exception",
            "x87 Floating Point Exception",
            "Alignment Exception",
            "Machine Check Exception",
            "SIMD Floating Point Exception"
        };
    }
}