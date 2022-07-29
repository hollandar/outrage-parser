namespace Outrage.TokenParser.Matchers
{
    public class WhenMatcher : IMatcher
    {
        private readonly IMatcher initialMatcher;
        private readonly Func<IEnumerable<IToken>, bool> clause;
        private readonly string msg;

        public WhenMatcher(IMatcher initialMatcher, Func<IEnumerable<IToken>, bool> clause, string msg = "")
        {
            this.initialMatcher = initialMatcher;
            this.clause = clause;
            this.msg = msg;
        }

        public Match Matches(Source source)
        {
            List<IToken> tokens = new();
            var initialMatch = initialMatcher.Matches(source);
            if (initialMatch.Success)
            {
                var finalMatch = clause(initialMatch.Tokens);
                if (finalMatch) return initialMatch;
                else
                {
                    return new Match($"matched content did not match then when criteria {msg}");
                }
            }
            else
            {
                return initialMatch;
            }
        }
    }
}
