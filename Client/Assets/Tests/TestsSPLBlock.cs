using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsSPLBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }


        [Test]
        public void SPLB(){
            SPLBlock block = BlockFactory.CreateBlock("SPL.B $0, $0") as SPLBlock;
            sim.SetBlock(block, 0, 0);

            MOVBlock block1 = BlockFactory.CreateBlock("MOV.I $0, $1") as MOVBlock;
            sim.SetBlock(block1, 1, 0);

            block.Execute(sim, 0);
            Assert.AreEqual(0, sim.lastJump);

            Assert.AreEqual(1, sim.GetLastProcess());

            block.Execute(sim, 0);
            block.Execute(sim, 0);
            block.Execute(sim, 0);
            Assert.False(sim.CanCreateProcess());
        }
    }
}