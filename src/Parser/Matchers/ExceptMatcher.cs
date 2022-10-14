using System.Runtime.CompilerServices;

namespace Outrage.TokenParser.Matchers
{
    public class ExceptMatcher :IMatcher
    {
        private readonly IMatcher except;
        private readonly IMatcher actual;

        public ExceptMatcher(IMatcher except, IMatcher actual)
        {
            this.except = except;
            this.actual = actual;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Match Matches(Source input)
        {
            var match = except.Preview().Matches(input);
            if (match.Success)
            {
                return new Match(() => "Except value was found.");
            }
            else
                return actual.Matches(input);
        }
    }
}
