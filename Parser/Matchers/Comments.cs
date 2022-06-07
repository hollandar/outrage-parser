using Parser.Tokens;

namespace Parser.Matchers
{
    public static class Comments
    {
        public static IMatcher CStyleOpeningSingle = Characters.ForwardSlash.Then(Characters.ForwardSlash);
        public static IMatcher CStyle =
            Matcher.Block(
                CStyleOpeningSingle.Ignore() // opening //
            ).Then(
                Matcher.ManyThen(
                    Characters.AnyChar,
                    Controls.EndOfLine.Or(Controls.EndOfFile.Preview()) // terminator
                )
            )
            .Text().Cast<Text, Comment>(text => new Comment(text.Value));

        public static IMatcher CStyleOpening = Characters.ForwardSlash.Then(Characters.Asterisk);
        public static IMatcher CStyleClosing = Characters.Asterisk.Then(Characters.ForwardSlash);

        public static IMatcher CStyleInline =
            Matcher.Block(
                CStyleOpening.Ignore() // opening /*
            ).Then(
                Matcher.ManyThen(
                    Characters.AnyChar,
                    CStyleClosing.Ignore() // closing */
                )
            )
            .Text().Cast<Text, Comment>(text => new Comment(text.Value));
    }
}
