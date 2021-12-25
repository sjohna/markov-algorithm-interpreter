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
            string currentString = input;

            while (true)
            {
                bool ruleFound = false;

                foreach (var rule in Rules)
                {
                    var application = rule.Apply(currentString);

                    if (application.Applied)
                    {
                        ruleFound = true;
                        currentString = application.Application;
                        break;
                    }
                }

                if (!ruleFound) break;
            }

            return new ProgramRun(currentString);
        }
    }
}
