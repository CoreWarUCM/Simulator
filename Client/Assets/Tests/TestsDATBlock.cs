using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsDATBlock{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }

        [Test]
        public void DATExecuteThrowsOnEveryModifierOtherThanF(){
            DATBlock[] blocks = {
                                  new DATBlock(0,0, CodeBlock.Modifier.A),
                                  new DATBlock(0,0, CodeBlock.Modifier.B),
                                  new DATBlock(0,0, CodeBlock.Modifier.AB),
                                  new DATBlock(0,0, CodeBlock.Modifier.BA),
                                  new DATBlock(0,0, CodeBlock.Modifier.I),
                                  new DATBlock(0,0, CodeBlock.Modifier.X),
                                };
            foreach(DATBlock block in blocks)
                Assert.Throws<CodeBlock.UnsupportedModifierException>( () => {
                    block.Execute(sim, 0);
                });
        }

        [Test]
        public void DATExecuteCallsKillWarriorOnFModifier(){
            DATBlock block = new DATBlock(0,0, CodeBlock.Modifier.F);
            block.Execute(sim, 0);
            Assert.That(sim.CountKills == 1);
        }
    }
}