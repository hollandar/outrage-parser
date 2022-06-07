using Parser.Tokens;
using System.Text;

namespace Parser.Matchers
{
    public static class Matcher
    {
        public static IMatcher Ignore(this IMatcher matcher) {
            return new CastMatcher<IToken, IgnoreToken>(matcher, token => IgnoreToken.Token);
        }

        public static IMatcher Cast(this IMatcher matcher, Func<IToken, IToken> cast)
        {
            return new CastMatcher<IToken, IToken>(matcher, cast);
        }

        public static IMatcher Cast<TFrom, TTo>(this IMatcher matcher, Func<TFrom, TTo> cast) where TFrom : IToken where TTo : IToken
        {
            return new CastMatcher<TFrom, TTo>(matcher, cast);
        }

        public static IMatcher Cast<TFrom>(this IMatcher matcher, Func<TFrom, IToken> cast) where TFrom : IToken
        {
            return new CastMatcher<TFrom, IToken>(matcher, cast);
        }

        public static IMatcher Identifier(this IMatcher matcher)
        {
            return new CastMatcher<TokenList, Identifier>(matcher, tokenList =>
            {
                var builder = new StringBuilder();
                foreach (var token in tokenList.Value.Cast<StringValue>())
                {
                    builder.Append(token.Value);
                }

                return new Identifier(builder.ToString());
            });
        }


        private static void FlattenTokenList(StringBuilder builder, TokenList tokenList)
        {
            foreach (var item in tokenList.Value)
            {
                if (item is StringValue)
                {
                    builder.Append((item as StringValue).Value);
                }
                if (item is TokenList)
                {
                    FlattenTokenList(builder, item as TokenList);
                }
            }
        }

        public static IMatcher Text(this IMatcher matcher)
        {
            return new CastMatcher<TokenList, Text>(matcher, tokenList =>
            {
                var builder = new StringBuilder();

                FlattenTokenList(builder, tokenList);

                return new Text(builder.ToString());
            });
        }

        public static IMatcher Convert<TType, TToken>(this IMatcher matcher, Func<TType, TToken> factory) where TToken: IToken
        {
            return new CastMatcher<TokenList, TToken>(matcher, tokenList =>
            {
                var builder = new StringBuilder();

                FlattenTokenList(builder, tokenList);

                return factory((TType)System.Convert.ChangeType(builder.ToString(), typeof(TType)));
            });
        }

        public static IMatcher Produce<TToken>(this IMatcher matcher) where TToken : IToken, new()
        {
            return new CastMatcher<IToken, TToken>(matcher, _ => new TToken());
        }

        public static IMatcher Then(this IMatcher matcher, IMatcher followon)
        {
            return new ThenMatcher(matcher, followon);
        }

        public static IMatcher Many(this IMatcher manyMatcher)
        {
            return new ManyMatcher(manyMatcher);
        }

        public static IMatcher Some(this IMatcher manyMatcher, int minimumMatches = 1)
        {
            return new ManyMatcher(manyMatcher, minimumMatches);
        }

        public static IMatcher ManyThen(this IMatcher manyMatcher, IMatcher thenMatcher)
        {
            return new ManyThenMatcher(manyMatcher, thenMatcher);
        }

        public static IMatcher SomeThen(this IMatcher manyMatcher, IMatcher thenMatcher, int minimumMatches = 1)
        {
            return new ManyThenMatcher(manyMatcher, thenMatcher, minimumMatches);
        }

        public static IMatcher FirstOf(params IMatcher[]? firstOfMatches)
        {
            return new FirstOfMatcher(firstOfMatches);
        }

        public static IMatcher Block(this IMatcher inner)
        {
            var blockMatcher = new BlockMatcher(inner);

            return blockMatcher;
        }

        public static IMatcher Preview(this IMatcher input)
        {
            var previewMatcher = new PreviewMatcher(input);

            return previewMatcher;
        }

        public static IMatcher Or(this IMatcher input, IMatcher other)
        {
            var firstOfMatcher = new FirstOfMatcher(input, other);

            return firstOfMatcher;
        }

        public static IMatcher DelimitedBy(this IMatcher input, IMatcher delimiter, int minOccurrence = 0, int maxOccurence = int.MaxValue)
        {
            return new DelimitedByMatcher(input, delimiter, minOccurrence, maxOccurence);
        }

        public static IMatcher Surrounded(this IMatcher innerMatcher, IMatcher leftMatcher, IMatcher rightMatcher)
        {
            return leftMatcher.Then(innerMatcher).Then(rightMatcher);
        }

        public static IMatcher Ref(Func<IMatcher> referenced)
        {
            return new RefMatcher(referenced);
        }
    }
}
