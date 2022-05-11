using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simulator.CodeBlocks;
using UnityEngine;

namespace Simulator
{
    /// <summary>
    /// A simple factory static class that create blocks
    /// </summary>
    public class BlockFactory
    {
        /// <summary>
        /// String must be equal to the compile output of pmars
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public CodeBlock CreateBlock(string str)
        {
            //remove all the starting whitespace and the START indicator
            str = str.ToUpper().Replace("START", "").TrimStart();

            //We end up with a string with the folowing format:
            //{OP_CODE}.{MODIFIER} {addresing mode of the first operand}\t{value of first operand}, {addresing mode of the secondoperand} \t {value of second operand} 
            
            string opCode = str.Substring(0, 3);
            str = str.Substring(3);



            //ignore dot
            string modifier = str.Substring(1,2).TrimEnd();
            str = str.Substring(3).TrimStart();


            char addressingModeA = str[0];
            str = str.Substring(1).TrimStart();

            int splitIndex = str.IndexOf(',');
            if (!int.TryParse(str.Substring(0, splitIndex), out int valueA))
                throw new Exception("Malformed input string");


            //ignore comma
            str = str.Substring(splitIndex+1).TrimStart();

            char addressingModeB = str[0];
            str = str.Substring(1).Trim();

            if (!int.TryParse(str, out int valueB))
                throw new Exception("Malformed input string"+str);


            CodeBlock.Register.AddressingMode modeA = GetAddresingMode(addressingModeA);
            CodeBlock.Register.AddressingMode modeB = GetAddresingMode(addressingModeB);

            var regA = new CodeBlock.Register(modeA, valueA);
            var regB = new CodeBlock.Register(modeB, valueB);

            CodeBlock.Modifier mod = GetModifier(modifier);

            return CreateBlock(opCode, mod, regA,regB);
        }

        private static CodeBlock CreateBlock(string opCode, CodeBlock.Modifier mod, CodeBlock.Register regA, CodeBlock.Register regB)
        {
            switch (opCode)
            {
                case "MOV":
                    return new MOVBlock(regA, regB, mod);
                case "JMP":
                    return new JMPBlock(regA, regB, mod);
                case "DAT":
                    return new DATBlock(regA, regB, mod);
                case "ADD":
                    return new ADDBlock(regA, regB, mod);
                case "SUB":
                    return new SUBBlock(regA, regB, mod);
                default:
                    throw new Exception("unsupported block type {"+opCode+"}, las quejas a Ricky");
            }
            
        }

        static CodeBlock.Modifier GetModifier(string mod)
        {
            switch (mod)
            {
                case "I":
                    return CodeBlock.Modifier.I;
                case "X":
                    return CodeBlock.Modifier.X;
                case "F":
                    return CodeBlock.Modifier.F;
                case "A":
                    return CodeBlock.Modifier.A;
                case "B":
                    return CodeBlock.Modifier.B;
                case "BA":
                    return CodeBlock.Modifier.BA;
                case "AB":
                    return CodeBlock.Modifier.AB;
                default:
                    throw new Exception("unsupported modifer");
            }
        }
        static CodeBlock.Register.AddressingMode GetAddresingMode(char m)
        {
            switch (m)
            {
                case '#':
                    return CodeBlock.Register.AddressingMode.immediate;
                case '$':
                    return CodeBlock.Register.AddressingMode.direct;
                case '@':
                    return CodeBlock.Register.AddressingMode.BIndirect;
                case '<':
                    return CodeBlock.Register.AddressingMode.BPredecrement;
                case '{':
                    return CodeBlock.Register.AddressingMode.APredecrement;
                case '*':
                    return CodeBlock.Register.AddressingMode.AIndirect;
                case '>':
                    return CodeBlock.Register.AddressingMode.BPostincrement;
                case '}':
                    return CodeBlock.Register.AddressingMode.APostincrement;
                default:
                    throw new Exception("Unsupported addressing mode");
            }
        }
    }
}
