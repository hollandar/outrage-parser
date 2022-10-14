using System.Runtime.CompilerServices;

namespace Outrage.TokenParser.Matchers
{
    public class UntilMatcher : IMatcher
    {
        private readonly IMatcher matcher;
        private readonly IMatcher until;

        public UntilMatcher(IMatcher matcher, IMatcher until)
        {
            this.matcher = matcher;
            this.until = until;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Match Matches(Source input)
        {
            Match? innerMatch = null;
            Match untilMatch;
            int untilPosition=0;
            int advance = 0;
            var untilSource = new Source(input);
            do
            {
                if (untilSource.ReadOnlyMemory.Length == 0)
                    return new Match(() => "Reached the end of the file matching until.");

                untilPosition = untilSource.Position;
                untilMatch = this.until.Matches(untilSource);

                if (untilMatch.Success)
                {
                    var innerSource = input.Constrain(untilPosition);
                    innerMatch = this.matcher.Matches(innerSource);

                    if (untilPosition != innerSource.Position)
                    {
                        innerMatch = new Match(() => $"terminator not reached matching until.");
                    }
                }
                else
                {
                    untilSource.Advance(1);
                }

                advance = untilSource.Position - input.Position;
            }
            while (!(innerMatch?.Success ?? false));

            input.Advance(advance);
            return new Match(innerMatch.Value.Tokens.Union(untilMatch.Tokens));

        }

    }
}
