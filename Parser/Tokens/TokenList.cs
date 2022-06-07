using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public sealed class TokenList : IToken
    {
        public TokenList(IEnumerable<IToken> tokens)
        {
            this.Value = tokens.Where(r => !(r is IgnoreToken)).ToList();
        }

        public List<IToken> Value { get; }
    }
}
