using Parser;
using Parser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tokens
{
    internal class BracketsToken: FlattenedToken
    {
        public BracketsToken(TokenList tokens): base(tokens)
        {
        }
    }
}
