using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public class FindRule
    {
        public static FindRule Start(string ToFind) => new FindRule(ToFind, Location.Start);
        public static FindRule End(string ToFind) => new FindRule(ToFind, Location.End);
        public static FindRule Anywhere(string ToFind) => new FindRule(ToFind, Location.Anywhere);

        public enum Location
        {
            Start,
            End,
            Anywhere
        }

        public string ToFind { get; private set; }

        public int FindLength => ToFind.Length;

        public Location FindLocation { get; private set; }

        public FindRule(string ToFind, Location FindLocation)
        {
            this.ToFind = ToFind;
            this.FindLocation = FindLocation;
        }

        public int Find(string input)
        {
            if (FindLocation == Location.Anywhere)
            {
                return input.IndexOf(ToFind);
            }

            if (FindLocation == Location.Start && input.StartsWith(ToFind))
            {
                return 0;
            }

            if (FindLocation == Location.End && input.EndsWith(ToFind))
            {
                return input.Length - ToFind.Length;
            }

            return -1;
        }

        public int Find(StringBuilder input)
        {
            if (FindLocation == Location.Anywhere)
            {
                for (int index = 0; index <= (input.Length - ToFind.Length); ++index)
                {
                    bool found = true;
                    for (int compIndex = 0; compIndex < ToFind.Length; ++compIndex)
                    {
                        if (ToFind[compIndex] != input[index + compIndex])
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found) return index;
                }

                return -1;
            }

            if (input.Length < ToFind.Length) return -1;

            if (FindLocation == Location.Start)
            {
                int index = 0;

                bool found = true;
                for (int compIndex = 0; compIndex < ToFind.Length; ++compIndex)
                {
                    if (ToFind[compIndex] != input[index + compIndex])
                    {
                        found = false;
                        break;
                    }
                }

                if (found) return index;

                return -1;
            }

            if (FindLocation == Location.End)
            {
                int index = input.Length - ToFind.Length;

                bool found = true;
                for (int compIndex = 0; compIndex < ToFind.Length; ++compIndex)
                {
                    if (ToFind[compIndex] != input[index + compIndex])
                    {
                        found = false;
                        break;
                    }
                }

                if (found) return index;

                return -1;
            }

            return -1;
        }

        public int Find(WorkingString input)
        {
            if (FindLocation == Location.Anywhere)
            {
                return input.IndexOf(ToFind);
            }

            if (FindLocation == Location.Start && input.StartsWith(ToFind))
            {
                return 0;
            }

            if (FindLocation == Location.End && input.EndsWith(ToFind))
            {
                return input.Length - ToFind.Length;
            }

            return -1;
        }
    }
}
