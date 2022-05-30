using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsCMPBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }

        [Test]
        public void CMPA(){
            DATBlock block1 = new DATBlock(0, 16);
            sim.SetBlock(block1, 1, 0);

            CMPBlock block = new CMPBlock(0, 1, CodeBlock.Modifier.A);
            block.Execute(sim,0);
            Assert.AreEqual(2,sim.lastJump); 
        }

        [Test]
        public void CMPB(){
            DATBlock block1 = new DATBlock(0, 1);
            sim.SetBlock(block1, 1, 0);

            CMPBlock block = new CMPBlock(0, 1, CodeBlock.Modifier.B);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void CMPAB()
        {
            DATBlock block1 = new DATBlock(16, 0);
            sim.SetBlock(block1, 1, 0);

            CMPBlock block = new CMPBlock(0, 1, CodeBlock.Modifier.AB);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void CMPBA()
        {
            DATBlock block1 = new DATBlock(1, 18);
            sim.SetBlock(block1, 1, 0);

            CMPBlock block = new CMPBlock(0, 1, CodeBlock.Modifier.BA);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void CMPF()
        {
            DATBlock block1 = new DATBlock(0, 1);
            sim.SetBlock(block1, 1, 0);

            CMPBlock block = new CMPBlock(0, 1, CodeBlock.Modifier.F);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void CMPX()
        {
            DATBlock block1 = new DATBlock(1, 0);
            sim.SetBlock(block1, 1, 0);

            CMPBlock block = new CMPBlock(0, 1, CodeBlock.Modifier.X);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void CMPI()
        {
            sim.SetBlock(new CMPBlock(0, 1, CodeBlock.Modifier.I), 1, 0);

            CMPBlock block = new CMPBlock(0, 1, CodeBlock.Modifier.I);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }
    }
}