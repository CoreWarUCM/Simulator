using Simulator.CodeBlocks;
namespace Simulator
{
    public interface ISimulator
    {
        public void CreateProcess(int position, int origin);
        public bool CanCreateProcess();
        public void SetBlock(CodeBlock block, int position, int origin);
        public CodeBlock GetBlock(int position, int origin);
        public void JumpTo(int absoluteDestination);
        public void KillVirus();
        public int ResolveAddress(int relativeAddress, int originalPosition);
        public void SendMessage(BaseMessage message);
        public void SetPrivateSpace(int location, int value);
        public int GetPrivateSpace(int location);
    }
}
