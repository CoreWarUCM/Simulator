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
            throw new CodeBlock.UnsupportedModifierException("B");
        }

        protected override BaseMessage BA(ISimulator simulator, int location)
        {
            throw new CodeBlock.UnsupportedModifierException("BA");
        }

        protected override BaseMessage F(ISimulator simulator, int location)
        {
            simulator.KillWarrior();
            return new DeathMessage(location);
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
