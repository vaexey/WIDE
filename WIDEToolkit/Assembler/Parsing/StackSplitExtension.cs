using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Assembler.Parsing
{
    public static class StackSplitExtension
    {
        public static string[] StackSplit(this string src, char delimeter, List<(char start, char end)> stackElements)
        {
            List<string> split = new();

            Stack<char> stack = new();
            List<char> starts = stackElements.Select(t => t.start).ToList();
            List<char> ends = stackElements.Select(t => t.end).ToList();

            string last = "";

            for(int i = 0; i < src.Length; i++)
            {
                char chr = src[i];
                
                if(stack.Count == 0)
                {
                    if(chr == delimeter)
                    {
                        split.Add(last);
                        last = "";

                        continue;
                    }
                }
                else
                {
                    if(stack.First() == chr)
                    {
                        stack.Pop();
                    }
                }

                int isStart = starts.IndexOf(chr);

                if(isStart != -1)
                {
                    last += chr;

                    stack.Push(ends[isStart]);
                    continue;
                }

                last += chr;
            }

            if(last.Length > 0)
                split.Add(last);

            return split.ToArray();
        }
    }
}
