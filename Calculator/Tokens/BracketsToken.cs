using Parser;
using Parser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tokens
{
    public class BracketsToken : IToken
    {
        public BracketsToken(IEnumerable<IToken> tokens)
        {
            this.Value = tokens;
        }

        public IEnumerable<IToken> Value { get; }
    }
}
