namespace Outrage.TokenParser.Tokens
{
    public class TextToken : IToken
    {
        public TextToken(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
