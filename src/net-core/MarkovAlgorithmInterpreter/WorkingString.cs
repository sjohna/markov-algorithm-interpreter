using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovAlgorithmInterpreter
{
    public class WorkingString
    {
        private int startIndex;
        private int endIndex;

        private char[] workingData;

        public WorkingString(string initial)
        {
            int initialLength = Math.Max(initial.Length * 3, 128);
            workingData = new char[initialLength];

            startIndex = (workingData.Length / 2) - (initial.Length / 2);
            endIndex = (workingData.Length / 2) + ((initial.Length + 1) / 2);

            for (int index = 0; index < initial.Length; ++index)
            {
                workingData[startIndex + index] = initial[index];
            }
        }

        public void ReplaceInline(int index, string replace)
        {
            for (int i = 0; i < replace.Length; ++i)
            {
                workingData[startIndex + index + i] = replace[i];
            }
        }

        public void Replace(int index, int length, string replace)
        {
            int newEndIndex = endIndex + replace.Length - length;
            if (newEndIndex > workingData.Length)
            {
                ReallocateAndRecenter(newEndIndex - startIndex);
            }

            if (length < replace.Length)
            {
                // TODO: check if it's more efficient to shift down
                Shift(startIndex + index + length, endIndex - startIndex - index - length, startIndex + index + replace.Length);
                endIndex = newEndIndex;
            }
            else if (length > replace.Length)
            {
                Shift(startIndex + index + length, endIndex - startIndex - index - length, startIndex + index + replace.Length);
                endIndex = newEndIndex;
            }

            Array.Copy(replace.ToCharArray(), 0, workingData, startIndex + index, replace.Length);
        }

        public void Append(string toAppend)
        {
            if (endIndex + toAppend.Length >= workingData.Length)
            {
                ReallocateAndRecenter(Length + toAppend.Length);
            }

            for (int i = 0; i < toAppend.Length; ++i)
            {
                workingData[endIndex + i] = toAppend[i];
            }

            endIndex += toAppend.Length;
        }

        public void Prepend(string toPrepend)
        {
            if (startIndex - toPrepend.Length < 0)
            {
                ReallocateAndRecenter(Length + toPrepend.Length);
            }

            for (int i = 0; i < toPrepend.Length; ++i)
            {
                workingData[startIndex - toPrepend.Length + i] = toPrepend[i];
            }

            startIndex -= toPrepend.Length;
        }

        public void RemoveChars(int index, int numChars)
        {
            int charsToShiftUp = index;
            int charsToShiftDown = Length - numChars - index;

            if (charsToShiftUp <= charsToShiftDown)
            {
                Shift(startIndex, charsToShiftUp, startIndex + numChars);
                startIndex += numChars;
            }
            else
            {
                Shift(startIndex + index + numChars, charsToShiftDown, startIndex + index);
                endIndex -= numChars;
            }
        }

        public int IndexOf(string substring)
        {
            if (substring.Length > Length) return -1;

            int findIndexMax = Length - substring.Length;

            for (int findIndex = 0; findIndex <= findIndexMax; ++findIndex)
            {
                bool match = true;

                for (int compareIndex = 0; compareIndex < substring.Length; ++compareIndex)
                {
                    if (workingData[startIndex + findIndex + compareIndex] != substring[compareIndex])
                    {
                        match = false;
                        break;
                    }
                }

                if (match) return findIndex;
            }

            return -1;
        }

        public bool StartsWith(string substring)
        {
            if (substring.Length > Length) return false;

            int findIndex = 0;

            for (int compareIndex = 0; compareIndex < substring.Length; ++compareIndex)
            {
                if (workingData[startIndex + findIndex + compareIndex] != substring[compareIndex])
                {
                    return false;
                }
            }

            return true;
        }

        public bool EndsWith(string substring)
        {
            if (substring.Length > Length) return false;

            int findIndex = Length - substring.Length;

            for (int compareIndex = 0; compareIndex < substring.Length; ++compareIndex)
            {
                if (workingData[startIndex + findIndex + compareIndex] != substring[compareIndex])
                {
                    return false;
                }
            }

            return true;
        }

        private void Shift(int index, int length, int shiftToIndex)
        {
            Array.Copy(workingData, index, workingData, shiftToIndex, length);
        }

        private void ReallocateAndRecenter(int neededLength)
        {
            if (neededLength * 3 > workingData.Length)
            {
                var newLength = Math.Max(neededLength * 3, workingData.Length) * 3;

                // allocate new buffer and copy in to
                var newWorkingData = new char[newLength];

                int newStartIndex = (newWorkingData.Length / 2) - (Length / 2);
                int newEndIndex = (newWorkingData.Length / 2) + ((Length + 1) / 2);

                Array.Copy(workingData, startIndex, newWorkingData, newStartIndex, Length);

                workingData = newWorkingData;
                startIndex = newStartIndex;
                endIndex = newEndIndex;
            }
            else
            {
                // compact without reallocating
                int midPoint = workingData.Length / 2;

                int newStartIndex = (midPoint) - (Length / 2);
                int newEndIndex = (midPoint) + ((Length + 1) / 2);

                Array.Copy(workingData, startIndex, workingData, newStartIndex, Length);

                Shift(startIndex, Length, newStartIndex);

                startIndex = newStartIndex;
                endIndex = newEndIndex;
            }
        }

        public void RemoveLastN(int numChars)
        {
            endIndex -= numChars;
        }

        public void RemoveFirstN(int numChars)
        {
            startIndex += numChars;
        }

        public override String ToString()
        {
            return new String(workingData, startIndex, Length);
        }

        public int Length => endIndex - startIndex;
    }
}
