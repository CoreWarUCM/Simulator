using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        public override CodeBlock Copy()
        {
            return new MOVBlock(new Register(_regA.Mode(), _regA.Value()), new Register(_regB.Mode(), _regB.Value()), _mod);
        }
        protected override void A(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            simulator.GetBlock(regB,0)._regA.Set(simulator.GetBlock(regA,0)._regA.Value());

            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void AB(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);
            CodeBlock dest = simulator.GetBlock(regB, 0);
            CodeBlock origin = simulator.GetBlock(regA,0);

            dest._regB.Set(origin._regA.Value());
            
            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void B(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock dest = simulator.GetBlock(regB, 0);
            CodeBlock origin = simulator.GetBlock(regA, 0);
            dest._regB.Set(origin._regB.Value());
            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void BA(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock dest = simulator.GetBlock(regB, 0);
            CodeBlock origin = simulator.GetBlock(regA, 0);
            dest._regA.Set(origin._regB.Value());
            
            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void F(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);

            CodeBlock dest = simulator.GetBlock(regB,0);
            CodeBlock origin = simulator.GetBlock(regA ,0);

            dest._regA.Set(origin._regA.Value());
            dest._regB.Set(origin._regB.Value());

            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void I(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);


            CodeBlock origin = simulator.GetBlock(regA,0);
            


            simulator.SetBlock(origin.Copy(), regB,0);
            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void X(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);
            int regA = _regA.rGet(simulator, location);
            CodeBlock dest = simulator.GetBlock(regB,0);
            CodeBlock origin = simulator.GetBlock(regA,0);
            dest._regA.Set(origin._regB.Value());
            dest._regB.Set(origin._regA.Value());

            simulator.SendMessage(new BlockModifyMessage(regB));
        }
    }
}
