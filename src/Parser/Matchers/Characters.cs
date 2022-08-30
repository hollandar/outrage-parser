using Outrage.TokenParser.Tokens;

namespace Outrage.TokenParser.Matchers
{
    public static class Characters
    {
        public static IMatcher AnyChar = new AnyCharacterMatcher(value => new StringValueToken(value));
        public static IMatcher Digit = new CharacterMatcher(value => (char.IsDigit(value), "Expected a digit (0-9)."), value => new StringValueToken(value));
        public static IMatcher LetterOrDigit = new CharacterMatcher(value => (char.IsLetterOrDigit(value), "Expected a letter or digit (a-z A-Z 0-9)."), value => new StringValueToken(value));
        public static IMatcher Letter = new CharacterMatcher(value => (char.IsLetter(value), "Expected a letter (a-z A-Z)."), value => new StringValueToken(value));
        public static IMatcher UppercaseLetter = new CharacterMatcher(value => (char.IsLetter(value) && char.IsUpper(value), "Expected an uppercase letter (A-Z)."), value => new StringValueToken(value));
        public static IMatcher LowercaseLetter = new CharacterMatcher(value => (char.IsLetter(value) && char.IsLower(value), "Expected a lowercase letter (a-z)."), value => new StringValueToken(value));

        public static IMatcher Period = new ExactMatcher('.', value => new StringValueToken(value));
        public static IMatcher Comma = new ExactMatcher(',', value => new StringValueToken(value));
        public static IMatcher Semicolon = new ExactMatcher(';', value => new StringValueToken(value));
        public static IMatcher LeftBracket = new ExactMatcher('(', value => new StringValueToken(value));
        public static IMatcher RightBracket = new ExactMatcher(')', value => new StringValueToken(value));
        public static IMatcher LeftSquareBracket = new ExactMatcher('[', value => new StringValueToken(value));
        public static IMatcher RightSwuareBracket = new ExactMatcher(']', value => new StringValueToken(value));
        public static IMatcher LeftBrace = new ExactMatcher('{', value => new StringValueToken(value));
        public static IMatcher RightBrace = new ExactMatcher('}', value => new StringValueToken(value));
        public static IMatcher ForwardSlash = new ExactMatcher('/', value => new StringValueToken(value));
        public static IMatcher Divide = ForwardSlash;
        public static IMatcher BackSlash = new ExactMatcher('\\', value => new StringValueToken(value));
        public static IMatcher Underscore = new ExactMatcher('_', value => new StringValueToken(value));
        public static IMatcher Plus = new ExactMatcher('+', value => new StringValueToken(value));
        public static IMatcher Minus = new ExactMatcher('-', value => new StringValueToken(value));
        public static IMatcher Hyphen = Minus;
        public static IMatcher Equal = new ExactMatcher('=', value => new StringValueToken(value));
        public static IMatcher Equality = new ExactMatcher("==", value => new StringValueToken(value));
        public static IMatcher NotEquality = new ExactMatcher("!=", value => new StringValueToken(value));
        public static IMatcher LessThan = new ExactMatcher('<', value => new StringValueToken(value));
        public static IMatcher LessThanOrEquality = new ExactMatcher("<=", value => new StringValueToken(value));
        public static IMatcher GreaterThan = new ExactMatcher('>', value => new StringValueToken(value));
        public static IMatcher GreaterThanOrEquality = new ExactMatcher(">=", value => new StringValueToken(value));
        public static IMatcher Ampersand = new ExactMatcher('&', value => new StringValueToken(value));
        public static IMatcher Multiply = new ExactMatcher('*', value => new StringValueToken(value));
        public static IMatcher Asterisk = Multiply;

        public static IMatcher Space = new ExactMatcher(' ', value => new StringValueToken(value));
        public static IMatcher Tab = new ExactMatcher('\t', value => new StringValueToken(value));
        public static IMatcher Whitespace = Matcher.Or(Space, Tab);
        public static IMatcher Whitespaces = Matcher.Some(Whitespace);

    }
}
