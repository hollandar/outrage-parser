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

        public Match Matches(Source source)
        {
            var trackingSource = source.Clone();
            List<IToken> tokens = new();
            var initialMatch = initialMatcher.Matches(trackingSource);
            if (initialMatch.Success)
            {
                if (initialMatch.Tokens != null) tokens.AddRange(initialMatch.Tokens);
            } else
            {
                return initialMatch;
            }

            var thenMatch = thenMatcher.Matches(trackingSource);
            if (thenMatch.Success) { 
                if (thenMatch.Tokens != null) tokens.AddRange(thenMatch.Tokens);   
            }
            else
            {
                return thenMatch;
            }

            source.Advance(trackingSource.Position - source.Position);

            return new Match(tokens);
        }
    }
}
