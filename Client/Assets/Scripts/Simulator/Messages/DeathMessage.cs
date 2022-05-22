namespace Simulator
{
    public class DeathMessage : BaseMessage
    {
        public int deathlocation { get; private set; }
        public bool divideByZero { get; private set; }
        public DeathMessage(int location, bool dvz = false):
            base(MessageType.Death)
        {
            deathlocation = location;
            divideByZero = dvz;
        }
    }
}
