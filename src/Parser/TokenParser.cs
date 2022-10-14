namespace Outrage.TokenParser
{
    public class TokenParser
    {
        public static Match Parse(string input, IMatcher rootMatcher)
        {
            var memory = new Source(input);
            var match = rootMatcher.Matches(memory);

            if (match.Success == false)
            {
                return new Match(() => $"Line {memory.Line}, Col {memory.Column}; {match.Error()}; at '{memory.ReadOnlyMemory}'");
            }
            else
            {
                return match;
            }
        }
    }
}
