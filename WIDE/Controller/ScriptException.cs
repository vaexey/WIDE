using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.Controller
{
    public class ScriptException : UserFriendlyException
    {
        // TODO: translate title
        public ScriptException(string? message = null, bool error = false) : base(message, "Scripts", error)
        { }
    }
}
