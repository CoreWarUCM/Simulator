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

        protected override BaseMessage A(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("A");
        }

        protected override BaseMessage AB(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("AB");
        }

        protected override BaseMessage B(ISimulator simulator, int location)
        {
            int addr = simulator.ResolveAddress(_regA.rGet(simulator, location), location);
            simulator.JumpTo(addr);
            return new JumpMessage(addr);
        }

        protected override BaseMessage BA(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("BA");
        }

        protected override BaseMessage F(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("F");
        }

        protected override BaseMessage I(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("I");
        }

        protected override BaseMessage X(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("X");
        }
    }
}
