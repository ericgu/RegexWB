using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTest
{
    internal class RegexSplitter: IRegexOperation
    {
        public string Execute(Regex regex, string[] strings)
        {
            StringBuilder outString = new StringBuilder();
            foreach (string s in strings)
            {
                outString.Append(String.Format("Splitting: {0}\r\n", s));

                string[] arr = regex.Split(s);

                int index = 0;
                foreach (string split in arr)
                {
                    outString.Append(String.Format("    [{0}] => {1}\r\n", index, split));
                    index++;
                }
            }

            return outString.ToString();
        }
    }
}