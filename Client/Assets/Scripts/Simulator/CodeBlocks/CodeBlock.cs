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
                        //throw new System.Exception("PANIC, NO SÉ CÓMO FUNCIONAN LAS CPUs");
                        return Value();
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
        
        public BaseMessage Execute(ISimulator simulator, int location)
        {
            switch (_mod)
            {
                case Modifier.A:
                    return this.A(simulator, location);
                case Modifier.B:
                    return this.B(simulator, location);
                case Modifier.AB:
                    return this.AB(simulator, location);
                case Modifier.BA:
                    return this.BA(simulator, location);
                case Modifier.F:
                    return this.F(simulator, location);
                case Modifier.X:
                    return this.X(simulator, location);
                case Modifier.I:
                    return this.I(simulator, location);
                default:
                    throw new System.Exception("Unreachable case");
                    return null;
            }
        }

        protected abstract BaseMessage A(ISimulator simulator, int location);
        protected abstract BaseMessage B(ISimulator simulator, int location);
        protected abstract BaseMessage AB(ISimulator simulator, int location);
        protected abstract BaseMessage BA(ISimulator simulator, int location);
        protected abstract BaseMessage F(ISimulator simulator, int location);
        protected abstract BaseMessage X(ISimulator simulator, int location);
        protected abstract BaseMessage I(ISimulator simulator, int location);
    }
}
