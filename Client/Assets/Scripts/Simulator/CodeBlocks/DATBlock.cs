using Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.CodeBlocks
{
    public class DATBlock : CodeBlock
    {
        public DATBlock(int regA = 0, int regB = 0, Modfier mod = Modfier.F) : base(mod, regA, regB) { }

        protected override void A(ISimulator simulator, int location)
        {
            throw new NotImplementedException();
        }

        protected override void AB(ISimulator simulator, int location)
        {
            throw new NotImplementedException();
        }

        protected override void B(ISimulator simulator, int location)
        {
            throw new NotImplementedException();
        }

        protected override void BA(ISimulator simulator, int location)
        {
            throw new NotImplementedException();
        }

        protected override void F(ISimulator simulator, int location)
        {
            throw new NotImplementedException();
        }

        protected override void I(ISimulator simulator, int location)
        {
            throw new NotImplementedException();
        }

        protected override void X(ISimulator simulator, int location)
        {
            throw new NotImplementedException();
        }
    }
}
