using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Simulator.CodeBlocks
{
    public class JMNBlock : CodeBlock
    {
        public JMNBlock(int a, int b = 0, CodeBlock.Modifier mod = CodeBlock.Modifier.B)
            : base(mod,
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, a),
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, b))
        {
        }
        public override CodeBlock Copy()
        {
            return new JMNBlock(new Register(_regA.Mode(), _regA.Value()), new Register(_regB.Mode(), _regB.Value()), _mod);
        }
        private void Jump(ISimulator simulator, int addr)
        {
            simulator.JumpTo(addr);
            simulator.SendMessage(new JumpMessage(addr));
        }

        public JMNBlock(CodeBlock.Register regA,
                        CodeBlock.Register regB = null,
                        CodeBlock.Modifier mod = CodeBlock.Modifier.B) 
                        : base(mod, regA, regB) { }

        protected override void A(ISimulator simulator, int location)
        {
            int value = _regA.rGet(simulator, location);
            int target = _regB.rGet(simulator, location);

            if (simulator.GetBlock(target, 0)._regA.Value() != 0)
                Jump(simulator, value);
        }

        protected override void AB(ISimulator simulator, int location)
        {
            B(simulator, location);
        }

        protected override void B(ISimulator simulator, int location)
        {
            int value = _regA.rGet(simulator, location);
            int target = _regB.rGet(simulator, location);

            if (simulator.GetBlock(target,0)._regB.Value() != 0)
                Jump(simulator,value);
        }

        protected override void BA(ISimulator simulator, int location)
        {
            A(simulator, location);
        }

        protected override void F(ISimulator simulator, int location)
        {
            int value = _regA.rGet(simulator, location);
            int target = _regB.rGet(simulator, location);

            if (simulator.GetBlock(target, 0)._regA.Value() != 0 && simulator.GetBlock(target, 0)._regB.Value() != 0)
                Jump(simulator, value);
        }

        protected override void I(ISimulator simulator, int location)
        {
            F(simulator , location);
        }

        protected override void X(ISimulator simulator, int location)
        {
            F(simulator, location);
        }
    }
}
