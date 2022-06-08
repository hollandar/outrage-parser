# Outrage Parser

A fast and flexible tokenizing parser for text instructions that you can actually understand.
It can flexibly tokenize a test structure, capturing heirarchical tokens where you need that.
Your token tree then drives how you interpret the instruction provided, assuming there were no errors parsing the token tree.

Help about parsing errors is provided, but can be improved.  In many cases it wont tell you what it expected, but will tell you why and where it failed to parse the text.

# Acutally understandable?

Yes, similar parsers make use of complex linq queries.  You dont need to do that here.

Your parser maps the found content into tokens, under your control.  You can easily break the parsing, or ignore parsed elements so they dont generate tokens but do contribute to the matching process, or preview items without consuming them.

Here is a basic example of parser expecting you to answer the question "What is your name?" with "My name is {name}.".

```c#
public static class MyNameIsParser {
    public static IMatcher MyNameIs = Matcher.String("My name is").Then(Characters.Whitespaces).Ignore();
    public static IMatcher Name = Matcher.Some(Characters.Letter).Then(Characters.Period.Ignore()).Text();
    public static IMatcher Source = MyNameIs.Then(Name).Then(Controls.EndOfFile);
    public static Match Process(string input) => TokenParser.Parse(input, Source);
}
```

In this scenario (will find in the unit tests) the matchers will:

1. Match "My name is" followed by any amount of whitespace, Ignore means this doesnt generate a token.
2. Match a name as a series of Characters.Letter (at least one) followed by a period (which it ignores) and produces a text token.
3. Match the end of the file.

The matchers are shown in the Source matcher, which refers by to the MyNameIs and Name matchers.

The rest of this will be a Match object with two Tokens (assuming Match.Success is true):
1. TextToken( Value = "YourName" )
2. EndOfFileToken

If the match fails (for example; you include a space in your name) you will get back Match (Success = false, Error = {what was wrong, and where}).

# Tests

Parser includes a test project to validate the base matching capabilities, what is also an example that provides a variety of usage models.

# License

Not yet defined.
