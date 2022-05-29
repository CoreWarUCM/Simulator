using Simulator.CodeBlocks;
namespace Simulator
{
    public abstract class CodeBlock : ICodeBlock
    {
        protected Modifier _mod;
        public enum Modifier
        {
            A,B,AB,BA,
            F,X,I
        }

        public class UnsupportedModifierException : System.Exception{
            public UnsupportedModifierException(string mod):base($"The {mod} modifier is unsupported for this OpCode"){
                
            }
        };

        public class Register{
            public enum AddressingMode
            {
                immediate,//#
                direct,//$
                AIndirect,//*
                BIndirect,//@
                APredecrement,//{
                BPredecrement,//<
                APostincrement,//}
                BPostincrement,//>
            }
            private int _value;
            AddressingMode _mode;
            public Register(){
                _value = 0;
                _mode = AddressingMode.immediate;
            }
            public Register(AddressingMode mode, int value) {_value = AssureBetween0AndMax(value); _mode= mode;}
            
            public int Value(){
                return _value;
            }
            public AddressingMode Mode() { return _mode; }

            //Returns absolute position ar witch register is pointing, evaluating with side effects such as preincrement.
            public int rGet(ISimulator sim, int location)
            {

                CodeBlock target = sim.GetBlock(_value,location);
                switch (_mode)
                {   
                    case AddressingMode.immediate:
                        return location;
                    case AddressingMode.AIndirect:
                    {
                            return sim.ResolveAddress(target._regA.Value(), (sim.ResolveAddress(_value,location)));        
                    }
                    case AddressingMode.BIndirect:
                    {
                            return sim.ResolveAddress(target._regB.Value(), (sim.ResolveAddress(_value, location))); ;
                    }
                    case AddressingMode.APredecrement:{
                        target._regA.Add(-1);
                        int pos = sim.ResolveAddress(target._regA.Value(), (sim.ResolveAddress(_value, location)));
                        sim.SendMessage(new BlockModifyMessage((sim.ResolveAddress(_value, location))));
                        return pos;
                    }
                    case AddressingMode.BPredecrement:{
                        target._regB.Add(-1);
                        int pos = sim.ResolveAddress(target._regB.Value(), (sim.ResolveAddress(_value, location)));
                        sim.SendMessage(new BlockModifyMessage((sim.ResolveAddress(_value, location))));
                        return pos;        
                    }
                    case AddressingMode.APostincrement:{
                        int pos = sim.ResolveAddress(target._regA.Value(), (sim.ResolveAddress(_value, location)));
                        sim.SendMessage(new BlockModifyMessage((sim.ResolveAddress(_value, location))));
                        target._regA.Add(1);
                        return pos;                            
                    }
                    case AddressingMode.BPostincrement:{
                        int pos = sim.ResolveAddress(target._regB.Value(), (sim.ResolveAddress(_value, location)));
                        sim.SendMessage(new BlockModifyMessage((sim.ResolveAddress(_value, location))));
                        target._regB.Add(1);
                        return pos;
                    }

                    case AddressingMode.direct:
                        return sim.ResolveAddress(Value(), location);
                    default:
                        return sim.ResolveAddress(Value(),location);

                }
            }
            public void Set(int v)
            {
                _value = AssureBetween0AndMax(v);
            }

            private int AssureBetween0AndMax(int v)
            {
                return v >= 0 ? v % 8000 : 8000 - ((v * -1) % 8000); 
            }
            
            public void Add(int v)
            {
                _value = AssureBetween0AndMax(_value + v);
            }
            public void Sub(int v)
            {
                _value = AssureBetween0AndMax(_value - v);
            }
            public void Mul(int v)
            {
                _value = AssureBetween0AndMax(_value * v);
            }
            public void Div(int v)
            {
                _value = AssureBetween0AndMax(_value / v);
            }
            public void Mod(int v)
            {
                _value = AssureBetween0AndMax(_value % v);
            }
        }
        public Register _regA;
        public Register _regB;

        public CodeBlock(Modifier mod, Register regA, Register regB)
        {
            _mod = mod;
            _regA = regA;
            _regB = regB;
        }
        
        public virtual void Execute(ISimulator simulator, int location)
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
                default:
                    throw new System.Exception("Unreachable case");
            }
        }


        public abstract CodeBlock Copy();

        protected abstract void A(ISimulator simulator, int location);
        protected abstract void B(ISimulator simulator, int location);
        protected abstract void AB(ISimulator simulator, int location);
        protected abstract void BA(ISimulator simulator, int location);
        protected abstract void F(ISimulator simulator, int location);
        protected abstract void X(ISimulator simulator, int location);
        protected abstract void I(ISimulator simulator, int location);
    }
}
