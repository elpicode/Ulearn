using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }
        
        [TestCase("text", new[] {"text"})]
        [TestCase("hello world", new[] {"hello", "world"})]
        [TestCase("hello   world", new[] {"hello", "world"})]
        [TestCase("'world'", new[] {"world"})]
        [TestCase("\"world\"", new[] {"world"})]
        [TestCase("'hello world'", new[] {"hello world"})]
        [TestCase("'\"hello world\"'", new[] {"\"hello world\""})]
        [TestCase("\"'hello world'\"", new[] {"'hello world'"})]
        [TestCase("", new string[0])]
        [TestCase("'' \"abc gh\" 'y z'", new[] {"", "abc gh", "y z"})]
        [TestCase("hi'hello'bonjour", new[] {"hi", "hello", "bonjour"})]
        [TestCase("'hello", new[] {"hello"})]
        [TestCase("'hel\\'lo'", new[] {"hel'lo"})]
        [TestCase("\"hel\\\"lo\"", new[] {"hel\"lo"})]
        [TestCase("\"hello \\\"world\\\"\"", new[] {"hello \"world\""})]
        [TestCase("\"\\\\\"", new[] {"\\"})]
        [TestCase(" b ", new[] {"b"})]
        [TestCase("'c ", new[] {"c "})]

        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var index = 0;
            var token = new List<Token>();

            while (index < line.Length)
            {
                if (line[index] == ' ')
                {
                    index++;
                    continue;
                }

                var newToken = line[index] == '\'' || line[index] == '"'
                    ? QuotedFieldTask.ReadQuotedField(line, index)
                    : ReadField(line, index);
                
                token.Add(newToken);
                index += newToken.Length;
            }
            return token;
        }
        
        private static Token ReadField(string line, int startIndex)
        {
            var newLine = new StringBuilder();

            for (int i = startIndex; i < line.Length; i++)
            {
                if (line[i] == '"' || line[i] == ' ' || line[i] == '\'')
                    break;

                newLine.Append(line[i]);
            }
            return new Token(newLine.ToString(), startIndex, newLine.Length);
        }
    }
}