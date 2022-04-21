using UnityEngine;
using UnityEditor;

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
            public Register(AddressingMode mode, int value) {_value = value; _mode= mode;}
            
            public int Value(){
                return _value;
            }
            public int rGet(ISimulator sim, int location)
            {

                CodeBlock target = sim.GetBlock(_value,location);
                switch (_mode)
                {   
                    case AddressingMode.immediate:
                        return throw new System.Exception("PANIC, NO SÉ CÓMO FUNCIONAN LAS CPUs");
                    case AddressingMode.AIndirect:
                    {
                        /*
                        [0] MOV.I *3,3
                        [1] DAT    ,
                        [2] DAT    ,
                        [3] DAT    2,0
                        [4] DAT    ,
                        [5] DAT    ,
                        */
                        return sim.ResolveAddress(target._regA.Value(), (sim.ResolveAddress(_value,location)));        
                    }
                    case AddressingMode.BIndirect:
                    {
                        return sim.ResolveAddress(target._regB.Value(), (sim.ResolveAddress(_value,location)));        
                    }
                    case AddressingMode.APredecrement:{
                        _value--;
                        target = sim.GetBlock(_value,location);
                        return sim.ResolveAddress(target._regA.Value(), (sim.ResolveAddress(_value,location)));        
                    }
                    case AddressingMode.BPredecrement:{
                        _value--;
                        target = sim.GetBlock(_value,location);
                        return sim.ResolveAddress(target._regB.Value(), (sim.ResolveAddress(_value,location)));        
                    }
                    case AddressingMode.APostincrement:{
                        return sim.ResolveAddress(target._regA.Value(), (sim.ResolveAddress(_value++,location)));        
                    }
                    case AddressingMode.BPostincrement:{
                        return sim.ResolveAddress(target._regB.Value(), (sim.ResolveAddress(_value++,location)));        
                    }

                    case AddressingMode.direct:
                    default:
                        return Value();

                }
            }
            public void Set(int v)
            {
                _value = v;
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
