using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEditor;

using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class MockMemorySimulator : ISimulator
    {
        public int CountKills {get; private set;}

        private List<CodeBlock> blocks = new List<CodeBlock>(8000);

        public int lastJump = -1;

        public MockMemorySimulator(){
            for(int i  = 0; i<blocks.Capacity;i++)
                blocks.Add(new DATBlock(0,0,CodeBlock.Modifier.F));
        }
        public void CreateBlock(CodeBlock block, int position, int origin)
        {
            //this is a mock, we are not testing over 8000
            blocks[position] = block;
        }

        public void CreateProcess(int virus, int position, int origin)
        {
            
        }

        public CodeBlock GetBlock(int position, int origin)
        {
            //this is a mock, we are not testing over 8000
            if(position+origin<0)
                Debug.Log($"{position},{origin}");
            return blocks[position+origin];
        }

        public void JumpTo(int destination)
        {
            lastJump = destination;
        }
        public void KillVirus(){
            CountKills++;
        }
        public int ResolveAddress(int dest, int origin)
        {
            //this is a mock, we are not testing over 8000
            return dest+origin;
        }
        public void SendMessage(BaseMessage message) { }
    }
}
