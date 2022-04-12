using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simulator;
using Simulator.CodeBlocks;
namespace Tests
{
    public class MockMemorySimulator : ISimulator
    {
        public int CountKills {get; private set;}

        private List<CodeBlock> blocks = new List<CodeBlock>(8000);


        public MockMemorySimulator(){
            for(int i  = 0; i<blocks.Capacity;i++)
                blocks.Add(new DATBlock(0,0,CodeBlock.Modifier.F));
        }
        public void CreateBlock(CodeBlock block, int position, int origin)
        {
            blocks[position] = block;
        }

        public void CreateProcess(int warrior, int position, int origin)
        {
            
        }

        public CodeBlock GetBlock(int position, int origin)
        {
            return blocks[position];
        }

        public void JumpTo(int destination,int origin)
        {
            
        }
        public void KillWarrior(){
            CountKills++;
        }
    }
}
