﻿namespace Outrage.TokenParser.Matchers
{
    public class RefMatcher: IMatcher
    {
        Func<IMatcher> referenced;

        public RefMatcher(Func<IMatcher> referenced)
        {
            this.referenced = referenced;
        }

        public Match Matches(Source input)
        {
            return referenced().Matches(input);
        }
    }
}
