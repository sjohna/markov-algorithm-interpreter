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

        public virtual RuleApplication<String> Apply(string input)
        {
            var findLocation = Find.Find(input);

            if (findLocation == -1) return RuleApplication<String>.NotApplicable();

            var newString = Replace.Replace(input.Substring(0, findLocation), input.Substring(findLocation + Find.FindLength));

            return RuleApplication<String>.Applicable(newString);
        }

        public virtual RuleApplication<StringBuilder> Apply(StringBuilder input)
        {
            var findLocation = Find.Find(input);

            if (findLocation == -1) return RuleApplication<StringBuilder>.NotApplicable();

            if (Replace.ReplaceLocation == ReplaceRule.Location.Inline)
            {
                if (Find.ToFind.Length > 0)
                {
                    input.Replace(Find.ToFind, Replace.ReplaceString, findLocation, Find.ToFind.Length);
                }
                else
                {
                    input.Insert(findLocation, Replace.ReplaceString);
                }
            }
            else if(Replace.ReplaceLocation == ReplaceRule.Location.Start)
            {
                input.Remove(findLocation, Find.ToFind.Length);

                input.Insert(0, Replace.ReplaceString);
            }
            else
            {
                input.Remove(findLocation, Find.ToFind.Length);

                input.Append(Replace.ReplaceString);
            }

            return RuleApplication<StringBuilder>.Applicable(input);
        }

        public virtual RuleApplication<WorkingString> Apply(WorkingString input)
        {
            var findLocation = Find.Find(input);

            if (findLocation == -1) return RuleApplication<WorkingString>.NotApplicable();

            if (Replace.ReplaceLocation == ReplaceRule.Location.Inline)
            {
                input.Replace(findLocation, Find.ToFind.Length, Replace.ReplaceString);
            }
            else if (Replace.ReplaceLocation == ReplaceRule.Location.Start)
            {
                input.RemoveChars(findLocation, Find.ToFind.Length);

                input.Prepend(Replace.ReplaceString);
            }
            else
            {
                input.RemoveChars(findLocation, Find.ToFind.Length);

                input.Append(Replace.ReplaceString);
            }

            return RuleApplication<WorkingString>.Applicable(input);
        }
    }
}
