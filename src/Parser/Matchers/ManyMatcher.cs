using System.Net.Mime;

namespace Outrage.TokenParser.Matchers
{
    public class ManyMatcher : IMatcher
    {
        private readonly IMatcher many;
        private readonly int minimumMatches;
        private readonly int maximumMatches;

        public ManyMatcher(IMatcher many, int minimumMatches = 0, int maximumMatches = int.MaxValue)
        {
            this.many = many;
            this.minimumMatches = minimumMatches;
            this.maximumMatches = maximumMatches;
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
                    if (matches >= maximumMatches)
                        break;
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
