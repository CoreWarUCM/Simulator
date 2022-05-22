using Simulator;
using NUnit.Framework;
using Simulator.CodeBlocks;
using UnityEngine;

namespace Tests
{
    class TestsADDBlock
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
        public void AddI()
        {
            ADDBlock block = BlockFactory.CreateBlock("ADD.I $1, $2") as ADDBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F #1, @1") as DATBlock;
            
            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);
            
            Assert.AreEqual(4,target._regA.Value());
            Assert.AreEqual(6, target._regB.Value());
        }

        [Test]
        public void AddF()
        {
            ADDBlock block = BlockFactory.CreateBlock("ADD.F $1, $2") as ADDBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(7, target._regA.Value());
            Assert.AreEqual(8, target._regB.Value());
        }

        [Test]
        public void AddX()
        {
            ADDBlock block = BlockFactory.CreateBlock("ADD.X $1, $2") as ADDBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(6, target._regA.Value());
            Assert.AreEqual(9, target._regB.Value());
        }

        [Test]
        public void AddA()
        {
            ADDBlock block = BlockFactory.CreateBlock("ADD.A $1, $2") as ADDBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(7, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }

        [Test]
        public void AddB()
        {
            ADDBlock block = BlockFactory.CreateBlock("ADD.B $1, $2") as ADDBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(8, target._regB.Value());
        }

        [Test]
        public void AddAB()
        {
            ADDBlock block = BlockFactory.CreateBlock("ADD.AB $1, $2") as ADDBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(9, target._regB.Value());
        }
        [Test]
        public void AddBA()
        {
            ADDBlock block = BlockFactory.CreateBlock("ADD.BA $1, $2") as ADDBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(6, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }
    }
}
