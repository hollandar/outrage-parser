﻿using Outrage.TokenParser.Tokens;

namespace Outrage.TokenParser.Matchers
{
    public static class Controls
    {
        public static IMatcher ControlLineFeed = new ExactMatcher("\r\n".ToArray(), value => new EndOfLineToken());
        public static IMatcher LineFeed = new ExactMatcher("\n".ToArray(), value => new EndOfLineToken());
        public static IMatcher EndOfLine = new FirstOfMatcher(ControlLineFeed, LineFeed);
        public static IMatcher EndOfFile = new EndOfFileMatcher();
    }
}
