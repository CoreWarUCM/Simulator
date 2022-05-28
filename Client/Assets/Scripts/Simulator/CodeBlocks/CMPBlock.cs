using UnityEngine;

namespace Simulator.CodeBlocks
{
    public class CMPBlock : CodeBlock
    {
        public CMPBlock(int a, int b, CodeBlock.Modifier mod)
      : base(mod,
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, a),
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, b))
        {
        }
        public CMPBlock(CodeBlock.Register regA,
                CodeBlock.Register regB,
                CodeBlock.Modifier mod = CodeBlock.Modifier.I)
                : base(mod, regA, regB) { }

        public override CodeBlock Copy()
        {
            return new CMPBlock(new Register(_regA.Mode(), _regA.Value()), new Register(_regB.Mode(), _regB.Value()), _mod);
        }
        private void Jump(ISimulator simulator, int location)
        {
            int absolute = simulator.ResolveAddress(2, location);
            simulator.JumpTo(absolute);
            simulator.SendMessage(new JumpMessage(absolute));
        }

        protected override void A(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            if (source._regA.Value() == dest._regA.Value())
                Jump(simulator, location);
        }

        protected override void AB(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            if (source._regA.Value() == dest._regB.Value())
                Jump(simulator, location);
        }

        protected override void B(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            if (source._regB.Value() == dest._regB.Value())
                Jump(simulator, location);
        }

        protected override void BA(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            if (source._regB.Value() == dest._regA.Value())
                Jump(simulator, location);
        }

        protected override void F(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            if (source._regA.Value() == dest._regA.Value() && source._regB.Value() == dest._regB.Value())
                Jump(simulator, location);
        }

        protected override void I(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            if (   source.GetType().Name == dest.GetType().Name
                && source._regA.Value() == dest._regA.Value()
                && source._regA.Mode() == dest._regA.Mode()
                && source._regB.Value() == dest._regB.Value()
                && source._regB.Mode() == dest._regB.Mode()
                )
                Jump(simulator, location);
        }

        protected override void X(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            if (source._regA.Value() == dest._regB.Value() && source._regB.Value() == dest._regA.Value())
                Jump(simulator, location);
        }
    }
}
