using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public class Rule
    {
        public string Find { get; protected set; }

        public int FindLength => Find.Length;

        public string Replace { get; protected set; }

        public int ReplaceLength => Replace.Length;

        public Rule(string Find, string Replace)
        {
            this.Find = Find;
            this.Replace = Replace;
        }

        public virtual RuleApplication Apply(string input)
        {
            if (!input.Contains(Find)) return RuleApplication.NotApplicable();

            var findLocation = input.IndexOf(Find);

            var newString = input.Substring(0, findLocation) + Replace + input.Substring(findLocation + FindLength);

            return RuleApplication.Applicable(newString);
        }
    }
}
