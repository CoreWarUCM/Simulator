using UnityEngine;

namespace Simulator.CodeBlocks
{
    public class LDPBlock : CodeBlock
    {
        public LDPBlock(CodeBlock.Register regA,
                CodeBlock.Register regB,
                CodeBlock.Modifier mod = CodeBlock.Modifier.I)
                : base(mod, regA, regB) { }
        public override CodeBlock Copy()
        {
            return new LDPBlock(new Register(_regA.Mode(), _regA.Value()), new Register(_regB.Mode(), _regB.Value()), _mod);
        }
        protected override void A(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(regA, 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);


            int value = simulator.GetPrivateSpace(source._regA.Value());
            dest._regA.Set(value);
        }

        protected override void AB(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(regA, 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            int value = simulator.GetPrivateSpace(source._regA.Value());
            dest._regB.Set(value);
        }

        protected override void B(ISimulator simulator, int location)
        {
            int regA = _regA.rGet(simulator, location);
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(regA, 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            int value = simulator.GetPrivateSpace(source._regB.Value());
            dest._regB.Set(value);
        }

        protected override void BA(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(regA, 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            int value = simulator.GetPrivateSpace(source._regB.Value());
            dest._regA.Set(value);
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
