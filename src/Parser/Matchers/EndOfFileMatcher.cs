using Outrage.TokenParser.Tokens;

namespace Outrage.TokenParser.Matchers
{
    public class EndOfFileMatcher : IMatcher
    {
        public EndOfFileMatcher()
        {
        }

        public Match Matches(Source input)
        {
            if (input.Length == 0)
            {
                return new Match(new EndOfFileToken());
            }

            return new Match("End of file expected.");
        }
    }
}
