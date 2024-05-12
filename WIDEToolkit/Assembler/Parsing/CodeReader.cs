using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Assembler.Parsing
{
    public class CodeReader
    {
        public string LineComment = "//";
        public string BlockCommentStart = "/*";
        public string BlockCommentEnd = "*/";

        public List<CodeOperator> Operators { get; set; } = new List<CodeOperator>();

        public CodeReader()
        {
            Operators.Add(
                CodeOperator.Of("QUOTE", '"')
                );

            Operators.Add(
                CodeOperator.Of("ROUND", '(', ')')
                .Opens(Operators, "QUOTE", "this")
                );

            Operators.Add(
                CodeOperator.Of("CURLY", '{', '}')
                .Opens(Operators, "QUOTE", "ROUND", "this")
                );
        }

        public string Sanitize(string text)
        {
            // Could be better but is enough for instruction parsing
            while(text.Contains(BlockCommentStart))
            {
                var cStart = text.IndexOf(BlockCommentStart);
                var cEnd = text.IndexOf(BlockCommentEnd, cStart + BlockCommentStart.Length);

                text = text.Remove(cStart, cEnd - cStart + BlockCommentEnd.Length).Insert(cStart, "\n");
            }

            var lines = text.Split(new string[] {
                    "\r\n",
                    "\n",
                    "\r"
                },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(ln => ln.Trim())
                .Where(ln => !ln.StartsWith(LineComment));

            return string.Join('\n', lines);
        }

        public SyntaxBranch GenerateTree(string text)
        {
            var tree = new SyntaxBranch()
            {
                Origin = text,
                Operator = "ROOT",
            };

            tree.Branches = Unwrap(text);

            return tree;
        }

        protected List<SyntaxBranch> Unwrap(string text)
        {
            List<SyntaxBranch> branches = new();

            string before = "";
            Stack<CodeOperator> stack = new ();
            
            for (int i = 0; i < text.Length; i++)
            {
                var chr = text[i];

                var op = Operators.Find(e => e.Open == chr);

                if (op is null)
                {
                    before += chr;
                    continue;
                }

            }

            if(before.Length > 0)
            {
                branches.Add(new SyntaxBranch()
                {
                    Origin = before,
                    Before = before,
                    Operator = "NONE",
                });
            }

            return branches;
        }
    }
}
