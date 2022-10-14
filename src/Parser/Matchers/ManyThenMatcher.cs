using System.Runtime.CompilerServices;

namespace Outrage.TokenParser.Matchers
{
    public class ManyThenMatcher : IMatcher
    {
        private readonly IMatcher many;
        private readonly IMatcher then;
        private readonly int minimumMatches;

        public ManyThenMatcher(IMatcher many, IMatcher then, int minimumMatches = 0)
        {
            this.many = many;
            this.then = then;
            this.minimumMatches = minimumMatches;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Match Matches(Source input)
        {
            int matches = 0;
            List<IToken> tokens = new List<IToken>();
            do
            {
                var termMatch = many.Matches(input);
                if (termMatch.Success)
                {
                    if (termMatch.Tokens != null) tokens.AddRange(termMatch.Tokens);
                    matches++;
                }

                var thenMatch = then.Matches(input);
                if (thenMatch.Success)
                {
                    if (thenMatch.Tokens != null) tokens.AddRange(thenMatch.Tokens);
                    break;
                }

                if (termMatch.Success == false && thenMatch.Success == false)
                {
                    return new Match(() => $"expected {termMatch.Error} or {thenMatch.Error}");
                }

            } while (true);

            if (matches < minimumMatches)
            {
                return new Match(() => $"expected at least {minimumMatches}, found {matches}");
            }

            return new Match(tokens);
        }
    }
}
