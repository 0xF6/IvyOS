namespace Ivy.Framework.Driver
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct FACP
    {
        internal fixed byte Signature[4];
        internal int Length;
        internal fixed byte unneded1[40 - 8];
        internal int* DSDT;
        internal fixed byte unneded2[48 - 44];
        internal int* SMI_CMD;
        internal byte ACPI_ENABLE;
        internal byte ACPI_DISABLE;
        internal fixed byte unneded3[64 - 54];
        internal int* PM1a_CNT_BLK;
        internal int* PM1b_CNT_BLK;
        internal fixed byte unneded4[89 - 72];
        internal byte PM1_CNT_LEN;
    }
}