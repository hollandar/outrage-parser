using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
{
    public class CharacterMatcher : IMatcher
    {
        private readonly Func<char, (bool success, string error)> match;
        private readonly Func<ReadOnlyMemory<char>, IToken> tokenize;

        public CharacterMatcher(Func<char, (bool success, string error)> match, Func<ReadOnlyMemory<char>, IToken> tokenize)
        {
            this.match = match;
            this.tokenize = tokenize;
        }

        public Match Matches(Source input)
        {
            if (input.Length >= 1)
            {
                var value = input.ReadOnlyMemory.Slice(0, 1);
                var matchResult = match(value.Span[0]);
                if (matchResult.success)
                {
                    var match = new Match(tokenize(value));
                    input.Advance(value.Length);

                    return match;
                }
                else
                    return new Match(matchResult.error);
            }

            return new Match("End of input reached.");
        }
    }
}
