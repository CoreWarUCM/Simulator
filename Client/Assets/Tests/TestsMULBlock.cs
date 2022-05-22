using Simulator;
using NUnit.Framework;
using Simulator.CodeBlocks;
using UnityEngine;

namespace Tests
{
    class TestsMULBlock
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
        public void MulI()
        {
            MULBlock block = BlockFactory.CreateBlock("MUL.I $1, $2") as MULBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F #1, @1") as DATBlock;
            
            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);
            
            Assert.AreEqual(3 * 1,target._regA.Value());
            Assert.AreEqual(5 * 1, target._regB.Value());
        }

        [Test]
        public void MulF()
        {
            MULBlock block = BlockFactory.CreateBlock("MUL.F $1, $2") as MULBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3*4, target._regA.Value());
            Assert.AreEqual(5*3, target._regB.Value());
        }

        [Test]
        public void MulX()
        {
            MULBlock block = BlockFactory.CreateBlock("MUL.X $1, $2") as MULBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3*3, target._regA.Value());
            Assert.AreEqual(5*4, target._regB.Value());
        }

        [Test]
        public void MulA()
        {
            MULBlock block = BlockFactory.CreateBlock("MUL.A $1, $2") as MULBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3*4, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }

        [Test]
        public void MulB()
        {
            MULBlock block = BlockFactory.CreateBlock("MUL.B $1, $2") as MULBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(5*3, target._regB.Value());
        }

        [Test]
        public void MulAB()
        {
            MULBlock block = BlockFactory.CreateBlock("MUL.AB $1, $2") as MULBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(5*4, target._regB.Value());
        }
        [Test]
        public void MulBA()
        {
            MULBlock block = BlockFactory.CreateBlock("MUL.BA $1, $2") as MULBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3*3, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }
    }
}
