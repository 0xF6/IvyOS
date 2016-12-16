namespace IvyOS.Driver.Graphics.VMWare
{
    public enum FIFO : uint
    {   // values are multiplied by 4 to access the array by byte index
        Min = 0,
        Max = 4,
        NextCmd = 8,
        Stop = 12
    }
}