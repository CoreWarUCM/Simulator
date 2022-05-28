using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsSNEBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }

        [Test]
        public void SNEA(){
            DATBlock block1 = new DATBlock(10, 16);
            sim.SetBlock(block1, 1, 0);

            SNEBlock block = new SNEBlock(0, 1, CodeBlock.Modifier.A);
            block.Execute(sim,0);
            Assert.AreEqual(2,sim.lastJump); 
        }

        [Test]
        public void SNEB(){
            DATBlock block1 = new DATBlock(0, 2);
            sim.SetBlock(block1, 1, 0);

            SNEBlock block = new SNEBlock(0, 1, CodeBlock.Modifier.B);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void SNEAB()
        {
            DATBlock block1 = new DATBlock(0, 20);
            sim.SetBlock(block1, 1, 0);

            SNEBlock block = new SNEBlock(0, 1, CodeBlock.Modifier.AB);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void SNEBA()
        {
            DATBlock block1 = new DATBlock(2, 0);
            sim.SetBlock(block1, 1, 0);

            SNEBlock block = new SNEBlock(0, 1, CodeBlock.Modifier.BA);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void SNEF()
        {
            DATBlock block1 = new DATBlock(1, 2);
            sim.SetBlock(block1, 1, 0);

            SNEBlock block = new SNEBlock(0, 1, CodeBlock.Modifier.F);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void SNEX()
        {
            DATBlock block1 = new DATBlock(2, 1);
            sim.SetBlock(block1, 1, 0);

            SNEBlock block = new SNEBlock(0, 1, CodeBlock.Modifier.X);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void SNEI()
        {
            DATBlock block1 = new DATBlock(1, 2);
            sim.SetBlock(block1, 1, 0);


            SNEBlock block = new SNEBlock(0, 1, CodeBlock.Modifier.I);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(2, sim.lastJump);
        }

        [Test]
        public void SNEFDontJumpOnEquals()
        {
            DATBlock block1 = new DATBlock(0, 1);
            sim.SetBlock(block1, 1, 0);


            SNEBlock block = new SNEBlock(0, 1, CodeBlock.Modifier.I);
            sim.SetBlock(block, 0, 0);
            block.Execute(sim, 0);
            Assert.AreEqual(-1, sim.lastJump);
        }
    }
}