using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tokens
{
    public class IgnoreToken: IToken
    {
        public static IgnoreToken Token = new IgnoreToken();
    }
}
