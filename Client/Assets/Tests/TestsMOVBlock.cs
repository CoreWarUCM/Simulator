using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsMOVBlock{
        
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
            sim.CreateBlock(new DATBlock(3,8,CodeBlock.Modifier.F),1,0);
        }

        [Test]
        public void MOVAMovesASourceToADest(){
            MOVBlock block = new MOVBlock(1,42,CodeBlock.Modifier.A);
            block.Execute(sim,0);
            Assert.That(sim.GetBlock(42,0)._regA.rGet(sim, 42) == 3);
        }
        
        [Test]
        public void MOVBMovesBSourceToBDest(){
            MOVBlock block = new MOVBlock(1,42,CodeBlock.Modifier.B);
            block.Execute(sim,0);
            Assert.That(sim.GetBlock(42,0)._regB.rGet(sim, 42) == 8);
        }

        [Test]
        public void MOVABMovesASourceToBDest(){
            MOVBlock block = new MOVBlock(1,14,CodeBlock.Modifier.AB);
            block.Execute(sim,0);
            Assert.That(sim.GetBlock(14,0)._regB.rGet(sim, 14) == 3);
        }

        [Test]
        public void MOVBAMovesBSourceToADest(){
            MOVBlock block = new MOVBlock(1,14,CodeBlock.Modifier.BA);
            block.Execute(sim,0);
            Assert.That(sim.GetBlock(14,0)._regA.rGet(sim, 14) == 8);
        }

        [Test]
        public void MOVFMovesABSourceToABDest(){
            MOVBlock block = new MOVBlock(1,20,CodeBlock.Modifier.F);
            block.Execute(sim,0);
            Assert.That(sim.GetBlock(20,0)._regA.rGet(sim, 20) == 3);
            Assert.That(sim.GetBlock(20,0)._regB.rGet(sim, 20) == 8);
        }

        [Test]
        public void MOVXMovesABSourceToABDest(){
            MOVBlock block = new MOVBlock(1,20,CodeBlock.Modifier.X);
            block.Execute(sim,0);
            Assert.That(sim.GetBlock(20,0)._regA.rGet(sim, 20) == 8);
            Assert.That(sim.GetBlock(20,0)._regB.rGet(sim, 20) == 3);
        }

        [Test]
        public void MOVIMovesourceToDest(){
            CodeBlock block = new MOVBlock(0,20,CodeBlock.Modifier.I);
            sim.CreateBlock(block,0,0);
            block.Execute(sim,0);

            Assert.That(sim.GetBlock(20,0)._regA.rGet(sim, 20) == 0);
            Assert.That(sim.GetBlock(20,0)._regB.rGet(sim, 20) == 20);
            Assert.IsInstanceOf<MOVBlock>(sim.GetBlock(20,0)); 
        }
    }
}