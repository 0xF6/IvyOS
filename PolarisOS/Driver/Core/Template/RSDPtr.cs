namespace PolarisOS.Driver.Core.Template
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct RSDPtr
    {
        internal fixed byte Signature[8];
        internal byte CheckSum;
        internal fixed byte OemID[6];
        internal byte Revision;
        internal int RsdtAddress;
    }
}