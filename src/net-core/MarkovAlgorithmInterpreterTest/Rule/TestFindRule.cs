using MarkovAlgorithmInterpreter;
using NUnit.Framework;
using System.Text;

namespace MarkovAlgorithmInterpreterTest
{
    [TestFixture(InputType.String)]
    [TestFixture(InputType.StringBuilder)]
    class TestFindRule
    {
        private InputType inputType;

        public TestFindRule(InputType inputType)
        {
            this.inputType = inputType;
        }

        public enum InputType
        {
            String,
            StringBuilder
        }

        private int Find(FindRule rule, string input)
        {
            if (inputType == InputType.String)
            {
                return rule.Find(input);
            }
            else
            {
                return rule.Find(new StringBuilder(input));
            }
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
        [TestCase("123", "abc123", -1)]
        public void FindAtStart(string ToFind, string input, int expectedResult)
        {
            var rule = FindRule.Start(ToFind);

            var result = this.Find(rule, input);

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
        [TestCase("123", "abc123", 3)]
        public void FindAtEnd(string ToFind, string input, int expectedResult)
        {
            var rule = FindRule.End(ToFind);

            var result = this.Find(rule, input);

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
        [TestCase("abc", "abc123", 0)]
        [TestCase("123", "abc123", 3)]
        public void FindAnywhere(string ToFind, string input, int expectedResult)
        {
            var rule = FindRule.Anywhere(ToFind);

            var result = this.Find(rule, input);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
