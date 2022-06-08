using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public class Text : IToken
    {
        public Text(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
