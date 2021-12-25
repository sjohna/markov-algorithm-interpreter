using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public struct RuleApplication
    {
        public static RuleApplication NotApplicable()
        {
            return new RuleApplication()
            {
                Applied = false
            };
        }

        public static RuleApplication Applicable(string newString)
        {
            return new RuleApplication()
            {
                Applied = true,
                Application = newString
            };
        }

        public bool Applied { get; private set; }
        public string Application { get; private set; }


    }
}
