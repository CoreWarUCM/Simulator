using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public enum MessageType{
        BlockModify,
        Death,
        Jump
    }
    public class BaseMessage
    {
        public MessageType _type { get; private set;}
        public int warrior;
        public BaseMessage(MessageType type){_type = type;}
    }
}
