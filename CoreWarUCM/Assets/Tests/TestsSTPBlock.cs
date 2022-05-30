using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsSTPBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
            int count = 0;
            sim.SetBlock(BlockFactory.CreateBlock("DAT.I $0, $1"),count++,0);
            sim.SetBlock(BlockFactory.CreateBlock("DAT.I $2, $3"),count++,0);
            sim.SetBlock(BlockFactory.CreateBlock("DAT.I $4, $5"),count++,0);
        }

        [Test]
        public void STPBDirect()
        {
            CodeBlock block = BlockFactory.CreateBlock("STP.AB #3, #5");
            sim.SetBlock(block, 3, 0);
            block.Execute(sim, 3);
            Assert.AreEqual(3, sim.GetPrivateSpace(5));
        }

        [Test]
        public void STPB(){
            CodeBlock block = BlockFactory.CreateBlock("STP.B $-3, $-1");
            sim.SetBlock(block, 3, 0);
            block.Execute(sim, 3);
            Assert.AreEqual(1, sim.GetPrivateSpace(5));
        }

        [Test]
        public void STPA()
        {
            CodeBlock block = BlockFactory.CreateBlock("STP.A $-2, $-1");
            sim.SetBlock(block, 3, 0);
            block.Execute(sim, 3);
            Assert.AreEqual(2, sim.GetPrivateSpace(4));
        }

        [Test]
        public void STPAB()
        {
            CodeBlock block = BlockFactory.CreateBlock("STP.AB $-2, $-3");
            sim.SetBlock(block, 3, 0);
            block.Execute(sim, 3);
            Assert.AreEqual(2, sim.GetPrivateSpace(1));
        }

        [Test]
        public void STPBA()
        {
            CodeBlock block = BlockFactory.CreateBlock("STP.BA $-3, $-2");
            sim.SetBlock(block, 3, 0);
            block.Execute(sim, 3);
            Assert.AreEqual(1, sim.GetPrivateSpace(2));
        }


    }
}