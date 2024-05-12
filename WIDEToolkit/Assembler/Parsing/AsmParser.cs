using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Data.Exceptions;

namespace WIDEToolkit.Assembler.Parsing
{
    public class AsmParser
    {
        public static List<List<IAsmSymbol>> Parse(string text)
        {
            var lines = text.Split(
                new string[]
                {
                    "\r\n",
                    "\n",
                    "\r"
                },
                StringSplitOptions.None)
                .Select(ln => ln.Trim())
                .Where(ln => ln.Length > 0);

            var listing = lines
                .Select(ln => ParseLine(ln))
                .Where(syms => syms.Count > 0);

            return listing.ToList();
        }

        protected static string[] AsmStackSplit(string str, char delimeter)
        {
            return str.StackSplit(
                delimeter,
                new()
                {
                    new('[', ']')
                });
        }

        public static List<IAsmSymbol> ParseLine(string line)
        {
            if (line.Contains(";"))
                line = line.Split(";")[0];

            var symbolStrings = AsmStackSplit(line, ' ');

            return symbolStrings
                .Select(str => str.Trim())
                .Where(str => str.Length > 0 && str != ",")
                .Select(str => str.StartsWith(",") ? str.Substring(1) : str)
                .Select(str => str.EndsWith(",") ? str.Substring(0, str.Length - 1) : str)
                .Select(str => ParseSymbol(str)).ToList();
        }

        public static IAsmSymbol ParseSymbol(string symbol)
        {
            if (symbol.Length == 0)
                throw new AssemblerException("Could not parse empty symbol");

            if(symbol.Where(chr => chr == '[').Count()
                != symbol.Where(chr => chr == ']').Count())
            {
                throw new AssemblerException($"Symbol \"{symbol}\" has invalid address brackets");
            }

            var labelRegex = new Regex("^[a-zA-Z_0-9]*$");
            var newLabelRegex = new Regex("^[a-zA-Z_0-9]*:$");

            var decRegex = new Regex("^[0-9]*$");
            var hexRegex = new Regex("^[0-9a-fA-F]*$");

            if (symbol.EndsWith(":"))
            {
                if(!newLabelRegex.IsMatch(symbol))
                {
                    throw new AssemblerException($"\"{symbol}\" is not a valid label name");
                }

                return new NewLabelSymbol(symbol.Substring(0, symbol.Length - 1));
            }

            var plusSplit = AsmStackSplit(symbol, '+');
            if (plusSplit.Length > 1)
            {
                var sym1 = plusSplit[0].Trim();
                var sym2 = symbol.Remove(0, plusSplit[0].Length + 1).Trim();

                return new OffsetSymbol(
                    ParseSymbol(sym1),
                    ParseSymbol(sym2),
                    1);
            }

            var minusSplit = AsmStackSplit(symbol, '-');
            if (minusSplit.Length > 1)
            {
                var sym1 = minusSplit[0].Trim();
                var sym2 = symbol.Remove(0, minusSplit[0].Length + 1).Trim();

                return new OffsetSymbol(
                    ParseSymbol(sym1),
                    ParseSymbol(sym2),
                    -1);
            }

            if (symbol.StartsWith("[") && symbol.EndsWith("]"))
            {
                return new AddressSymbol(
                        ParseSymbol(symbol.Substring(1, symbol.Length - 2))
                    );
            }

            if ("0123456789".Contains(symbol[0]))
            {
                string? hex = null;

                if(symbol.ToLower().EndsWith("h"))
                {
                    hex = symbol.Substring(0, symbol.Length - 1);
                }
                else if(symbol.ToLower().StartsWith("0x"))
                {
                    hex = symbol.Substring(2);
                }

                ulong value;

                if(hex is not null)
                {
                    if (!hexRegex.IsMatch(hex))
                        throw new AssemblerException($"\"{symbol}\" is not a valid hexadecimal number");

                    value = Convert.ToUInt64(hex, 16);
                }
                else
                {
                    if (!decRegex.IsMatch(symbol))
                        throw new AssemblerException($"\"{symbol}\" is not a valid number");

                    value = Convert.ToUInt64(symbol, 10);
                }

                return new ImmediateSymbol(value);
            }

            if(!labelRegex.IsMatch(symbol))
            {
                throw new AssemblerException($"\"{symbol}\" is not a valid label name");
            }

            return new LabelSymbol(symbol);
        }
    }
}
