namespace Simulator
{
    public class BlockExecutedMessage : BaseMessage
    {
        public int modifiedLcoation { get; private set; }
        public BlockExecutedMessage(int location) :
            base(MessageType.BlockExecuted)
        {
            modifiedLcoation = location;
        }
    }
}
