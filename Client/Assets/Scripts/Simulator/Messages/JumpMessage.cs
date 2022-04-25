namespace Simulator
{
    public class JumpMessage : BaseMessage
    {
        public int jumpLocation { get; private set; }
        public JumpMessage(int location)
        {
            jumpLocation = location;
        }
    }
}
