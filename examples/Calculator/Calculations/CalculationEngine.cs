using Calculator.Tokens;
using Outrage.TokenParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Calculations
{
    public class CalculationEngine
    {
        List<IToken> list;

        public CalculationEngine(List<IToken> list)
        {
            this.list = list;
        }

        public decimal Calculate()
        {
            return CalculateFromTokens(this.list);
        }

        private decimal CalculateFromTokens(List<IToken> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is BracketsToken)
                {
                    var bracketsToken = (BracketsToken)list[i];
                    decimal bracketsValue = CalculateFromTokens(bracketsToken.Value.ToList());

                    list[i] = new DecimalToken(bracketsValue);
                }

                if (list[i] is FunctionToken)
                {
                    var functionToken = (FunctionToken)list[i];
                    decimal functionValue = CalculateFromTokens(functionToken.Parameters.ToList());

                    decimal result = functionToken.Function switch
                    {
                        FunctionEnum.sqrt => (decimal)Math.Sqrt((double)functionValue),
                        _ => throw new Exception($"Unknown function {functionToken.Function}")
                    };

                    list[i] = new DecimalToken(result);
                }
            }

            ProcessOperation<RaiseToken>(ref list, (d1, d2) => (decimal)Math.Pow((double)d1, (double)d2));
            ProcessOperation<DivideToken>(ref list, (d1, d2) => d1 / d2);
            ProcessOperation<MultiplyToken>(ref list, (d1, d2) => d1 * d2);
            ProcessOperation<AddToken>(ref list, (d1, d2) => d1 + d2);
            ProcessOperation<SubtractToken>(ref list, (d1, d2) => d1 - d2);

            if (list.Count != 1)
                throw new Exception("Operations incomplete exception.");

            return ((DecimalToken)list.First()).Value;
        }

        private void ProcessOperation<TToken>(ref List<IToken> list, Func<decimal, decimal, decimal> op)
        {
            while (list.Where(t => t is TToken).Any())
            {
                var opInstance = list.Where(t => t is TToken).First();
                var opIndex = list.IndexOf(opInstance);
                if (opIndex <= 0 || opIndex >= list.Count - 1)
                    throw new Exception($"Operation was out of place at {opIndex}.");

                var left = list[opIndex - 1];
                var right = list[opIndex + 1];

                if (!(left is DecimalToken) || !(right is DecimalToken))
                    throw new Exception($"Decimals out of place around operation {opIndex}");

                var leftValue = ((DecimalToken)left).Value;
                var rightValue = ((DecimalToken)right).Value;

                var result = op(leftValue, rightValue);
                list.RemoveRange(opIndex - 1, 3);
                list.Insert(opIndex - 1, new DecimalToken(result));
            }
        }
    }
}
