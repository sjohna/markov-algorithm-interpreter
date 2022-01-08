using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkovAlgorithmInterpreter;

namespace MarkovAlgorithmInterpreterTest
{
    [TestFixture(InputType.String)]
    [TestFixture(InputType.StringBuilder)]
    [TestFixture(InputType.WorkingString)]
    class TestFindReplaceRule
    {
        private InputType inputType;

        public TestFindReplaceRule(InputType inputType)
        {
            this.inputType = inputType;
        }

        public enum InputType
        {
            String,
            StringBuilder,
            WorkingString
        }

        private void DoApplicableTest(FindReplaceRule rule, string input, string expectedOutput)
        {
            if (inputType == InputType.String)
            {
                var application = rule.Apply(input);

                Assert.IsTrue(application.Applied);
                Assert.AreEqual(expectedOutput, application.Application);
            }
            else if (inputType == InputType.StringBuilder)
            {
                var application = rule.Apply(new StringBuilder(input));

                Assert.IsTrue(application.Applied);
                Assert.AreEqual(expectedOutput, application.Application.ToString());
            }
            else if (inputType == InputType.WorkingString)
            {
                var application = rule.Apply(new WorkingString(input));

                Assert.IsTrue(application.Applied);
                Assert.AreEqual(expectedOutput, application.Application.ToString());
            }
        }

        private void DoInapplicableTest(FindReplaceRule rule, string input)
        {
            if (inputType == InputType.String)
            {
                var application = rule.Apply(input);

                Assert.IsFalse(application.Applied);
            }
            else
            {
                var application = rule.Apply(new StringBuilder(input));

                Assert.IsFalse(application.Applied);
            }
        }

        [TestCase("a", "b", "a", "b")]
        [TestCase("a", "b", "aaaaa", "baaaa")]
        [TestCase("a", "b", "cccaccc", "cccbccc")]
        [TestCase("a", "b", "cccca", "ccccb")]
        [TestCase("abc", "cba", "AabcB", "AcbaB")]
        [TestCase("hello", "world", "hello", "world")]
        [TestCase("abc", "", "AabcB", "AB")]
        [TestCase("abc", "", "abcAB", "AB")]
        [TestCase("abc", "", "ABabc", "AB")]
        [TestCase("abc", "", "abcAabcBabc", "AabcBabc")]
        [TestCase("", "abc", "AB", "abcAB")]
        [TestCase("", "abc", "", "abc")]
        public void ApplicableFindAnywhereReplaceInlineRule(string find, string replace, string input, string expectedOutput)
        {
            var rule = FindReplaceRule.Create(find, replace);

            DoApplicableTest(rule, input, expectedOutput);
        }

        [TestCase("a", "b", "a", "b")]
        [TestCase("a", "b", "aaaaa", "baaaa")]
        [TestCase("a", "b", "cccaccc", "bcccccc")]
        [TestCase("a", "b", "cccca", "bcccc")]
        [TestCase("abc", "cba", "AabcB", "cbaAB")]
        [TestCase("hello", "world", "hello", "world")]
        [TestCase("abc", "", "AabcB", "AB")]
        [TestCase("abc", "", "abcAB", "AB")]
        [TestCase("abc", "", "ABabc", "AB")]
        [TestCase("abc", "", "abcAabcBabc", "AabcBabc")]
        [TestCase("", "abc", "AB", "abcAB")]
        [TestCase("", "abc", "", "abc")]
        public void ApplicableFindAnywhereReplaceAtStartRule(string find, string replace, string input, string expectedOutput)
        {
            var rule = new FindReplaceRule(new FindRule(find, FindRule.Location.Anywhere), new ReplaceRule(replace, ReplaceRule.Location.Start));

            DoApplicableTest(rule, input, expectedOutput);
        }

        [TestCase("a", "b", "a", "b")]
        [TestCase("a", "b", "aaaaa", "aaaab")]
        [TestCase("a", "b", "cccaccc", "ccccccb")]
        [TestCase("a", "b", "cccca", "ccccb")]
        [TestCase("abc", "cba", "AabcB", "ABcba")]
        [TestCase("hello", "world", "hello", "world")]
        [TestCase("abc", "", "AabcB", "AB")]
        [TestCase("abc", "", "abcAB", "AB")]
        [TestCase("abc", "", "ABabc", "AB")]
        [TestCase("abc", "", "abcAabcBabc", "AabcBabc")]
        [TestCase("", "abc", "AB", "ABabc")]
        [TestCase("", "abc", "", "abc")]
        public void ApplicableFindAnywhereReplaceAtEndRule(string find, string replace, string input, string expectedOutput)
        {
            var rule = new FindReplaceRule(new FindRule(find, FindRule.Location.Anywhere), new ReplaceRule(replace, ReplaceRule.Location.End));

            DoApplicableTest(rule, input, expectedOutput);
        }

        [TestCase("a", "b", "cbcbc")]
        [TestCase("a", "b", "")]
        [TestCase("a", "", "cbcbc")]
        [TestCase("abc", "bca", "bca")]
        public void InapplicableFindAnywhereRule(string find, string replace, string input)
        {
            var rule = new FindReplaceRule(new FindRule(find, FindRule.Location.Anywhere), new ReplaceRule(replace, ReplaceRule.Location.Inline));

            DoInapplicableTest(rule, input);
        }

        [TestCase("a", "b", "cbcbc")]
        [TestCase("a", "b", "")]
        [TestCase("a", "", "cbcbc")]
        [TestCase("abc", "bca", "bca")]
        [TestCase("abc", "bca", "ABabc")]
        public void InapplicableFindAtStartRule(string find, string replace, string input)
        {
            var rule = new FindReplaceRule(new FindRule(find, FindRule.Location.Start), new ReplaceRule(replace, ReplaceRule.Location.Inline));

            DoInapplicableTest(rule, input);
        }

        [TestCase("a", "b", "cbcbc")]
        [TestCase("a", "b", "")]
        [TestCase("a", "", "cbcbc")]
        [TestCase("abc", "bca", "bca")]
        [TestCase("abc", "bca", "abcAB")]
        public void InapplicableFindAtEndRule(string find, string replace, string input)
        {
            var rule = new FindReplaceRule(new FindRule(find, FindRule.Location.End), new ReplaceRule(replace, ReplaceRule.Location.Inline));

            DoInapplicableTest(rule, input);
        }
    }
}
