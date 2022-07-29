using Outrage.TokenParser;

namespace Calculator.Tokens
{
    public class BracketsToken : IToken
    {
        public BracketsToken(IEnumerable<IToken> tokens)
        {
            this.Value = tokens;
        }

        public IEnumerable<IToken> Value { get; }
    }
}
