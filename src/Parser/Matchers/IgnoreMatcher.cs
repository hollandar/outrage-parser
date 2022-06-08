using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
{
    public class IgnoreMatcher : IMatcher
    {
        private readonly IMatcher ignore;

        public IgnoreMatcher(IMatcher ignore)
        {
            this.ignore = ignore;
        }

        public Match Matches(Source input)
        {
            var match = ignore.Matches(input);
            if (match.Success)
            {
                return new Match();
            }

            return match;
        }
    }
}
