using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public abstract class Rule
    {
        public string Find { get; protected set; }

        public int FindLength => Find.Length;

        public string Replace { get; protected set; }

        public int ReplaceLength => Replace.Length;

        public abstract RuleApplication Apply(string input);
    }
}
