using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.CodeBlocks
{
    public class DATBlock : CodeBlock
    {
        public DATBlock(int a, int b, CodeBlock.Modifier mod = CodeBlock.Modifier.F)
            : base(mod,
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, a),
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, b))
        {
        }

        public DATBlock(CodeBlock.Register regA,
                        CodeBlock.Register regB,
                        CodeBlock.Modifier mod = CodeBlock.Modifier.F) 
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
            throw new CodeBlock.UnsupportedModifierException("B");
        }

        protected override void BA(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("BA");
        }

        protected override void F(ISimulator simulator, int location)
        {
            simulator.KillVirus();
            simulator.SendMessage(new DeathMessage(location));
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
