namespace Simulator
{
    public class BlockModifyMessage : BaseMessage
    {
        public int modifiedLcoation { get;  private set; }
        public BlockModifyMessage(int location) :
            base(MessageType.BlockModify)
        {
            modifiedLcoation = location;
        }
    }
}
