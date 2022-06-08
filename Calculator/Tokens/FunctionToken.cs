﻿using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tokens
{
    public enum FunctionEnum { sqrt }
    public class FunctionToken: IToken
    {
        FunctionEnum function;
        IEnumerable<IToken> paramters;

        public FunctionToken(FunctionEnum function, IEnumerable<IToken> paramters)
        {
            this.function = function;
            this.paramters = paramters;
        }

        public FunctionEnum Function => function;
        public IEnumerable<IToken> Parameters => paramters;
    }
}
