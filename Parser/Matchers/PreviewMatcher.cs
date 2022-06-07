using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Matchers
{
    public class PreviewMatcher : IMatcher
    {
        private readonly IMatcher preview;

        public PreviewMatcher(IMatcher preview)
        {
            this.preview = preview;
        }

        public Match Matches(Source input)
        {
            var source = input.Clone();
            return preview.Matches(source);
        }
    }
}
