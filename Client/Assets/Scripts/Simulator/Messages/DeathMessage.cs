namespace Simulator
{
    public class DeathMessage : BaseMessage
    {
        public int deathlocation { get; private set; }
        public DeathMessage(int location):
            base(MessageType.Death)
        {
            deathlocation = location;
        }
    }
}
