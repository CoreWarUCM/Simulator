using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsDJNBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }

        [Test]
        public void DJNA(){
            DATBlock block1 = new DATBlock(1, 1);
            sim.SetBlock(block1, 0, 0);

            DJNBlock block = new DJNBlock(5, -2, CodeBlock.Modifier.BA);
            block.Execute(sim,2);
            Assert.AreEqual(-1,sim.lastJump);  //Doesn't jump beacuse 1-1 == 0
        }

        [Test]
        public void DJNB(){
            DATBlock block2 = new DATBlock(0,-50); 
            sim.SetBlock(block2, 3, 0);

            DJNBlock block = new DJNBlock(1,-7);
            block.Execute(sim,10); 
            Assert.AreEqual(11,sim.lastJump); // -1 means it dind't jump
        }

        [Test]
        public void DJNF()
        {
            DATBlock block2 = new DATBlock(-5, 4);
            sim.SetBlock(block2, 0, 0);

            DJNBlock block = new DJNBlock(5, -1);
            block.Execute(sim, 1);
            Assert.AreEqual(6, sim.lastJump); // -1 means it dind't jump
        }
    }
}