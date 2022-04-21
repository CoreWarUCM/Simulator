using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simulator;
using Simulator.CodeBlocks;
namespace Tests
{
    public class MockBattleSimulator : ISimulator
    {
        public void CreateBlock(CodeBlock block, int position, int origin)
        {
            new DATBlock(new CodeBlock.Register(), new CodeBlock.Register(), CodeBlock.Modifier.F);
        }

        public void CreateProcess(int warrior, int position, int origin)
        {
            
        }

        public CodeBlock GetBlock(int position, int origin)
        {
            return new DATBlock(0,0,CodeBlock.Modifier.F);
        }

        public void JumpTo(int destination)
        {
            
        }
        public void KillWarrior(){
            
        }
        public int ResolveAddress(int dest, int origin)
        {
            return -1;
        }
    }
}
