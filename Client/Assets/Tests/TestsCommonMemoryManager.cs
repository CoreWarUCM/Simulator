using NUnit.Framework;
using Simulator;
using Simulator.CodeBlocks;

namespace Tests
{
    public class TestsCommonMemoryManager
    {
        private CommonMemoryManager _commonMemoryManager;
        private ISimulator _sim;
        
        [SetUp]
        public void SetUp()
        {
            _sim = new MockBattleSimulator();
            _commonMemoryManager = new CommonMemoryManager(_sim);
        }
        
        [Test]
        public void ResolveAddressUpperLimit()
        {
            Assert.That(_commonMemoryManager.ResolveAddress(5,7995)==0);
        }
        
        [Test]
        public void ResolveAddressLowerLimit()
        {
            Assert.AreEqual(7998,_commonMemoryManager.ResolveAddress(-2,0));
        }
        
        [Test]
        public void ResolveAddressDifferentSizeLimit()
        {
            Assert.AreEqual(299,new CommonMemoryManager(_sim,300).ResolveAddress(-1,0));
        }

        [Test]
        public void CreateAndGetBlockAtUpperLimitPosition()
        {
            _commonMemoryManager.CreateBlock(new DATBlock(0,42), 8000, 0);
            DATBlock db = (DATBlock)_commonMemoryManager.GetBlock(0, 0);
            
            int loc = _commonMemoryManager.ResolveAddress(8000,0);
            Assert.AreEqual(db._regA.rGet(_commonMemoryManager, loc), 0);
            Assert.AreEqual(db._regB.rGet(_commonMemoryManager, loc), 42);
        }

        [Test]
        public void CreateAndGetBlockAtLowerLimitPosition()
        {
            _commonMemoryManager.CreateBlock(new DATBlock(100, 2), -3, 0);
            DATBlock db = (DATBlock)_commonMemoryManager.GetBlock(-3, 0);

            int loc = _commonMemoryManager.ResolveAddress(-3,0);
            Assert.AreEqual(db._regA.rGet(_commonMemoryManager,loc), 100);
            Assert.AreEqual(db._regB.rGet(_commonMemoryManager,loc), 2);
        }
    }
}
