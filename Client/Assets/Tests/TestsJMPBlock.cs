using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsJMPBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }

        [Test]
        public void JMPToNegative(){
            JMPBlock block = new JMPBlock(-1);
            block.Execute(sim,2);
            Assert.AreEqual(1,sim.lastJump);
        }

        [Test]
        public void JMPToPositive(){
            JMPBlock block = new JMPBlock(1);
            block.Execute(sim,0);
            Assert.AreEqual(1,sim.lastJump);
        }
    }
}