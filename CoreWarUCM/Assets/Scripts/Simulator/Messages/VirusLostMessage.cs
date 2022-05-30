namespace Simulator
{
    public class VirusLostMessage : BaseMessage
    {
        public int deathlocation { get; private set; }
        public bool divideByZero { get; private set; }
        public VirusLostMessage(int location, bool dvz = false):
            base(MessageType.VirusLost)
        {
            deathlocation = location;
            divideByZero = dvz;
        }
    }
}
