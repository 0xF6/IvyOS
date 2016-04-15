namespace PolarisOS.Driver.Graphics.VMWare
{
    public enum ID : uint
    {
        Magic = 0x900000,
        V0 = Magic << 8,
        V1 = (Magic << 8) | 1,
        V2 = (Magic << 8) | 2,
        Invalid = 0xFFFFFFFF
    }
}