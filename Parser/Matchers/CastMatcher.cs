using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
{
    public class CastMatcher<TFrom, TTo> : IMatcher where TFrom: IToken where TTo: IToken
    {
        private readonly IMatcher inner;
        private readonly Func<TFrom, TTo> convert;

        public CastMatcher(IMatcher inner, Func<TFrom, TTo> convert)
        {
            this.inner = inner;
            this.convert = convert;
        }


        public Match Matches(Source input)
        {
            var match = inner.Matches(input);
            if (match.Success && match.Token != null && match.Token is TFrom)
            {
                return new Match(convert((TFrom)match.Token));
            }

            return match;
        }
    }
}
