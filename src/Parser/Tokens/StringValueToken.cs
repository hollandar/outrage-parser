using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public class StringValueToken: IToken
    {
        public StringValueToken(ReadOnlyMemory<char> value)
        {
            this.Value = value;
        }

        public ReadOnlyMemory<char> Value { get; }
    }
}
