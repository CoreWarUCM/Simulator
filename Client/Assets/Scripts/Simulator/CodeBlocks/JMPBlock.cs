using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        public override CodeBlock Copy()
        {
            return new JMPBlock(new Register(_regA.Mode(), _regA.Value()), new Register(_regB.Mode(), _regB.Value()), _mod);
        }
        protected override void A(ISimulator simulator, int location)
        {
            B(simulator, location);
        }

        protected override void AB(ISimulator simulator, int location)
        {
            B(simulator, location);
        }

        protected override void B(ISimulator simulator, int location)
        {
            int addr = _regA.rGet(simulator, location);
            _regB.rGet(simulator, location);
            simulator.JumpTo(addr);
            simulator.SendMessage(new JumpMessage(addr));
            if(addr<location)
                Debug.Log("JUMP TO " + addr);
        }

        protected override void BA(ISimulator simulator, int location)
        {
            B(simulator, location);
        }

        protected override void F(ISimulator simulator, int location)
        {
            B(simulator, location);
        }

        protected override void I(ISimulator simulator, int location)
        {
            B(simulator, location);
        }

        protected override void X(ISimulator simulator, int location)
        {
            B(simulator, location);
        }
    }
}
