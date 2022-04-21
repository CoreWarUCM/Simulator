using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.CodeBlocks
{
    public class JMPBlock : CodeBlock
    {
        public JMPBlock(int a, int b = 0, CodeBlock.Modifier mod = CodeBlock.Modifier.B)
            : base(mod,
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, a),
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, b))
        {
        }

        public JMPBlock(CodeBlock.Register regA,
                        CodeBlock.Register regB = null,
                        CodeBlock.Modifier mod = CodeBlock.Modifier.B) 
                        : base(mod, regA, regB) { }

        protected override void A(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("A");
        }

        protected override void AB(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("AB");
        }

        protected override void B(ISimulator simulator, int location)
        {
            simulator.JumpTo(simulator.ResolveAddress(_regA.rGet(simulator,location),location));
        }

        protected override void BA(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("BA");
        }

        protected override void F(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("F");
        }

        protected override void I(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("I");
        }

        protected override void X(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("X");
        }
    }
}
