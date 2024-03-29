﻿using System.Runtime.CompilerServices;

namespace Outrage.TokenParser.Matchers
{
    public class FirstOfMatcher : IMatcher
    {
        private readonly IMatcher[]? matchers;

        public FirstOfMatcher(params IMatcher[]? matchers)
        {
            this.matchers = matchers;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Match Matches(Source input)
        {
            List<Match> matches = new List<Match>();
            if (matchers != null)
            {
                foreach (var matcher in matchers)
                {
                    var match = matcher.Matches(input);
                    if (match.Success)
                        return match;
                    matches.Add(match);
                }
            }

            return new Match (() => $"{String.Join(" or ", matches.Select(r => r.Error()))}.");
        }
    }
}
