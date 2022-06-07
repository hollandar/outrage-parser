using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
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
