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
    class TestWorkingString
    {
        [TestCase("abc")]
        [TestCase("a")]
        [TestCase("")]
        public void CreateSmallInput(string input)
        {
            var ws = new WorkingString(input);

            Assert.AreEqual(input.Length, ws.Length);
            Assert.AreEqual(input, ws.ToString());
        }

        [Test]
        public void CreateLargeInput()
        {
            var input = new String('a', 100) + new String('b', 100) + new string('c', 100);

            var ws = new WorkingString(input);

            Assert.AreEqual(input.Length, ws.Length);
            Assert.AreEqual(input, ws.ToString());
        }

        [TestCase("a", 0, "b", "b")]
        [TestCase("b", 0, "b", "b")]
        [TestCase("abc", 1, "c", "acc")]
        [TestCase("hello", 0, "world", "world")]
        [TestCase("abc123", 0, "123", "123123")]
        [TestCase("abc123", 3, "abc", "abcabc")]
        [TestCase("abc123abc", 3, "abc", "abcabcabc")]
        public void ReplaceInline(string input, int index, string replace, string expectedOutput)
        {
            var ws = new WorkingString(input);

            ws.ReplaceInline(index, replace);

            Assert.AreEqual(input.Length, ws.Length);
            Assert.AreEqual(expectedOutput, ws.ToString());
        }

        [TestCase("", "abc")]
        [TestCase("", "")]
        [TestCase("a", "b")]
        [TestCase("abc", "123")]
        public void Append(string input, string toAppend)
        {
            var ws = new WorkingString(input);
            ws.Append(toAppend);

            Assert.AreEqual(input.Length + toAppend.Length, ws.Length);
            Assert.AreEqual(input + toAppend, ws.ToString());
        }
        
        [Test]
        public void ManyAppends()
        {
            var ws = new WorkingString("");
            var expected = "";

            for (int i = 0; i < 1000; ++i)
            {
                ws.Append("abc");
                expected += "abc";
            }

            Assert.AreEqual(3000, ws.Length);
            Assert.AreEqual(expected, ws.ToString());
        }

        [TestCase("", "abc")]
        [TestCase("", "")]
        [TestCase("a", "b")]
        [TestCase("abc", "123")]
        public void Prepend(string input, string toPrepend)
        {
            var ws = new WorkingString(input);
            ws.Prepend(toPrepend);

            Assert.AreEqual(toPrepend.Length + input.Length, ws.Length);
            Assert.AreEqual(toPrepend + input, ws.ToString());
        }

        [Test]
        public void ManyPrepends()
        {
            var ws = new WorkingString("");
            var expected = "";

            for (int i = 0; i < 1000; ++i)
            {
                ws.Prepend("abc");
                expected += "abc";
            }

            Assert.AreEqual(3000, ws.Length);
            Assert.AreEqual(expected, ws.ToString());
        }

        [TestCase("abc", 1, "bc")]
        [TestCase("abc", 2, "c")]
        [TestCase("abc", 3, "")]
        [TestCase("a", 1, "")]
        [TestCase("abc123", 3, "123")]
        public void RemoveFirstN(string input, int n, string expected)
        {
            var ws = new WorkingString(input);
            ws.RemoveFirstN(n);

            Assert.AreEqual(input.Length - n, ws.Length);
            Assert.AreEqual(expected, ws.ToString());
        }

        [TestCase("abc", 1, "ab")]
        [TestCase("abc", 2, "a")]
        [TestCase("abc", 3, "")]
        [TestCase("a", 1, "")]
        [TestCase("abc123", 3, "abc")]
        public void RemoveLastN(string input, int n, string expected)
        {
            var ws = new WorkingString(input);
            ws.RemoveLastN(n);

            Assert.AreEqual(input.Length - n, ws.Length);
            Assert.AreEqual(expected, ws.ToString());
        }

        [Test]
        public void RollingAppend()
        {
            var ws = new WorkingString("abc");

            for (int i = 0; i < 1000; ++i)
            {
                ws.Append("abc");
                ws.RemoveFirstN(3);
            }

            Assert.AreEqual(3, ws.Length);
            Assert.AreEqual("abc", ws.ToString());
        }

        [Test]
        public void RollingPrepend()
        {
            var ws = new WorkingString("abc");

            for (int i = 0; i < 1000; ++i)
            {
                ws.Prepend("abc");
                ws.RemoveLastN(3);
            }

            Assert.AreEqual(3, ws.Length);
            Assert.AreEqual("abc", ws.ToString());
        }

        [TestCase("abc", 0, 1, "bc")]
        [TestCase("abc", 0, 2, "c")]
        [TestCase("abc", 0, 3, "")]
        [TestCase("abc", 1, 1, "ac")]
        [TestCase("abc", 1, 2, "a")]
        [TestCase("abc", 2, 1, "ab")]
        [TestCase("aa123bb123cc", 2, 3, "aabb123cc")]
        [TestCase("aa123bb123cc", 7, 3, "aa123bbcc")]
        [TestCase("aa123bb123cc", 5, 2, "aa123123cc")]
        public void RemoveChars(string input, int index, int numChars, string expectedOutput)
        {
            var ws = new WorkingString(input);

            ws.RemoveChars(index, numChars);

            Assert.AreEqual(input.Length - numChars, ws.Length);
            Assert.AreEqual(expectedOutput, ws.ToString());
        }

        [TestCase("abc", "a", 0)]
        [TestCase("abc", "b", 1)]
        [TestCase("abc", "c", 2)]
        [TestCase("abc", "d", -1)]
        [TestCase("abc", "ab", 0)]
        [TestCase("abc", "bc", 1)]
        [TestCase("abc", "abc", 0)]
        [TestCase("abc", "ac", -1)]
        [TestCase("abc123abc", "123", 3)]
        [TestCase("abc", "", 0)]
        [TestCase("abc", "abcd", -1)]
        public void IndexOf(string input, string toFind, int expectedResult)
        {
            var ws = new WorkingString(input);

            var result = ws.IndexOf(toFind);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("abc", "a", true)]
        [TestCase("abc", "b", false)]
        [TestCase("abc", "c", false)]
        [TestCase("abc", "d", false)]
        [TestCase("abc", "ab", true)]
        [TestCase("abc", "bc", false)]
        [TestCase("abc", "abc", true)]
        [TestCase("abc", "ac", false)]
        [TestCase("abc123abc", "123", false)]
        [TestCase("abc123abc", "abc", true)]
        [TestCase("abc", "", true)]
        [TestCase("abc", "abcd", false)]
        public void StartsWith(string input, string prefix, bool expectedResult)
        {
            var ws = new WorkingString(input);

            var result = ws.StartsWith(prefix);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("abc", "a", false)]
        [TestCase("abc", "b", false)]
        [TestCase("abc", "c", true)]
        [TestCase("abc", "d", false)]
        [TestCase("abc", "ab", false)]
        [TestCase("abc", "bc", true)]
        [TestCase("abc", "abc", true)]
        [TestCase("abc", "ac", false)]
        [TestCase("abc123abc", "123", false)]
        [TestCase("abc123abc", "abc", true)]
        [TestCase("abc", "", true)]
        [TestCase("abc", "abcd", false)]
        public void EndsWith(string input, string prefix, bool expectedResult)
        {
            var ws = new WorkingString(input);

            var result = ws.EndsWith(prefix);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("abc", 1, 1, "d", "adc")]
        [TestCase("abc", 1, 1, "b", "abc")]
        [TestCase("abc", 1, 1, "123", "a123c")]
        [TestCase("abc", 1, 1, "", "ac")]
        [TestCase("abc", 0, 2, "123", "123c")]
        [TestCase("abc", 3, 0, "d", "abcd")]
        [TestCase("abc", 1, 2, "d", "ad")]
        [TestCase("hello", 0, 5, "world", "world")]
        public void Replace(string input, int index, int length, string replace, string expected)
        {
            var ws = new WorkingString(input);

            ws.Replace(index, length, replace);

            Assert.AreEqual(expected, ws.ToString());
        }
    }
}
