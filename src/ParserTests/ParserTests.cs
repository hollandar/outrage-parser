using Parser;
using Parser.Matchers;
using Parser.Tokens;

namespace TestProject1
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Lf()
        {
            var code = "\n";
            var ast = TokenParser.Parse(code, Controls.EndOfLine);

            Assert.IsTrue(ast.Success);
            Assert.IsInstanceOfType(ast.Tokens.Single(), typeof(EndOfLine));
        }

        [TestMethod]
        public void Crlf()
        {
            var code = "\r\n";
            var ast = TokenParser.Parse(code, Controls.EndOfLine);

            Assert.IsTrue(ast.Success);
            Assert.IsInstanceOfType(ast.Tokens.First(), typeof(EndOfLine));
        }

        [TestMethod]
        public void LfEof()
        {
            var code = "\n";
            var ast = TokenParser.Parse(code, Controls.EndOfLine.Then(Controls.EndOfFile));

            Assert.IsTrue(ast.Success);
            var list = ast.Tokens.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(EndOfLine));
            Assert.IsInstanceOfType(list[1], typeof(EndOfFile));
        }

        [TestMethod]
        public void NoEndLine()
        {
            var code = "";
            var ast = TokenParser.Parse(code, Controls.EndOfLine.Then(Controls.EndOfFile));

            Assert.IsFalse(ast.Success);
            Assert.AreEqual("Line 0, Col 0; not enough input to match \\r\\n or not enough input to match \\n.; at ''", ast.Error);
        }

        [TestMethod]
        public void OptionalEndline()
        {
            var code = "";
            var ast = TokenParser.Parse(code, Controls.EndOfLine.ManyThen(Controls.EndOfFile));

            Assert.IsTrue(ast.Success);
            var list = ast.Tokens.ToList();
            Assert.AreEqual(1, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(EndOfFile));
        }

        [TestMethod]
        public void Ignore()
        {
            var code = "a,b";
            var ast = TokenParser.Parse(code,
                Matcher.ManyThen(
                    Matcher.FirstOf(
                        Characters.Comma.Ignore(),
                        Characters.Letter
                    ),
                    Controls.EndOfFile)
                );

            var list = ast.Tokens.ToList();
            Assert.AreEqual(3, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(StringValue));
            Assert.AreEqual("a", (list[0] as StringValue).Value.ToString());
            Assert.IsInstanceOfType(list[1], typeof(StringValue));
            Assert.AreEqual("b", (list[1] as StringValue).Value.ToString());
            Assert.IsInstanceOfType(list[2], typeof(EndOfFile));
        }

        [TestMethod]
        public void DelimitedBy()
        {
            var code = "a,b,c";
            var ast = TokenParser.Parse(code,
                Characters.Letter.DelimitedBy(Characters.Comma.Ignore()).Then(Controls.EndOfFile)
            );

            var list = ast.Tokens.ToList();
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("a", (list[0] as StringValue).Value.ToString());
            Assert.AreEqual("b", (list[1] as StringValue).Value.ToString());
            Assert.AreEqual("c", (list[2] as StringValue).Value.ToString());
            Assert.IsInstanceOfType(list[3], typeof(EndOfFile));
        }

        [TestMethod]
        public void Identifier()
        {
            var code = "identifier";
            var ast = TokenParser.Parse(code, 
                Identfiers.Identifier.Then(Controls.EndOfFile)
            );

            Assert.IsTrue(ast.Success);
            var list = ast.Tokens.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(Identifier));
            Assert.AreEqual("identifier", (list[0] as Identifier).Value);
        }

        [TestMethod]
        public void CStyle()
        {
            var code = "// comment\r\n";
            var ast = TokenParser.Parse(code,
                Comments.CStyle.Then(Controls.EndOfFile)
            );

            Assert.IsTrue(ast.Success);
            var list = ast.Tokens.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(Comment));
            Assert.AreEqual(" comment", ((Comment)list[0]).Value);
        }

        [TestMethod]
        public void CStyleNoNewline()
        {
            var code = "// comment";
            var ast = TokenParser.Parse(code,
                Comments.CStyle.Then(Controls.EndOfFile)
            );

            Assert.IsTrue(ast.Success);
            var list = ast.Tokens.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(Comment));
            Assert.AreEqual(" comment", ((Comment)list[0]).Value);
        }

        [TestMethod]
        public void CStyleInline()
        {
            var code = "/* comment */";
            var ast = TokenParser.Parse(code,
                Comments.CStyleInline.Then(Controls.EndOfFile)
            );

            Assert.IsTrue(ast.Success);
            var list = ast.Tokens.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(Comment));
            Assert.AreEqual(" comment ", ((Comment)list[0]).Value);
        }

        [TestMethod]
        public void CStyleMultiple()
        {
            var code = "//one\r\n//two\r\n//three\r\n";
            var ast = TokenParser.Parse(code, Comments.CStyle.ManyThen(Controls.EndOfFile));

            Assert.IsTrue(ast.Success);
            var list = ast.Tokens.ToList();
            Assert.AreEqual(4, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(Comment));
            Assert.IsInstanceOfType(list[1], typeof(Comment));
            Assert.IsInstanceOfType(list[2], typeof(Comment));
            Assert.AreEqual("three", ((Comment)list[2]).Value);
            Assert.IsInstanceOfType(list[3], typeof(EndOfFile));
        }

        [TestMethod]
        public void Surrounded()
        {
            var code = "{abc}";
            var ast = TokenParser.Parse(code, Identfiers.Identifier.Surrounded(Characters.LeftBrace, Characters.RightBrace).Then(Controls.EndOfFile));

            Assert.IsTrue(ast.Success);
            var list = ast.Tokens.ToList();
            Assert.AreEqual(4, list.Count);
            Assert.IsInstanceOfType(list[1], typeof(Identifier));
            Assert.AreEqual("abc", ((Identifier)list[1]).Value);
        }
    }
}