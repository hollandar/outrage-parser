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
            .Text().Cast<TextToken>(text => new CommentToken(text.Value));
        
        public static IMatcher ShellStyleOpeningSingle = Matcher.Char('#');
        public static IMatcher ShellStyle =
            Matcher.Block(
                ShellStyleOpeningSingle.Ignore() // opening #
            ).Then(
                Matcher.ManyThen(
                    Characters.AnyChar,
                    Controls.EndOfLine.Or(Controls.EndOfFile.Preview()) // terminator
                )
            )
            .Text().Cast<TextToken>(text => new CommentToken(text.Value));

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
            .Text().Cast<TextToken>(text => new CommentToken(text.Value));
    }
}
