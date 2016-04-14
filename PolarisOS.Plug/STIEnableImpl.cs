using Cosmos.Assembler;
using Cosmos.IL2CPU.Plugs;
using PolarisOS.Driver.Core;
namespace PolarisOS.Plugs
{
    [Plug(Target = typeof(STIEnabler))]
    public class STIEnableImpl : AssemblerMethod
    {
        public override void AssembleNew(Assembler aAssembler, object aMethodInfo) => new Cosmos.Assembler.x86.Sti();
    }
}