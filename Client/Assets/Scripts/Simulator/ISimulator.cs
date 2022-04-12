using Simulator.CodeBlocks;
namespace Simulator
{
    public interface ISimulator
    {
        public void CreateProcess(int warrior, int position, int origin);
        public void CreateBlock(CodeBlock block, int position, int origin);
        public CodeBlock GetBlock(int position, int origin);
        public void JumpTo(int destination, int origin);
        public void KillWarrior();
    }
}
