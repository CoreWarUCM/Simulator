using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;
using UnityEngine;

namespace Tests
{
    public class TestsLDPBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
            int count = 0;
            sim.SetBlock(BlockFactory.CreateBlock("DAT.I $0, $1"),count++,0);
            sim.SetBlock(BlockFactory.CreateBlock("DAT.I $2, $3"),count++,0);
            sim.SetBlock(BlockFactory.CreateBlock("DAT.I $4, $5"),count++,0);
            sim.SetBlock(BlockFactory.CreateBlock("STP.AB #4, #5"),count++,0);
            sim.GetBlock(count - 1, 0).Execute(sim, count - 1);
            sim.SetBlock(BlockFactory.CreateBlock("STP.AB #123, #4"), count++,0);
            sim.GetBlock(count - 1, 0).Execute(sim, count - 1);
        }

        [Test]
        public void LDPB(){
            CodeBlock block = BlockFactory.CreateBlock("LDP.B $-2, $1");
            sim.SetBlock(block, 5, 0);
            block.Execute(sim, 5);
            CodeBlock target = sim.GetBlock(1, 5);
            Debug.Log(sim.GetPrivateSpace(5));
            Assert.AreEqual(target._regB.Value(), sim.GetPrivateSpace(5));
        }

        [Test]
        public void LDPA()
        {
            CodeBlock block = BlockFactory.CreateBlock("LDP.A $-2, $1");
            sim.SetBlock(block, 5, 0);
            block.Execute(sim, 5);
            CodeBlock target = sim.GetBlock(1, 5);
            Debug.Log(target._regA.Value());
            Assert.AreEqual(target._regA.Value(), sim.GetPrivateSpace(4));
        }

        [Test]
        public void LDPAB()
        {
            CodeBlock block = BlockFactory.CreateBlock("LDP.AB $-2, $1");
            sim.SetBlock(block, 5, 0);
            block.Execute(sim, 5);
            CodeBlock target = sim.GetBlock(1, 5);
            Debug.Log(target._regB.Value());
            Assert.AreEqual(target._regB.Value(), sim.GetPrivateSpace(4));
        }

        [Test]
        public void LDPBA()
        {
            CodeBlock block = BlockFactory.CreateBlock("LDP.BA $-2, $1");
            sim.SetBlock(block, 5, 0);
            block.Execute(sim, 5);
            CodeBlock target = sim.GetBlock(1, 5);
            Debug.Log(target._regA.Value());
            Assert.AreEqual(target._regA.Value(), sim.GetPrivateSpace(5));
        }

    }
}