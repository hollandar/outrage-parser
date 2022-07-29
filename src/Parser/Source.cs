namespace Outrage.TokenParser
{
    public class Source
    {
        ReadOnlyMemory<char> memory;
        public Source(string source)
        {
            this.memory = source.AsMemory();
        }

        internal Source(Source source, int constrainLength)
        {
            this.memory = source.memory.Slice(0, constrainLength);
            this.Line = source.Line;
            this.Column = source.Column;
            this.Position = source.Position;
        }

        internal Source(Source from)
        {
            this.memory = from.memory;
            this.Line = from.Line;
            this.Column = from.Column;
            this.Position = from.Position;
        }

        public int Position { get; private set; } = 0;
        public int Line { get; set; } = 0;
        public int Column { get; set; } = 0;
        public ReadOnlyMemory<char> ReadOnlyMemory => this.memory.Slice(Position);
        public int Length => this.memory.Length - Position;

        public void Advance(int length)
        {
            var processedSlice = memory.Slice(Position, length);
            foreach (var c in processedSlice.Span)
            {
                Column += 1;
                if (c == '\n')
                {
                    Column = 0;
                    Line += 1;
                }
            }

            Position += length;
        }

        public Source Clone()
        {
            return new Source(this);
        }

        public Source Constrain(int length)
        {
            return new Source(this, length);
        }
    }
}
