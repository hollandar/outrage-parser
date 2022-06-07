using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public class FlattenedToken : IToken
    {
        public FlattenedToken(TokenList tokens)
        {
            this.Values = tokens.Value;//.Flatten();
        }

        public IEnumerable<IToken> Values { get; }

    }
}
