using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsJMZBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }

        [Test]
        public void JMZA(){
            DATBlock block1 = new DATBlock(1, 0);
            sim.SetBlock(block1, 1, 0);

            JMZBlock block = new JMZBlock(-1, 1, CodeBlock.Modifier.BA);
            block.Execute(sim,2);
            Assert.AreEqual(1,sim.lastJump);
        }

        [Test]
        public void JMZB(){
            DATBlock block2 = new DATBlock(0, 1);
            sim.SetBlock(block2, 3, 0);

            JMZBlock block = new JMZBlock(1,-10);
            block.Execute(sim,10);
            Assert.AreEqual(11,sim.lastJump);
        }

        [Test]
        public void JMZF()
        {
            DATBlock block2 = new DATBlock(0, 0);
            sim.SetBlock(block2, 0, 0);

            JMZBlock block = new JMZBlock(5, -1);
            block.Execute(sim, 1);
            Assert.AreEqual(6, sim.lastJump);
        }
    }
}