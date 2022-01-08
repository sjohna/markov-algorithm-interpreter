using System;
using System.Collections.Generic;
using System.Text;

namespace MarkovAlgorithmInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = 200;

            var shuffled = new StringBuilder();

            int aCount = 0;
            int bCount = 0;
            int cCount = 0;

            var random = new System.Random(12345);

            for (int i = 0; i < length; ++i)
            {
                var nextChar = (char)(random.Next(3) + 'a');
                if (nextChar == 'a') ++aCount;
                if (nextChar == 'b') ++bCount;
                if (nextChar == 'c') ++cCount;
                shuffled.Append(nextChar);
            }

            var expectedOutput = new String('a', aCount) + new String('b', bCount) + new string('c', cCount);

            var rules = new List<FindReplaceRule>();
            rules.Add(FindReplaceRule.Create("ba", "ab"));
            rules.Add(FindReplaceRule.Create("ca", "ac"));
            rules.Add(FindReplaceRule.Create("cb", "bc"));

            var program = new MarkovProgram(rules);

            var run = program.Run(shuffled.ToString());
        }
    }
}
