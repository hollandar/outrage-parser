using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public static class TokenExtensions
    {
        public static IEnumerable<IToken> Flatten(this IToken token)
        {
            if (!(token is TokenList))
                yield return token;
            else
            {
                var list = (TokenList)token;
                foreach (var item in list.Value)
                {
                    var innerList = item.Flatten().ToList();

                    foreach (var innerToken in innerList)
                        yield return innerToken;
                }
            }
        }
    }
}
