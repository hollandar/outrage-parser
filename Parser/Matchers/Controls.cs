using Parser.Tokens;

namespace Parser.Matchers
{
    public static class Controls
    {
        public static IMatcher ControlLineFeed = new ExactMatcher("\r\n".ToArray(), value => new EndOfLine());
        public static IMatcher LineFeed = new ExactMatcher("\n".ToArray(), value => new EndOfLine());
        public static IMatcher EndOfLine = new FirstOfMatcher(ControlLineFeed, LineFeed);
        public static IMatcher EndOfFile = new EndOfFileMatcher();
    }
}
