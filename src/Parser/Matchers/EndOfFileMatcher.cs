using Outrage.TokenParser.Tokens;
using System.Runtime.CompilerServices;

namespace Outrage.TokenParser.Matchers
{
    public class EndOfFileMatcher : IMatcher
    {
        public EndOfFileMatcher()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Match Matches(Source input)
        {
            if (input.Length == 0)
            {
                return new Match(new EndOfFileToken());
            }

            return new Match(() => "End of file expected.");
        }
    }
}
