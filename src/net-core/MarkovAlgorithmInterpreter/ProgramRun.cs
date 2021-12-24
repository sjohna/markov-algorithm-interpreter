using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public class ProgramRun
    {
        public string Output { get; private set; }

        public ProgramRun(string Output)
        {
            this.Output = Output;
        }
    }
}
