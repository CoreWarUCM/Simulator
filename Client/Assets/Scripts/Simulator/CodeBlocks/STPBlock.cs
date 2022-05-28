﻿using UnityEngine;

namespace Simulator.CodeBlocks
{
    public class STPBlock : CodeBlock
    {
        public STPBlock(CodeBlock.Register regA,
                CodeBlock.Register regB,
                CodeBlock.Modifier mod = CodeBlock.Modifier.I)
                : base(mod, regA, regB) { }

        protected override void A(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(regA, 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            simulator.SetPrivateSpace(dest._regA.Value(), source._regA.Value());
        }

        protected override void AB(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(regA, 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            simulator.SetPrivateSpace(dest._regB.Value(), source._regA.Value());
        }

        protected override void B(ISimulator simulator, int location)
        {
            int regA = _regA.rGet(simulator, location);
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(regA, 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            simulator.SetPrivateSpace(dest._regB.Value(), source._regB.Value());
        }

        protected override void BA(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(regA, 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            simulator.SetPrivateSpace(dest._regA.Value(), source._regB.Value());

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
