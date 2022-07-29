namespace Outrage.TokenParser.Tokens
{
    public class CommentToken : IToken
    {
        public CommentToken(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
