using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public class MarkovProgram
    {
        public IReadOnlyList<FindReplaceRule> Rules { get; private set; }

        public MarkovProgram(IReadOnlyList<FindReplaceRule> Rules)
        {
            this.Rules = Rules.ToList();
        }

        public ProgramRun Run(string input)
        {
            var current = new WorkingString(input);

            while (true)
            {
                bool ruleFound = false;

                foreach (var rule in Rules)
                {
                    var application = rule.Apply(current);

                    if (application.Applied)
                    {
                        ruleFound = true;
                        current = application.Application;
                        break;
                    }
                }

                if (!ruleFound) break;
            }

            return new ProgramRun(current.ToString());
        }
    }
}
