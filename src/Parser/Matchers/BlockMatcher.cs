namespace Outrage.TokenParser.Matchers
{
    public class BlockMatcher : IMatcher
    {
        private IMatcher innerMatcher;

        public BlockMatcher(IMatcher inner)
        {
            this.innerMatcher = inner;
        }

        public Match Matches(Source input)
        {
            return innerMatcher.Matches(input);
        }
    }
}
