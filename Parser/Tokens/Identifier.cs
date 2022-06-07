﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public class Identifier : IToken
    {
        public Identifier(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
