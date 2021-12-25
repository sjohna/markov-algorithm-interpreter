using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public class FindReplaceRule : Rule
    {
        public FindReplaceRule(string Find, string Replace)
        {
            this.Find = Find;
            this.Replace = Replace;
        }

        public virtual RuleApplication Apply(string input)
        {
            var findLocation = input.IndexOf(Find);

            if (findLocation == -1) return RuleApplication.NotApplicable();

            var newString = input.Substring(0, findLocation) + Replace + input.Substring(findLocation + FindLength);

            return RuleApplication.Applicable(newString);
        }
    }
}
