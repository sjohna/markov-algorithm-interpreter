using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public struct RuleApplication<T>
    {
        public static RuleApplication<T> NotApplicable()
        {
            return new RuleApplication<T>()
            {
                Applied = false
            };
        }

        public static RuleApplication<T> Applicable(T newString)
        {
            return new RuleApplication<T>()
            {
                Applied = true,
                Application = newString
            };
        }

        public bool Applied { get; private set; }
        public T Application { get; private set; }


    }
}
