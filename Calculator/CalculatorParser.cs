using Calculator.Tokens;
using Parser;
using Parser.Matchers;
using Parser.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal static class CalculatorParser
    {
        public static IMatcher Decimal = Matcher.Some(
            Matcher.FirstOf(Characters.Digit, Characters.Period)
        ).Convert<decimal, DecimalToken>(value => new DecimalToken(value));

        public static IMatcher Add => Characters.Plus.Produce<AddToken>();
        public static IMatcher Subtract => Characters.Minus.Produce<SubtractToken>();
        public static IMatcher Multiply => Characters.Multiply.Produce<MultiplyToken>();
        public static IMatcher Divide => Characters.Divide.Produce<DivideToken>();

        public static IMatcher Expression => Matcher.Many(
            Matcher.FirstOf(Add, Subtract, Multiply, Divide, Decimal, Matcher.Ref(() => BracketedExpression), Characters.Whitespaces.Ignore())
        );

        public static IMatcher BracketedExpression = Matcher.Surrounded(
            Expression,
            Characters.LeftBracket.Ignore(),
            Characters.RightBracket.Ignore()
        ).Cast<TokenList, BracketsToken>(list => new BracketsToken(list));

        public static IMatcher Calculation = Expression.Then(Controls.EndOfFile.Ignore());

        public static IEnumerable<IToken> ParseExpression(string expression)
        {
            var match = TokenParser.Parse(expression, Calculation);
            if (!match.Success)
            {
                throw new Exception(match.Error);
            }
            else
            {
                Debug.Assert(match.Token is TokenList);
                return (match.Token as TokenList).Flatten();
            }
        }
    }
}
