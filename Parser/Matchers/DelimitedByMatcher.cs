﻿using Parser.Tokens;

namespace Parser.Matchers
{
    public class DelimitedByMatcher : IMatcher
    {
        private readonly IMatcher term;
        private readonly IMatcher delimiter;
        private readonly int minOccurence;
        private readonly int maxOccurence;

        public DelimitedByMatcher(IMatcher term, IMatcher delimiter, int minOccurence = 0, int maxOccurence = int.MaxValue)
        {
            this.term = term;
            this.delimiter = delimiter;
            this.minOccurence = minOccurence;
            this.maxOccurence = maxOccurence;
        }

        public Match Matches(Source input)
        {
            int occurrences = 0;
            List<IToken> tokens = new List<IToken>();
            do
            {
                var termMatch = term.Matches(input);
                if (termMatch.Success)
                {
                    if (termMatch.Token != null) tokens.Add(termMatch.Token);
                    occurrences++;
                }
                else
                {
                    return termMatch;
                }

                var delimiterMatch = delimiter.Matches(input);
                if (delimiterMatch.Success)
                {
                    if (delimiterMatch.Token != null) tokens.Add(delimiterMatch.Token);
                    continue;
                }
                else
                {
                    break;
                }
            } while (true);

            if (occurrences < minOccurence || occurrences > maxOccurence) {
                return new Match($"expected between {minOccurence} and {maxOccurence}");
            }
            return new Match(new TokenList(tokens));
        }
    }
}
