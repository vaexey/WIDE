using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.Controller
{
    public class UserFriendlyException : Exception
    {
        public string? Category { get; }
        public bool Error { get; }

        public UserFriendlyException(string? message = null, string? category = null, bool error = false) : base(message)
        {
            Category = category;
            Error = error;
        }
    }
}
