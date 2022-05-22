using UnityEngine.Assertions;

using Simulator.CodeBlocks;

namespace Simulator
{
    /// <summary>
    /// Class that handles common memory, that is: memory shared for all warriors
    /// Extends ISimulator as a decorator to forward calls from blocks to actual manager without coupling them directly
    /// </summary>
    public class CommonMemoryManager : ISimulator
    {
        //This 8000 should be fetched from a configuration object to allow custom memory sizes
        private readonly int _memSize; 
        private CodeBlock[] _memory;

        private ISimulator _simulator;

        public CommonMemoryManager(ISimulator simulator, int memSize = 8000)
        {
            _memSize = memSize;
            _memory = new CodeBlock[_memSize];
            _simulator = simulator;
            for (int i = 0; i < _memSize; i++)
                _memory[i] = new DATBlock(0,0);
        }

        public int ResolveAddress(int relativeAddress, int originalPosition)
        {
            Assert.IsTrue(originalPosition >= 0);
            int res = (originalPosition + relativeAddress);
            if (res < 0)
                return _memSize + res;
            return res % _memSize;
        }

        public void SetBlock(CodeBlock block, int position, int origin)
        {
            _memory[ResolveAddress(position, origin)] = block;
        }

        public void CreateProcess(int virus, int position, int origin)
        {
            //Call delegated to actual simulator
            _simulator.CreateProcess(virus, position, origin);
        }

        public CodeBlock GetBlock(int position, int origin)
        {
            return _memory[ResolveAddress(position, origin)];
        }

        //Calls delegated to actual simulator ----------------------------------------------

        public void JumpTo(int destination){_simulator.JumpTo(destination);}
        public void KillVirus(){_simulator.KillVirus(); }
        public void SendMessage(BaseMessage message) { _simulator.SendMessage(message); }
    }
}
