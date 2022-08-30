namespace Outrage.TokenParser.Tokens
{
    public class StringValueToken: IToken
    {
        public StringValueToken(ReadOnlyMemory<char> value)
        {
            this.Value = value;
        }

        public ReadOnlyMemory<char> Value { get; }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
