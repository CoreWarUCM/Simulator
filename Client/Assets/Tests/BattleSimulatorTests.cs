using NUnit.Framework;
using Simulator;

namespace Tests
{
    public class BattleSimulatorTests
    {
        private CommonMemoryManager _commonMemoryManager;
        
        [SetUp]
        public void SetUp()
        {
            _commonMemoryManager = new CommonMemoryManager();
        }
        
        [Test]
        public void UselessTest()
        {
            Assert.That(true);
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
            Assert.AreEqual(299,new CommonMemoryManager(300).ResolveAddress(-1,0));
        }
    }
}
