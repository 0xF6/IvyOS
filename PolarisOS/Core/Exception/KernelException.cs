namespace PolarisOS.Core.Exception
{
    public class KernelException : System.Exception
    {
        public string MethodBase { private set; get; }
        public ulong UID { private set; get; } = 289275347456;
        public Cosmos.Core.INTs.IRQContext context { private set; get; }
    
        public KernelException(string msg) : base(msg) { }

        public KernelException(string msg, string method) : this(msg)
        {
            MethodBase = method;
        }
        public KernelException(string msg, Cosmos.Core.INTs.IRQContext context) : this(msg)
        {
            this.context = context;
        }
        public KernelException(string msg, Cosmos.Core.INTs.IRQContext context, string method) : this(msg, method)
        {
            this.context = context;
        }
    }
}