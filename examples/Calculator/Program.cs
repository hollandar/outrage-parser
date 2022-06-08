// See https://aka.ms/new-console-template for more information

using Calculator;
using Calculator.Calculations;
using System.Text.Json;

Console.WriteLine("Calc ('exit' to quit)");

while (true)
{
    Console.Write("calc> ");
    var expression = Console.ReadLine();
    if (expression == "exit")
        break;

    try
    {
        var tokenList = CalculatorParser.ParseExpression(expression);

        var tokens = tokenList.ToList();
        var engine = new CalculationEngine(tokens);
        Console.WriteLine(engine.Calculate());

    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

