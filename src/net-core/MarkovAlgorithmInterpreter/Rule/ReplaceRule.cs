using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public class ReplaceRule
    {
        public static ReplaceRule Start(string ToFind) => new ReplaceRule(ToFind, Location.Start);
        public static ReplaceRule End(string ToFind) => new ReplaceRule(ToFind, Location.End);
        public static ReplaceRule Inline(string ToFind) => new ReplaceRule(ToFind, Location.Inline);

        public enum Location
        {
            Start,
            End,
            Inline
        }

        public string ReplaceString { get; private set; }

        public int ReplaceLength => ReplaceString.Length;

        public Location ReplaceLocation { get; private set; }

        public ReplaceRule(string Replace, Location ReplaceLocation)
        {
            this.ReplaceString = Replace;
            this.ReplaceLocation = ReplaceLocation;
        }

        public string Replace(string before, string after)
        {
            if (ReplaceLocation == Location.Start)
            {
                return ReplaceString + before + after;
            }

            if (ReplaceLocation == Location.Inline)
            {
                return before + ReplaceString + after;
            }

            if (ReplaceLocation == Location.End)
            {
                return before + after + ReplaceString;
            }

            throw new Exception();
        }
    }
}
