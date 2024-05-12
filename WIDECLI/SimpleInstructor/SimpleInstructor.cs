using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Parsing;

namespace WIDECLI.Instructor
{
    internal class SimpleInstructor
    {
        public void Start()
        {
            var cr = new CodeReader();

            var o = cr.Sanitize("// Test\nabcde/*fhg\nijklmnop\nqrst*/uvwyzźż\n123");
        }
    }
}
