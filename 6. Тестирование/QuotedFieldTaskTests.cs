using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase(@"'a\'a'", 0, @"a'a", 6)]
        [TestCase(@"'a\\'a'", 0, @"a\", 5)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var isActive = false;
            var newLineLength = 1;
            var newLine = new StringBuilder();

            if (startIndex < line.Length - 1)
            {
                for (int i = startIndex + 1; i < line.Length; i++)
                {
                    newLineLength++;
                    
                    if (!isActive && line[i] == line[startIndex] )
                        break;

                    isActive = line[i] == '\\' && !isActive;

                    if (isActive)
                        continue;
                    
                    newLine.Append(line[i]);
                }
            }
            return new Token(newLine.ToString(), startIndex, newLineLength);
        }
    }
}