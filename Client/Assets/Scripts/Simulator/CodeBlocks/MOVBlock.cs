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
            simulator.GetBlock(_regB.rGet(simulator, location),location)._regA.Set(simulator.GetBlock(_regA.rGet(simulator, location),location)._regA.rGet(simulator, location));
        }

        protected override void AB(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.rGet(simulator, location),location);
            CodeBlock origin = simulator.GetBlock(_regA.rGet(simulator, location),location);
            dest._regB.Set(origin._regA.rGet(simulator, location));
        }

        protected override void B(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.rGet(simulator, location),location);
            CodeBlock origin = simulator.GetBlock(_regA.rGet(simulator, location),location);
            dest._regB.Set(origin._regB.rGet(simulator, location));
        }

        protected override void BA(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.rGet(simulator, location),location);
            CodeBlock origin = simulator.GetBlock(_regA.rGet(simulator, location),location);
            dest._regA.Set(origin._regB.rGet(simulator, location));
        }

        protected override void F(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.rGet(simulator, location),location);
            CodeBlock origin = simulator.GetBlock(_regA.rGet(simulator, location),location);
            dest._regA.Set(origin._regA.rGet(simulator, location));
            dest._regB.Set(origin._regB.rGet(simulator, location));
        }

        protected override void I(ISimulator simulator, int location)
        {   
            CodeBlock origin = simulator.GetBlock(_regA.rGet(simulator, location),location);
            simulator.CreateBlock(origin, _regB.rGet(simulator, location),location);   
        }

        protected override void X(ISimulator simulator, int location)
        {
            CodeBlock dest = simulator.GetBlock(_regB.rGet(simulator, location),location);
            CodeBlock origin = simulator.GetBlock(_regA.rGet(simulator, location),location);
            dest._regA.Set(origin._regB.rGet(simulator, location));
            dest._regB.Set(origin._regA.rGet(simulator, location));
        }
    }
}
