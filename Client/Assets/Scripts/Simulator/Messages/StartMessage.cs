namespace Simulator
{
    public class StartMessage : BaseMessage
    {
        public VirusIO.Virus _v1 { get; private set; }
        public VirusIO.Virus _v2 { get; private set; }

        public StartMessage(VirusIO.Virus v1, VirusIO.Virus v2) :
            base(MessageType.BlockExecuted)
        {
            _v1 = v1;
            _v2 = v2;
        }
    }
}
