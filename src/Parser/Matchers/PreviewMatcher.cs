using System.Runtime.CompilerServices;

namespace Outrage.TokenParser.Matchers
{
    public class PreviewMatcher : IMatcher
    {
        private readonly IMatcher preview;

        public PreviewMatcher(IMatcher preview)
        {
            this.preview = preview;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Match Matches(Source input)
        {
            var source = input.Clone();
            return preview.Matches(source);
        }
    }
}
