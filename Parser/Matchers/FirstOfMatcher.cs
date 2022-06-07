using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
{
    public class FirstOfMatcher : IMatcher
    {
        private readonly IMatcher[]? matchers;

        public FirstOfMatcher(params IMatcher[]? matchers)
        {
            this.matchers = matchers;
        }

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

            return new Match ($"{String.Join(" or ", matches.Select(r => r.Error))}.");
        }
    }
}
