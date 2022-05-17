namespace Simulator
{
    public class StartMessage : BaseMessage
    {
        public Virus _v1 { get; private set; }
        public Virus _v2 { get; private set; }

        public StartMessage(Virus v1, Virus v2) :
            base(MessageType.BlockExecuted)
        {
            _v1 = v1;
            _v2 = v2;
        }
    }
}
