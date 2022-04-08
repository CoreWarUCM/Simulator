using UnityEngine;

using Simulator;
using Simulator.CodeBlocks;
namespace Simulator
{
    public abstract class CodeBlock : ICodeBlock
    {
        private Modfier _mod;
        public enum Modfier
        {
            A,B,AB,BA,
            F,X,I
        }
        public int _regA;
        public int _regB;

        public CodeBlock(Modfier mod, int regA, int regB)
        {
            _mod = mod;
            _regA = regA;
            _regB = regB;
        }
        
        public void Execute(ISimulator simulator, int location)
        {
            switch (_mod)
            {
                case Modfier.A:
                    this.A(simulator, location);
                    break;
                case Modfier.B:
                    this.B(simulator, location);
                    break;
                case Modfier.AB:
                    this.AB(simulator, location);
                    break;
                case Modfier.BA:
                    this.BA(simulator, location);
                    break;
                case Modfier.F:
                    this.F(simulator, location);
                    break;
                case Modfier.X:
                    this.X(simulator, location);
                    break;
                case Modfier.I:
                    this.I(simulator, location);
                    break;
            }
        }

        protected abstract void A(ISimulator simulator, int location);
        protected abstract void B(ISimulator simulator, int location);
        protected abstract void AB(ISimulator simulator, int location);
        protected abstract void BA(ISimulator simulator, int location);
        protected abstract void F(ISimulator simulator, int location);
        protected abstract void X(ISimulator simulator, int location);
        protected abstract void I(ISimulator simulator, int location);
    }
}
