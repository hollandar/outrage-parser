using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public class Comment : IToken
    {
        public Comment(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
