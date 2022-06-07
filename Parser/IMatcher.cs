using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public struct Match
    {
        public Match(IToken token)
        {
            Success = true;
            //Length = length;
            Token = token;
        }

        public Match(string error)
        {
            Success = false;
            Error = error;
        }

        public bool Success { get; } = false;
        //public int Length { get; } = 0;
        public IToken? Token { get; } = null;
        public string Error { get; } = String.Empty;
    }

    public interface IMatcher
    {
        Match Matches(Source input);
    }
}
