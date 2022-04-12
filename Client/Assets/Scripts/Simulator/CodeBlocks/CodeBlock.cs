using UnityEngine;

using Simulator;
using Simulator.CodeBlocks;
namespace Simulator
{
    public abstract class CodeBlock : ICodeBlock
    {
        private Modifier _mod;
        public enum Modifier
        {
            A,B,AB,BA,
            F,X,I
        }

        public class UnsupportedModifierException : System.Exception{
            public UnsupportedModifierException(string mod):base($"The {mod} modifier is unsupported for this OpCode"){
                
            }
        };
        public int _regA;
        public int _regB;

        public CodeBlock(Modifier mod, int regA, int regB)
        {
            _mod = mod;
            _regA = regA;
            _regB = regB;
        }
        
        public void Execute(ISimulator simulator, int location)
        {
            switch (_mod)
            {
                case Modifier.A:
                    this.A(simulator, location);
                    break;
                case Modifier.B:
                    this.B(simulator, location);
                    break;
                case Modifier.AB:
                    this.AB(simulator, location);
                    break;
                case Modifier.BA:
                    this.BA(simulator, location);
                    break;
                case Modifier.F:
                    this.F(simulator, location);
                    break;
                case Modifier.X:
                    this.X(simulator, location);
                    break;
                case Modifier.I:
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
