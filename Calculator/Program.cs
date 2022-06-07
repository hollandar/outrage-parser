// See https://aka.ms/new-console-template for more information

using Calculator;
using System.Text.Json;

var tokenList = CalculatorParser.ParseExpression("(1)");
Console.WriteLine(JsonSerializer.Serialize(tokenList));