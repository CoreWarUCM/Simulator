﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.CodeBlocks
{
    public interface ICodeBlock
    {
        public BaseMessage Execute(ISimulator simulator, int location);
    }
}
