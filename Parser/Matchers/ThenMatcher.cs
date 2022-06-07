using Parser.Tokens;

namespace Parser.Matchers
{
    public class ThenMatcher: IMatcher
    {
        private readonly IMatcher initialMatcher;
        private readonly IMatcher thenMatcher;

        public ThenMatcher(IMatcher initialMatcher, IMatcher thenMatcher)
        {
            this.initialMatcher = initialMatcher;
            this.thenMatcher = thenMatcher;
        }

        public Match Matches(Source input)
        {
            List<IToken> tokens = new();
            var initialMatch = initialMatcher.Matches(input);
            if (initialMatch.Success)
            {
                if (initialMatch.Token != null) tokens.Add(initialMatch.Token);
            } else
            {
                return initialMatch;
            }

            var thenMatch = thenMatcher.Matches(input);
            if (thenMatch.Success) { 
                if (thenMatch.Token != null) tokens.Add(thenMatch.Token);   
            }
            else
            {
                return thenMatch;
            }

            return new Match(new TokenList(tokens));
        }
    }
}
