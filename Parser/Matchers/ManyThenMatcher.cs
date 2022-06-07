using Parser.Tokens;

namespace Parser.Matchers
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

        public Match Matches(Source input)
        {
            int matches = 0;
            List<IToken> tokens = new List<IToken>();
            do
            {
                var termMatch = many.Matches(input);
                if (termMatch.Success)
                {
                    if (termMatch.Token != null) tokens.Add(termMatch.Token);
                    matches++;
                }

                var thenMatch = then.Matches(input);
                if (thenMatch.Success)
                {
                    if (thenMatch.Token != null) tokens.Add(thenMatch.Token);
                    break;
                }

                if (termMatch.Success == false && thenMatch.Success == false)
                {
                    return new Match($"expected {termMatch.Error} or {thenMatch.Error}");
                }

            } while (true);

            if (matches < minimumMatches)
            {
                return new Match($"expected at least {minimumMatches}, found {matches}");
            }

            return new Match(new TokenList(tokens));
        }
    }
}
