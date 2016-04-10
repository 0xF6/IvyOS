namespace PolarisOS.Core.Exception
{
    public class KernelException : System.Exception
    {
        public string MethodBase { private set; get; }
        public ulong UID { private set; get; } = 289275347456;

        public KernelException(string msg) : base(msg) { }

        public KernelException(string msg, string method) : this(msg)
        {
            MethodBase = method;
        }
    }
}