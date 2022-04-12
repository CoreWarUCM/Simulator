using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.CodeBlocks
{
    public class MOVBlock : CodeBlock
    {
        public MOVBlock(int a, int b, CodeBlock.Modifier mod)
            : base(mod,
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, a),
            new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, b))
        {
        }
        public MOVBlock(CodeBlock.Register regA,
                        CodeBlock.Register regB,
                        CodeBlock.Modifier mod = CodeBlock.Modifier.I) 
                        : base(mod, regA, regB) { }

        protected override void A(ISimulator simulator, int location)
        {
            simulator.GetBlock(_regB.Get(),location)._regA.Set(simulator.GetBlock(_regA.Get(),location)._regA.Get());
        }

        protected override void AB(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.Get(),location);
            CodeBlock origin = simulator.GetBlock(_regA.Get(),location);
            dest._regB.Set(origin._regA.Get());
        }

        protected override void B(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.Get(),location);
            CodeBlock origin = simulator.GetBlock(_regA.Get(),location);
            dest._regB.Set(origin._regB.Get());
        }

        protected override void BA(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.Get(),location);
            CodeBlock origin = simulator.GetBlock(_regA.Get(),location);
            dest._regA.Set(origin._regB.Get());
        }

        protected override void F(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.Get(),location);
            CodeBlock origin = simulator.GetBlock(_regA.Get(),location);
            dest._regA.Set(origin._regA.Get());
            dest._regB.Set(origin._regB.Get());
        }

        protected override void I(ISimulator simulator, int location)
        {   
            CodeBlock origin = simulator.GetBlock(_regA.Get(),location);
            simulator.CreateBlock(origin, _regB.Get(),location);   
        }

        protected override void X(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.Get(),location);
            CodeBlock origin = simulator.GetBlock(_regA.Get(),location);
            dest._regA.Set(origin._regB.Get());
            dest._regB.Set(origin._regA.Get());
        }
    }
}
