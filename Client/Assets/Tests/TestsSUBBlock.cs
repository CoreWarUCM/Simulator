using Simulator;
using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;
using UnityEngine;

namespace Tests
{
    class TestsSUBBlock
    {

        private MockMemorySimulator sim;
        private DATBlock target;

        [SetUp]
        public void SetUpCommonMemory()
        {
            sim = new MockMemorySimulator();
            target = new DATBlock(3, 5, CodeBlock.Modifier.F);
            sim.CreateBlock(target, 2, 0);
        }

        [Test]
        public void SubISubsRegisterWise()
        {
            SUBBlock block = BlockFactory.CreateBlock("SUB.I $1, $2") as SUBBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F #1, @1") as DATBlock;
            
            sim.CreateBlock(datBlock, 1, 2);
            sim.CreateBlock(block, 0, 0);

            block.Execute(sim, 0);
            
            Assert.AreEqual(2,target._regA.Value());
            Assert.AreEqual(4, target._regB.Value());
        }

        [Test]
        public void SubFSubsRegisterWise()
        {
            SUBBlock block = BlockFactory.CreateBlock("SUB.F $1, $2") as SUBBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.CreateBlock(datBlock, 1, 2);
            sim.CreateBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(7999, target._regA.Value());
            Assert.AreEqual(2, target._regB.Value());
        }

        [Test]
        public void SubXSubsRegisterWise()
        {
            SUBBlock block = BlockFactory.CreateBlock("SUB.X $1, $2") as SUBBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.CreateBlock(datBlock, 1, 2);
            sim.CreateBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(0, target._regA.Value());
            Assert.AreEqual(1, target._regB.Value());
        }

        [Test]
        public void SubASubsRegisterWise()
        {
            SUBBlock block = BlockFactory.CreateBlock("SUB.A $1, $2") as SUBBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.CreateBlock(datBlock, 1, 2);
            sim.CreateBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(7999, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }

        [Test]
        public void SubBSubsRegisterWise()
        {
            SUBBlock block = BlockFactory.CreateBlock("SUB.B $1, $2") as SUBBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.CreateBlock(datBlock, 1, 2);
            sim.CreateBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(2, target._regB.Value());
        }

        [Test]
        public void SubABSubsRegisterWise()
        {
            SUBBlock block = BlockFactory.CreateBlock("SUB.AB $1, $2") as SUBBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.CreateBlock(datBlock, 1, 2);
            sim.CreateBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(3, target._regA.Value());
            Assert.AreEqual(1, target._regB.Value());
        }
        [Test]
        public void SubBASubsRegisterWise()
        {
            SUBBlock block = BlockFactory.CreateBlock("SUB.BA $1, $2") as SUBBlock;
            DATBlock datBlock = BlockFactory.CreateBlock("DAT.F <4, #3") as DATBlock;

            sim.CreateBlock(datBlock, 1, 2);
            sim.CreateBlock(block, 0, 0);

            block.Execute(sim, 0);

            Assert.AreEqual(0, target._regA.Value());
            Assert.AreEqual(5, target._regB.Value());
        }
    }
}
