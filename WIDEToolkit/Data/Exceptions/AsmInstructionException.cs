﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Data.Exceptions
{
    public class AsmInstructionException : AssemblerException
    {
        public AsmInstructionException(string? message) : base(message) { }
    }
}
