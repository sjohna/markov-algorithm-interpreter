using MarkovAlgorithmInterpreter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreterTest
{
    [TestFixture]
    class TestReplaceRule
    {
        [TestCase("", "", "a", "a")]
        [TestCase("b", "", "a", "ab")]
        [TestCase("", "b", "a", "ab")]
        [TestCase("", "", "", "")]
        [TestCase("Hello", "world!", ", ", ", Helloworld!")]

        public void AtStart(string before, string after, string replace, string expectedResult)
        {
            var rule = ReplaceRule.Start(replace);

            var result = rule.Replace(before, after);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("", "", "a", "a")]
        [TestCase("b", "", "a", "ba")]
        [TestCase("", "b", "a", "ba")]
        [TestCase("", "", "", "")]
        [TestCase("Hello", "world!", ", ", "Helloworld!, ")]

        public void AtEnd(string before, string after, string replace, string expectedResult)
        {
            var rule = ReplaceRule.End(replace);

            var result = rule.Replace(before, after);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("", "", "a" ,"a")]
        [TestCase("b", "", "a", "ba")]
        [TestCase("", "b", "a", "ab")]
        [TestCase("", "", "", "")]
        [TestCase("Hello", "world!", ", ", "Hello, world!")]

        public void Inline(string before, string after, string replace, string expectedResult)
        {
            var rule = ReplaceRule.Inline(replace);

            var result = rule.Replace(before, after);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
