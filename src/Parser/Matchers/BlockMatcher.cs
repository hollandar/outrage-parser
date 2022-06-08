using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
{
    public class BlockMatcher : IMatcher
    {
        private IMatcher innerMatcher;

        public BlockMatcher(IMatcher inner)
        {
            this.innerMatcher = inner;
        }

        public Match Matches(Source input)
        {
            return innerMatcher.Matches(input);
        }
    }
}
