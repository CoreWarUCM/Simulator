using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsAddresingModes{
        private MockMemorySimulator sim;
        [SetUp]
        public void SetUpCommonMemory(){
            sim = new MockMemorySimulator();
        }

        [Test]
        public void TestImmediate(){
           DATBlock d = new DATBlock(
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.immediate, 3),
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.immediate, 3)
               );
            Assert.AreEqual(d._regA.rGet(sim,3),3);
        }

        [Test]
        public void TestDirect(){
           DATBlock d = new DATBlock(
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 3),
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 3)
               );
            Assert.That(d._regA.rGet(sim,0)==3);
        }

        [Test]
        public void TestAIndirect(){
            DATBlock source = new DATBlock(
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.AIndirect, 3),
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 3)
               );
            DATBlock other = new DATBlock(2,0);

            sim.CreateBlock(source,0,0);
            sim.CreateBlock(other,3,0);
            Assert.AreEqual(5,source._regA.rGet(sim,0));
            Assert.AreEqual(3,source._regA.Value());
        }

        [Test]
        public void TestBIndirect(){
            DATBlock source = new DATBlock(
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.BIndirect, 3),
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 3)
               );
            DATBlock other = new DATBlock(2,1);

            sim.CreateBlock(source,0,0);
            sim.CreateBlock(other,3,0);
            Assert.AreEqual(4,source._regA.rGet(sim,0));
        }
        
        [Test]
        public void TestAPredecrement(){
            DATBlock source = new DATBlock(
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.APredecrement, 3),
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 3)
               );
            DATBlock other = new DATBlock(2,0);

            sim.CreateBlock(source,0,0);
            sim.CreateBlock(other,2,0);
            Assert.AreEqual(4,source._regA.rGet(sim,0));
            Assert.AreEqual(2,source._regA.Value());
        }

        [Test]
        public void TestBPredecrement(){
            DATBlock source = new DATBlock(
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 4),
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.BPredecrement, 2)
               );
            DATBlock other = new DATBlock(2,0);

            sim.CreateBlock(source,0,0);
            sim.CreateBlock(other,1,0);
            Assert.AreEqual(1,source._regB.rGet(sim,0));
            Assert.AreEqual(1,source._regB.Value());
        }
        [Test]
        public void TestAPostincrement(){
            DATBlock source = new DATBlock(
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.APostincrement, 3),
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 3)
               );
            DATBlock other = new DATBlock(2,0);

            sim.CreateBlock(source,0,0);
            sim.CreateBlock(other,3,0);
            
            Assert.AreEqual(5,source._regA.rGet(sim,0));
            Assert.AreEqual(4,source._regA.Value());
        }

        [Test]
        public void TestBPostincrement(){
            DATBlock source = new DATBlock(
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.direct, 4),
                new CodeBlock.Register(CodeBlock.Register.AddressingMode.BPostincrement, 2)
               );
            DATBlock other = new DATBlock(2,0);

            sim.CreateBlock(source,0,0);
            sim.CreateBlock(other,2,0);
            Assert.AreEqual(2,source._regB.rGet(sim,0));
            Assert.AreEqual(3,source._regB.Value());
        }
    }
}