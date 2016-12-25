namespace IvyOS.Core
{
    using Cosmos.IL2CPU.Plugs;

    [Plug(typeof(System.Math))]
    public class MathImpl
    {
        [PluggedMethod(nameof(System.Math.Max))] public static int Max(int a, int b) => b & ((a - b) >> 31) | a & (~(a - b) >> 31);
        [PluggedMethod(nameof(System.Math.Min))] public static int Min(int a, int b) => a & ((a - b) >> 31) | b & (~(a - b) >> 31);
    }



    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class PluggedMethod : System.Attribute
    {
        public PluggedMethod(string d)
        { }
    }
}