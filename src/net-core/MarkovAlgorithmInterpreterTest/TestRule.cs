using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkovAlgorithmInterpreter;

namespace MarkovAlgorithmInterpreterTest
{
    [TestFixture]
    class TestRule
    {
        [Test]
        public void ReplaceOneLetter()
        {
            var rule = new Rule("a", "b");

            var application = rule.Apply("a");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("b", application.Application);
        }

        [Test]
        public void ReplaceFirstLetter()
        {
            var rule = new Rule("a", "b");

            var application = rule.Apply("aaaaa");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("baaaa", application.Application);
        }

        [Test]
        public void ReplaceLetterInMiddle()
        {
            var rule = new Rule("a", "b");

            var application = rule.Apply("cccaccc");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("cccbccc", application.Application);
        }

        [Test]
        public void ReplaceLastLetter()
        {
            var rule = new Rule("a", "b");

            var application = rule.Apply("cccca");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("ccccb", application.Application);
        }

        [Test]
        public void FindNotFound()
        {
            var rule = new Rule("a", "b");

            var application = rule.Apply("cbcbcb");

            Assert.IsFalse(application.Applied);
        }

        [Test]
        public void ReplaceMultipleCharacters()
        {
            var rule = new Rule("abc", "bca");

            var application = rule.Apply("AabcB");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("AbcaB", application.Application);
        }

        [Test]
        public void ReplaceWholeMultiCharacterString()
        {
            var rule = new Rule("hello", "world");

            var application = rule.Apply("hello");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("world", application.Application);
        }

        [Test]
        public void ReplaceWithEmptyStringInMiddle()
        {
            var rule = new Rule("abc", "");

            var application = rule.Apply("AabcB");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("AB", application.Application);
        }

        [Test]
        public void ReplaceWithEmptyStringAtStart()
        {
            var rule = new Rule("abc", "");

            var application = rule.Apply("abcAB");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("AB", application.Application);
        }

        [Test]
        public void ReplaceWithEmptyStringAtEnd()
        {
            var rule = new Rule("abc", "");

            var application = rule.Apply("ABabc");

            Assert.IsTrue(application.Applied);
            Assert.AreEqual("AB", application.Application);
        }
    }
}
