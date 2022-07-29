namespace Outrage.TokenParser.Matchers
{
    public class CastMatcher<TFrom>: IMatcher where TFrom: IToken
    {
        private readonly IMatcher inner;
        private readonly Func<TFrom, IToken> convert;

        public CastMatcher(IMatcher inner, Func<TFrom, IToken> convert)
        {
            this.inner = inner;
            this.convert = convert;
        }


        public Match Matches(Source input)
        {
            var match = inner.Matches(input);
            if (match.Success && match.Tokens != null)
            {
                return new Match(match.Tokens.Select( t => t is TFrom? convert((TFrom)t): t));
            }

            return match;
        }
    }
}
