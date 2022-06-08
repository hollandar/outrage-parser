using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
{
    public class ExactMatcher : IMatcher
    {
        private readonly Func<ReadOnlyMemory<char>, IToken> tokenize;
        private Memory<char> match;

        public ExactMatcher(char[] match, Func<ReadOnlyMemory<char>, IToken> tokenize)
        {
            this.match = new Memory<char>(match);
            this.tokenize = tokenize;
        }

        public ExactMatcher(string match, Func<ReadOnlyMemory<char>, IToken> tokenize)
        {
            this.match = new Memory<char>(match.ToArray());
            this.tokenize = tokenize;
        }

        public ExactMatcher(char c, Func<ReadOnlyMemory<char>, IToken> tokenize)
        {
            this.match = new Memory<char>(new char[] { c });
            this.tokenize = tokenize;
        }

        public Match Matches(Source input)
        {
            if (input.Length >= match.Length)
            {
                var value = input.ReadOnlyMemory.Slice(0, match.Length);
                if (value.Span.SequenceEqual(match.Span))
                {
                    var match = new Match(tokenize(value));
                    input.Advance(value.Length);

                    return match;
                }
                return new Match($"{match.ToString()} was expected, got {value.ToString()}.");
            }

            return new Match($"not enough input to match {match.ToString().Replace("\r", "\\r").Replace("\n", "\\n")}");
        }
    }
}
