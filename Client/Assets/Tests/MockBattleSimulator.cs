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
        int[] pspace = new int[8000/16];

        public void SetBlock(CodeBlock block, int position, int origin)
        {
            new DATBlock(new CodeBlock.Register(), new CodeBlock.Register(), CodeBlock.Modifier.F);
        }

        public void CreateProcess(int virus, int position, int origin)
        {
            
        }

        public CodeBlock GetBlock(int position, int origin)
        {
            return new DATBlock(0,0,CodeBlock.Modifier.F);
        }

        public void JumpTo(int destination)
        {
            
        }
        public void KillVirus(){
            
        }
        public int ResolveAddress(int dest, int origin)
        {
            return -1;
        }
        public void SendMessage(BaseMessage message) { }

        public void SetPrivateSpace(int location, int value)
        {
            pspace[location] = value;
        }

        public int GetPrivateSpace(int location)
        {
            return pspace[location];
        }
    }
}
