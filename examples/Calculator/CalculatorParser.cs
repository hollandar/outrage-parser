using Calculator.Tokens;
using Outrage.TokenParser;
using Outrage.TokenParser.Matchers;
using Outrage.TokenParser.Tokens;
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
            Matcher.FirstOf(
                Matcher.Some(Characters.Digit).Then(Characters.Period).Then(Matcher.Some(Characters.Digit)), 
                Matcher.Some(Characters.Digit)
            )
        ).Convert<decimal, DecimalToken>(value => new DecimalToken(value));

        public static IMatcher Sqrt = 
            Matcher.String("sqrt").Ignore()
            .Then(Matcher.Surrounded(Matcher.Ref(() => Expression), Characters.LeftBracket.Ignore(), Characters.RightBracket.Ignore()))
            .Wrap<FunctionToken>(list => new FunctionToken(FunctionEnum.sqrt, list.Tokens));

        public static IMatcher Functions =
            Matcher.FirstOf(Sqrt);

        public static IMatcher Raise = Matcher.Char('^').Produce<RaiseToken>();
        public static IMatcher Add => Characters.Plus.Produce<AddToken>();
        public static IMatcher Subtract => Characters.Minus.Produce<SubtractToken>();
        public static IMatcher Multiply => Characters.Multiply.Produce<MultiplyToken>();
        public static IMatcher Divide => Characters.Divide.Produce<DivideToken>();

        public static IMatcher Expression => Matcher.Many(
            Matcher.FirstOf(Functions, Raise, Add, Subtract, Multiply, Divide, Decimal, Matcher.Ref(() => BracketedExpression), Characters.Whitespaces.Ignore())
        );

        public static IMatcher BracketedExpression = Matcher.Surrounded(
            Expression,
            Characters.LeftBracket.Ignore(),
            Characters.RightBracket.Ignore()
        ).Wrap<BracketsToken>(list => new BracketsToken(list.Tokens));

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
                return match.Tokens;
            }
        }
    }
}
