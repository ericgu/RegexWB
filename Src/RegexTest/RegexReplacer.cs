using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTest
{
    internal class RegexReplacer: IRegexOperation
    {
        private readonly bool _useMatchEvaluator;
        private readonly string _replaceText;

        public RegexReplacer(bool useMatchEvaluator, string replaceText)
        {
            _replaceText = replaceText;
            _useMatchEvaluator = useMatchEvaluator;
        }

        public string Execute(Regex regex, string[] strings)
        {
            ReplaceMatchEvaluator replacer = null;
            StringBuilder outString = new StringBuilder();

            if (_useMatchEvaluator)
            {
                replacer = new ReplaceMatchEvaluator(regex, _replaceText);
                string output = replacer.CreateAndLoadClass();
                if (output != null)
                {
                    outString.Append(output);
                }
            }

            string replace = _replaceText;
            foreach (string s in strings)
            {
                outString.Append(String.Format("Replacing: {0}\r\n", s));

                string output;
                if (replacer != null)
                {
                    outString.Append("  with a custom MatchEvaluator\r\n");
                    output = regex.Replace(s, replacer.MatchEvaluator);
                }
                else
                {
                    outString.Append(String.Format("  with: {0}\r\n", replace));
                    output = regex.Replace(s, replace);
                }
                outString.Append(String.Format("  result: {0}\r\n", output));
            }

            return outString.ToString();
        }
    }
}