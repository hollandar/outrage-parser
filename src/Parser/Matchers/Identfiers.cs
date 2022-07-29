namespace Outrage.TokenParser.Matchers
{
    public static class Identfiers
    {
        public static IMatcher Identifier = Matcher.Many(
            Matcher.Or(
                Characters.LetterOrDigit,
                Characters.Underscore
            )
        ).Identifier();
    }
}
