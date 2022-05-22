using UnityEngine;

namespace Simulator.CodeBlocks
{
    public class MODBlock : CodeBlock
    {
        public MODBlock(CodeBlock.Register regA,
                CodeBlock.Register regB,
                CodeBlock.Modifier mod = CodeBlock.Modifier.I)
                : base(mod, regA, regB) { }

        public override void Execute(ISimulator simulator, int location)
        {
            try
            {
                base.Execute(simulator, location);
            }
            catch (System.DivideByZeroException)
            {
                simulator.KillVirus();
                simulator.SendMessage(new DeathMessage(location));
                Debug.Log("OJO QUE MODIDIMOS ENTRE 0");
            }
        }

        protected override void A(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            dest._regA.Mod(source._regA.Value());

            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void AB(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            int v = source._regA.Value();
            dest._regB.Mod(v);

            simulator.SendMessage(new BlockModifyMessage(simulator.ResolveAddress(regB, 0)));
        }

        protected override void B(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            dest._regB.Mod(source._regB.Value());

            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void BA(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            dest._regA.Mod(source._regB.Value());

            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void F(ISimulator simulator, int location)
        {
            this.I(simulator, location);
        }

        protected override void I(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            dest._regA.Mod(source._regA.Value());
            dest._regB.Mod(source._regB.Value());

            simulator.SendMessage(new BlockModifyMessage(regB));
        }

        protected override void X(ISimulator simulator, int location)
        {
            int regB = _regB.rGet(simulator, location);

            CodeBlock source = simulator.GetBlock(_regA.rGet(simulator, location), 0);
            CodeBlock dest = simulator.GetBlock(regB, 0);

            dest._regA.Mod(source._regB.Value());
            dest._regB.Mod(source._regA.Value());

            simulator.SendMessage(new BlockModifyMessage(regB));
        }
    }
}
