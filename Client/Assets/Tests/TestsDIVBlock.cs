using Simulator;
using NUnit.Framework;
using Simulator.CodeBlocks;
using UnityEngine;

namespace Tests
{
    class TestsDIVBlock
    {

        private MockMemorySimulator sim;
        private DATBlock target;

        [SetUp]
        public void SetUpCommonMemory()
        {
            sim = new MockMemorySimulator();
            target = new DATBlock(3, 5, CodeBlock.Modifier.F);
            sim.SetBlock(target, 2, 0);
        }

        [Test]
        public void DivI()
        {
            DIVBlock block = BlockFactory.CreateBlock("DIV.I $1, $2") as DIVBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F #1, @1") as DATBlock;
            
            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);
            
            Assert.AreEqual(3 / 1,target._regA.Value());
            Assert.AreEqual(5 / 1, target._regB.Value());
        }

        [Test]
        public void DivF()
        {
            DIVBlock block = BlockFactory.CreateBlock("DIV.F $1, $2") as DIVBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3/4, target._regA.Value());
            Assert.AreEqual(5/3, target._regB.Value());
        }

        [Test]
        public void DivX()
        {
            DIVBlock block = BlockFactory.CreateBlock("DIV.X $1, $2") as DIVBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3/3, target._regA.Value());
            Assert.AreEqual(5/4, target._regB.Value());
        }

        [Test]
        public void DivA()
        {
            DIVBlock block = BlockFactory.CreateBlock("DIV.A $1, $2") as DIVBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3/4, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }

        [Test]
        public void DivB()
        {
            DIVBlock block = BlockFactory.CreateBlock("DIV.B $1, $2") as DIVBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(5/3, target._regB.Value());
        }

        [Test]
        public void DivAB()
        {
            DIVBlock block = BlockFactory.CreateBlock("DIV.AB $1, $2") as DIVBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(5/4, target._regB.Value());
        }
        [Test]
        public void DivBA()
        {
            DIVBlock block = BlockFactory.CreateBlock("DIV.BA $1, $2") as DIVBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3/3, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }
    }
}
