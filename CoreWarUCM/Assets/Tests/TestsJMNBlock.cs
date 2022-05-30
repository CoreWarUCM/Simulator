using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsJMNBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }

        [Test]
        public void JMNA(){
            DATBlock block1 = new DATBlock(1, -50);
            sim.SetBlock(block1, 0, 0);

            JMNBlock block = new JMNBlock(5, -2, CodeBlock.Modifier.BA);
            block.Execute(sim,2);
            Assert.AreEqual(7,sim.lastJump); 
        }

        [Test]
        public void JMNB(){
            DATBlock block2 = new DATBlock(0,-50); 
            sim.SetBlock(block2, 3, 0);

            JMNBlock block = new JMNBlock(1,-7);
            block.Execute(sim,10); 
            Assert.AreEqual(11,sim.lastJump); // -1 means it dind't jump
        }

        [Test]
        public void JMNF()
        {
            DATBlock block2 = new DATBlock(-5, 4);
            sim.SetBlock(block2, 0, 0);

            JMNBlock block = new JMNBlock(5, -1);
            block.Execute(sim, 1);
            Assert.AreEqual(6, sim.lastJump); // -1 means it dind't jump
        }
    }
}