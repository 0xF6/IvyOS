namespace Ivy.Framework
{
    using Driver;

    public class KernelException : System.Exception
    {
        public string MethodBase { private set; get; }
        public ulong UID { private set; get; } = 289275347456;
        public IRQSafeContext context { private set; get; }
        public bool isSetContext { private set; get; } = false;
        public KernelException(string msg) : base(msg) { }

        public KernelException(string msg, string method) : this(msg)
        {
            MethodBase = method;
        }
        public KernelException(string msg, IRQSafeContext context) : this(msg)
        {
            this.context = context;
            isSetContext = true;
        }
        public KernelException(string msg, IRQSafeContext context, string method) : this(msg, method)
        {
            this.context = context;
            isSetContext = true;
        }
    }
}