using Simulator.CodeBlocks;
namespace Simulator
{
    public interface ISimulator
    {
        public void CreateProcess(int warrior, int position, int origin);
        public void CreateBlock(CodeBlock block, int position, int origin);
        public CodeBlock GetBlock(int position, int origin);
        public void JumpTo(int absoluteDestination);
        public void KillWarrior();
        public int ResolveAddress(int relativeAddress, int originalPosition);
    }
}
