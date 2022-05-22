using UnityEngine;

namespace Simulator.CodeBlocks
{
    public class NOPBlock : CodeBlock
    {
        public NOPBlock(CodeBlock.Register regA,
                CodeBlock.Register regB,
                CodeBlock.Modifier mod = CodeBlock.Modifier.I)
                : base(mod, regA, regB) { }

        protected override void A(ISimulator simulator, int location)
        {
        
        }

        protected override void AB(ISimulator simulator, int location)
        {
        
        }

        protected override void B(ISimulator simulator, int location)
        {
        
        }

        protected override void BA(ISimulator simulator, int location)
        {
        
        }

        protected override void F(ISimulator simulator, int location)
        {
        
        }

        protected override void I(ISimulator simulator, int location)
        {
        }

        protected override void X(ISimulator simulator, int location)
        {
        }
    }
}
