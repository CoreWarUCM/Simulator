using UnityEngine;
using UnityEngine.Assertions;

namespace Simulator
{
    public class CommonMemoryManager
    {
        //This 8000 should be fetched from a configuration object to allow custom memory sizes
        private readonly int _memSize; 
        private CodeBlock[] _memory;

        public CommonMemoryManager(int memSize = 8000)
        {
            _memSize = memSize;
            _memory = new CodeBlock[_memSize];
        }
        
        public int ResolveAddress(int relativeAddress, int originalPosition)
        {
            Assert.IsTrue(originalPosition >= 0);
            int res = (originalPosition + relativeAddress);
            if (res < 0)
                return _memSize + res;
            return  res % _memSize;
        }
    }
}
