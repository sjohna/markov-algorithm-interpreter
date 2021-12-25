using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public class FindReplaceRule
    {
        /**
         * Creates a find-anywhere, replace-inline rule.
         */
        public static FindReplaceRule Create(string find, string replace)
        {
            return new FindReplaceRule(
                FindRule.Anywhere(find),
                ReplaceRule.Inline(replace));
        }

        public FindRule Find { get; protected set; }

        public ReplaceRule Replace { get; protected set; }

        public FindReplaceRule(FindRule Find, ReplaceRule Replace)
        {
            this.Find = Find;
            this.Replace = Replace;
        }

        public virtual RuleApplication Apply(string input)
        {
            var findLocation = Find.Find(input);

            if (findLocation == -1) return RuleApplication.NotApplicable();

            var newString = Replace.Replace(input.Substring(0, findLocation), input.Substring(findLocation + Find.FindLength));

            return RuleApplication.Applicable(newString);
        }
    }
}
