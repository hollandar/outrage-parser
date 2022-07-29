namespace Outrage.TokenParser.Matchers
{
    public class AnyCharacterMatcher : IMatcher
    {
        private readonly Func<ReadOnlyMemory<char>, IToken> tokenize;

        public AnyCharacterMatcher(Func<ReadOnlyMemory<char>, IToken> tokenize)
        {
            this.tokenize = tokenize;
        }

        public Match Matches(Source input)
        {
            if (input.Length >= 1)
            {
                var match = new Match (tokenize(input.ReadOnlyMemory.Slice(0, 1)));
                input.Advance(1);

                return match;
            }

            return new Match("Any character was expected.");
        }
    }
}
