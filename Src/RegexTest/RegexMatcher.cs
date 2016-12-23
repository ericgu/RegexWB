using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTest
{
    internal class RegexMatcher : IRegexOperation
    {
        private readonly int _iterations;
        private readonly bool _hideGroupZero;

        public RegexMatcher(int iterations, bool hideGroupZero)
        {
            _hideGroupZero = hideGroupZero;
            _iterations = iterations;
        }

        public string Execute(Regex regex, string[] strings)
        {
            string[] groupNames = regex.GetGroupNames();

            StringBuilder outString = new StringBuilder();
            foreach (string s in strings)
            {
                outString.Append(String.Format("Matching: {0}\r\n", s));

                Match m;
                for (int i = 0; i < _iterations; i++)
                {
                    m = regex.Match(s);
                    while (m.Success)
                    {
                        m = m.NextMatch();
                    }
                }

                m = regex.Match(s);
                bool noMatch = true;
                while (m.Success)
                {
                    noMatch = false;
                    int groupNumber = 0;
                    foreach (Group group in m.Groups)
                    {
                        foreach (Capture capture in @group.Captures)
                        {
                            if (!_hideGroupZero ||
                                groupNames[groupNumber] != "0")
                            {
                                outString.Append(String.Format("    {0} => {1}\r\n", groupNames[groupNumber], capture));
                            }
                        }
                        groupNumber++;
                    }

                    m = m.NextMatch();
                }
                if (noMatch)
                {
                    outString.Append("    No Match\r\n");
                }
            }

            return outString.ToString();
        }
    }
}