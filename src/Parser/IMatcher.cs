namespace Outrage.TokenParser
{
    public struct Match
    {
        public Match(IToken token)
        {
            Success = true;
            Tokens = Enumerable.Repeat(token, 1);
        }

        public Match(IEnumerable<IToken> tokens)
        {
            Success = true;
            Tokens = tokens;
        }

        public Match(string error)
        {
            Success = false;
            Error = error;
        }

        public Match()
        {
            Success = true;
            Tokens = Enumerable.Empty<IToken>();
        }

        public bool Success { get; } = false;
        //public int Length { get; } = 0;
        public IEnumerable<IToken> Tokens { get; } = Enumerable.Empty<IToken>();
        public string Error { get; } = String.Empty;
    }

    public interface IMatcher
    {
        Match Matches(Source input);
    }
}
