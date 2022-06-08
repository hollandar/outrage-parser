using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
{
    public class WrapMatcher<TTo> : IMatcher where TTo: IToken
    {
        private readonly IMatcher inner;
        private readonly Func<Match, TTo> wrap;

        public WrapMatcher(IMatcher inner, Func<Match, TTo> wrap)
        {
            this.inner = inner;
            this.wrap = wrap;
        }

        public Match Matches(Source input)
        {
            var match = inner.Matches(input);

            if (!match.Success)
                return match;
            else
                return new Match(wrap(match));
        }
    }
}
