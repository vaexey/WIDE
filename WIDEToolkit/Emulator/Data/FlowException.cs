﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Data
{
    public class FlowException : EmulatorException
    {
        public FlowException(string message) : base(message) { }
    }
}
