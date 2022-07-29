namespace Outrage.TokenParser.Tokens
{
    public class IdentifierToken : IToken
    {
        public IdentifierToken(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
