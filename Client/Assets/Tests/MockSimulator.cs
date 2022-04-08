using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simulator;
using Simulator.CodeBlocks;
namespace Tests
{
    public class MockSimulator : ISimulator
    {
        public void CreateBlock(CodeBlock block, int position, int origin)
        {
            new DATBlock();
        }

        public void CreateProcess(int warrior, int position, int origin)
        {
            
        }

        public CodeBlock GetBlock(int position, int origin)
        {
            return new DATBlock();
        }

        public void JumpTo(int destination,int origin)
        {
            
        }
    }
}
