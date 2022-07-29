namespace Outrage.TokenParser.Matchers
{
    public class IgnoreMatcher : IMatcher
    {
        private readonly IMatcher ignore;

        public IgnoreMatcher(IMatcher ignore)
        {
            this.ignore = ignore;
        }

        public Match Matches(Source input)
        {
            var match = ignore.Matches(input);
            if (match.Success)
            {
                return new Match();
            }

            return match;
        }
    }
}
