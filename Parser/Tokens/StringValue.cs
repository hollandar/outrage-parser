using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public class StringValue: IToken
    {
        public StringValue(ReadOnlyMemory<char> value)
        {
            this.Value = value;
        }

        public ReadOnlyMemory<char> Value { get; }
    }
}
