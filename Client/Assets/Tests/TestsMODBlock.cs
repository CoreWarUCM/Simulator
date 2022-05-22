using Simulator;
using NUnit.Framework;
using Simulator.CodeBlocks;
using UnityEngine;

namespace Tests
{
    class TestsMODBlock
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
        public void ModI()
        {
            MODBlock block = BlockFactory.CreateBlock("MOD.I $1, $2") as MODBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F #1, @1") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3 % 1, target._regA.Value());
            Assert.AreEqual(5 % 1, target._regB.Value());
        }

        [Test]
        public void ModF()
        {
            MODBlock block = BlockFactory.CreateBlock("MOD.F $1, $2") as MODBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3 % 4, target._regA.Value());
            Assert.AreEqual(5 % 3, target._regB.Value());
        }

        [Test]
        public void ModX()
        {
            MODBlock block = BlockFactory.CreateBlock("MOD.X $1, $2") as MODBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3 % 3, target._regA.Value());
            Assert.AreEqual(5 % 4, target._regB.Value());
        }

        [Test]
        public void ModA()
        {
            MODBlock block = BlockFactory.CreateBlock("MOD.A $1, $2") as MODBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3 % 4, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }

        [Test]
        public void ModB()
        {
            MODBlock block = BlockFactory.CreateBlock("MOD.B $1, $2") as MODBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(5 % 3, target._regB.Value());
        }

        [Test]
        public void ModAB()
        {
            MODBlock block = BlockFactory.CreateBlock("MOD.AB $1, $2") as MODBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(5 % 4, target._regB.Value());
        }
        [Test]
        public void ModBA()
        {
            MODBlock block = BlockFactory.CreateBlock("MOD.BA $1, $2") as MODBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.SetBlock(datBlock, 1, 2);
            sim.SetBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3 % 3, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }
    }
}
