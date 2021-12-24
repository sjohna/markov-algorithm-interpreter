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
    class TestMarkovProgram
    {
        [TestCase("a","b")]
        [TestCase("aa", "bb")]
        [TestCase("aaaaaaaaaa", "bbbbbbbbbb")]
        [TestCase("b", "b")]
        [TestCase("abcba", "bbcbb")]
        [TestCase("Hello World!", "Hello World!")]
        public void ReplaceAWithB(string input, string expectedOutput)
        {
            var rule = new Rule("a", "b");
            var program = new MarkovProgram(new List<Rule>() { rule });

            var run = program.Run(input);

            Assert.AreEqual(expectedOutput, run.Output);
        }

        [TestCase("a", "a")]
        [TestCase("b", "b")]
        [TestCase("ab", "ab")]
        [TestCase("ba", "ab")]
        [TestCase("abbabababa", "aaaaabbbbb")]
        [TestCase("bbbbbbbbbbaaaaaaaaaa", "aaaaaaaaaabbbbbbbbbb")]
        public void SortAAndB(string input, string expectedOutput)
        {
            var rule = new Rule("ba", "ab");
            var program = new MarkovProgram(new List<Rule>() { rule });

            var run = program.Run(input);

            Assert.AreEqual(expectedOutput, run.Output);
        }

        [TestCase("a","a")]
        [TestCase("b", "b")]
        [TestCase("c", "c")]
        [TestCase("ab", "ab")]
        [TestCase("ac", "ac")]
        [TestCase("bc", "bc")]
        [TestCase("ba", "ab")]
        [TestCase("ca", "ac")]
        [TestCase("cb", "bc")]
        [TestCase("cba", "abc")]
        [TestCase("ccccbbbbaaaa", "aaaabbbbcccc")]
        [TestCase("abcabcabcabcabcabcabcabcabcabc", "aaaaaaaaaabbbbbbbbbbcccccccccc")]
        public void SortABC(string input, string expectedOutput)
        {
            var rules = new List<Rule>();
            rules.Add(new Rule("ba", "ab"));
            rules.Add(new Rule("ca", "ac"));
            rules.Add(new Rule("cb", "bc"));

            var program = new MarkovProgram(rules);

            var run = program.Run(input);

            Assert.AreEqual(expectedOutput, run.Output);
        }
    }
}
