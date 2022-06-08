using Parser.Tokens;

namespace Parser.Matchers
{
    public class ManyMatcher : IMatcher
    {
        private readonly IMatcher many;
        private readonly int minimumMatches;

        public ManyMatcher(IMatcher many, int minimumMatches = 0)
        {
            this.many = many;
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
                    if (termMatch.Tokens != null) tokens.AddRange(termMatch.Tokens);
                    matches++;
                }
                else
                {
                    break;
                }
            } while (true);

            if (matches < minimumMatches)
            {
                return new Match($"expected at least {minimumMatches}, found {matches}");
            }

            return new Match (tokens);
        }
    }
}
