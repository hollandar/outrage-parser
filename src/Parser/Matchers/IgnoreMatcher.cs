using System.Runtime.CompilerServices;

namespace Outrage.TokenParser.Matchers
{
    public class IgnoreMatcher : IMatcher
    {
        private readonly IMatcher ignore;

        public IgnoreMatcher(IMatcher ignore)
        {
            this.ignore = ignore;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
