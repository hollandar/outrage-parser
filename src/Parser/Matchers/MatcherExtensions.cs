using Parser.Tokens;
using System.Text;

namespace Parser.Matchers
{
    public static class Matcher
    {
        public static IMatcher Ignore(this IMatcher matcher) {
            return new IgnoreMatcher(matcher);
        }

        public static IMatcher Cast(this IMatcher matcher, Func<IToken, IToken> cast)
        {
            return new CastMatcher<IToken>(matcher, cast);
        }

        public static IMatcher Cast<TFrom>(this IMatcher matcher, Func<TFrom, IToken> cast) where TFrom : IToken
        {
            return new CastMatcher<TFrom>(matcher, cast);
        }

        public static IMatcher Wrap<TToken>(this IMatcher matcher, Func<Match, TToken> cast) where TToken : IToken
        {
            return new WrapMatcher<TToken>(matcher, cast);
        }

        public static IMatcher Identifier(this IMatcher matcher)
        {
            return new WrapMatcher<IdentifierToken>(matcher, match =>
            {
                var builder = new StringBuilder();
                foreach (var token in match.Tokens.Where(t => t is StringValueToken).Cast<StringValueToken>())
                {
                    builder.Append(token.Value);
                }

                return new IdentifierToken(builder.ToString());
            });
        }

        public static IMatcher Char(char c)
        {
            return new ExactMatcher(c, v => new StringValueToken(v));
        }

        public static IMatcher String(string s)
        {
            return new ExactMatcher(s, v => new StringValueToken(v));
        }

        public static IMatcher Text(this IMatcher matcher)
        {
            return new WrapMatcher<TextToken>(matcher, match =>
            {
                var builder = new StringBuilder();

                foreach (var token in match.Tokens.Where(r => r is StringValueToken).Cast<StringValueToken>())
                {
                    builder.Append(token.Value);
                }

                return new TextToken(builder.ToString());
            });
        }
        
        public static IMatcher TextTrim(this IMatcher matcher)
        {
            return matcher.TextTrim(' ');
        }

        public static IMatcher TextTrim(this IMatcher matcher, params char[] trimChars)
        {
            return new WrapMatcher<TextToken>(matcher, match =>
            {
                var builder = new StringBuilder();

                foreach (var token in match.Tokens.Where(r => r is StringValueToken).Cast<StringValueToken>())
                {
                    builder.Append(token.Value);
                }

                return new TextToken(builder.ToString().Trim(trimChars));
            });
        }

        public static IMatcher Convert<TType, TToken>(this IMatcher matcher, Func<TType, TToken> factory) where TToken: IToken
        {
            return new WrapMatcher<TToken>(matcher, match =>
            {
                var builder = new StringBuilder();

                foreach (var token in match.Tokens.Where(t => t is StringValueToken).Cast<StringValueToken>())
                {
                    builder.Append(token.Value);
                }

                return factory((TType)System.Convert.ChangeType(builder.ToString(), typeof(TType)));
            });
        }

        public static IMatcher Produce<TToken>(this IMatcher matcher) where TToken : IToken, new()
        {
            return new CastMatcher<IToken>(matcher, _ => new TToken());
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

        public static IMatcher SurroundedUntil(this IMatcher innerMatcher, IMatcher leftMatcher, IMatcher rightMatcher)
        {
            return leftMatcher.Then(innerMatcher.Until(rightMatcher));
        }

        public static IMatcher Surrounded(this IMatcher innerMatcher, IMatcher leftMatcher, IMatcher rightMatcher)
        {
            return leftMatcher.Then(innerMatcher).Then(rightMatcher);
        }

        public static IMatcher Until(this IMatcher matcher, IMatcher until)
        {
            return new UntilMatcher(matcher, until);
        }

        public static IMatcher Ref(Func<IMatcher> referenced)
        {
            return new RefMatcher(referenced);
        }
    }
}
