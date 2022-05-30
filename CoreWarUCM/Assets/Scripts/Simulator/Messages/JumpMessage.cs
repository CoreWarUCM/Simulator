namespace Simulator
{
    public class JumpMessage : BaseMessage
    {
        public int jumpLocation { get; private set; }
        public JumpMessage(int location):
            base(MessageType.Jump)
        {
            jumpLocation = location;
        }
    }
}
