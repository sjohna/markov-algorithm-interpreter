using MarkovAlgorithmInterpreter;
using NUnit.Framework;

namespace MarkovAlgorithmInterpreterTest
{
    [TestFixture]
    class TestFindRule
    {
        [TestCase("a", "a", 0)]
        [TestCase("a", "b", -1)]
        [TestCase("a", "", -1)]
        [TestCase("a", "aaa", 0)]
        [TestCase("a", "aba", 0)]
        [TestCase("aaa", "aabaa", -1)]
        [TestCase("abcd", "abc", -1)]
        [TestCase("", "a", 0)]
        [TestCase("", "", 0)]
        public void FindAtStart(string ToFind, string input, int expectedResult)
        {
            var rule = FindRule.Start(ToFind);

            var result = rule.Find(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("a", "a", 0)]
        [TestCase("a", "b", -1)]
        [TestCase("a", "", -1)]
        [TestCase("a", "aaa", 2)]
        [TestCase("a", "aba", 2)]
        [TestCase("aaa", "aabaa", -1)]
        [TestCase("abc", "dabc", 1)]
        [TestCase("", "a", 1)]
        [TestCase("", "abcde", 5)]
        [TestCase("abc", "abcdab", -1)]
        [TestCase("", "", 0)]
        public void FindAtEnd(string ToFind, string input, int expectedResult)
        {
            var rule = FindRule.End(ToFind);

            var result = rule.Find(input);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("a", "a", 0)]
        [TestCase("a", "b", -1)]
        [TestCase("a", "", -1)]
        [TestCase("a", "aaa", 0)]
        [TestCase("a", "aba", 0)]
        [TestCase("aaa", "aabaa", -1)]
        [TestCase("abcd", "abc", -1)]
        [TestCase("", "a", 0)]
        [TestCase("", "", 0)]
        [TestCase("abc", "abcdab", 0)]
        [TestCase("abc", "bcabcba", 2)]
        public void FindAnywhere(string ToFind, string input, int expectedResult)
        {
            var rule = FindRule.Anywhere(ToFind);

            var result = rule.Find(input);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
