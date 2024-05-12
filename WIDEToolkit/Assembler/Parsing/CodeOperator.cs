using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Assembler.Parsing
{
    public class CodeOperator
    {
        public string? Name { get; set; } = null;

        public char Open { get; set; }
        public char Close { get; set; }

        public List<CodeOperator> CanBeOpened { get; set; } = new();

        public CodeOperator Opens(params CodeOperator?[] ops)
        {
            CanBeOpened.AddRange(ops.Select(op =>
            {
                if (op is null)
                    return this;

                return op;
            }));

            return this;
        }

        public CodeOperator Opens(IEnumerable<CodeOperator> list, params string[] ops)
        {
            Opens(ops.Select(op =>
            {
                return list.Where(e => e.Name == op).FirstOrDefault();
            }).ToArray());

            return this;
        }

        //public CodeOperator Closes(IEnumerable<CodeOperator> list, params string[] ops)
        //{
        //    Closes(ops.Select(op =>
        //    {
        //        return list.Where(e => e.Name == op).FirstOrDefault();
        //    }).ToArray());

        //    return this;
        //}

        //public CodeOperator Closes(params CodeOperator?[] ops)
        //{
        //    CanBeClosed.AddRange(ops.Select(op =>
        //    {
        //        if (op is null)
        //            return this;

        //        return op;
        //    }));

        //    return this;
        //}

        public static CodeOperator Of(string? name, char open, char? close = null)
        {
            var co = new CodeOperator()
            {
                Name = name,
                Open = open,
                Close = close ?? open
            };

            return co;
        }
    }
}
