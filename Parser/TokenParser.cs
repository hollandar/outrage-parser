using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class TokenParser
    {
        public static Match Parse(string input, IMatcher rootMatcher)
        {
            var memory = new Source(input);
            var match = rootMatcher.Matches(memory);

            if (match.Success == false)
            {
                return new Match($"Line {memory.Line}, Col {memory.Column}; {match.Error}");
            }
            else
            {
                return match;
            }
        }
    }
}
