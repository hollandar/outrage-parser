using Outrage.TokenParser;

namespace Calculator.Tokens
{
    internal class DecimalToken: IToken
    {
        public DecimalToken(decimal value)
        {
            this.Value = value;
        }

        public decimal Value { get; set; }
    }
}
