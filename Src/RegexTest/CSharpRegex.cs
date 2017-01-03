using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegexTest
{
    internal static class CSharpRegex
    {
        public static string MakeCSharpString(string regexText, RegexOptions createRegexOptions)
        {
            string s =
                "Regex regex = new Regex(@\"\r\n" +
                regexText.Replace("\"", "\"\"") +
                "\", \r\n";

            ArrayList optionStrings = new ArrayList();
            RegexOptions options = createRegexOptions;

            foreach (RegexOptions option in Enum.GetValues(typeof(RegexOptions)))
            {
                if ((options & option) != 0)
                {
                    optionStrings.Add("RegexOptions." + Enum.GetName(typeof(RegexOptions), option));
                }
            }	
		
            if (optionStrings.Count != 0)
            {
                s += String.Join(" | \r\n", (string[]) optionStrings.ToArray(typeof(string)));
            }
            else
            {
                s += " (RegexOptions) 0";
            }
            s += ");";

            s = s.Replace("\n", "\n\t\t\t\t");
            return s;
#if fred
Regex r = new Regex(
				@"(?<Field>         # capture to field
				[^,""]+              # one or more not , or ""
				|                 # or
				""[^""]+              # "" and one or more not , or ""
				""),               # end capture", 
				RegexOptions.IgnorePatternWhitespace);
			
			#endif
        }

        public static void ParseCSharpToSettings(Settings settings)
        {
            IDataObject clipboard = Clipboard.GetDataObject();
            string value = (string) clipboard.GetData(typeof (string));

            // first, get rid of the "Regex regex line, if it exists"
            Regex regex2 = new Regex(@"
				^.+?new\ Regex\(@""(?<Rest>.+)
				",
                RegexOptions.Multiline |
                RegexOptions.ExplicitCapture |
                RegexOptions.Singleline |
                RegexOptions.IgnorePatternWhitespace);

            Match m = regex2.Match(value);
            if (m.Success)
            {
                value = m.Groups["Rest"].Value;
            }

            // get rid of the leading whitespace on each line...
            Regex regex = new Regex(@"
				^\s+
				",
                RegexOptions.Multiline |
                RegexOptions.ExplicitCapture |
                RegexOptions.IgnorePatternWhitespace);

            value = regex.Replace(value, "");

            // see if there is a " and options after the string...
            Regex regex3 = new Regex(@"
				(?<Pattern>.+)^\s*"",(?<Rest>.+)
				",
                RegexOptions.Multiline |
                RegexOptions.ExplicitCapture |
                RegexOptions.Singleline |
                RegexOptions.IgnorePatternWhitespace);

            m = regex3.Match(value);

            settings.IgnoreCase = false;
            settings.IgnoreWhitespace = false;
            settings.Multiline = false;
            settings.Singleline = false;
            settings.Compiled = false;
            settings.ExplicitCapture = false;

            if (m.Success)
            {
                value = m.Groups["Pattern"].Value;

                string rest = m.Groups["Rest"].Value;

                settings.IgnoreCase = rest.IndexOf("IgnoreCase") != -1;
                settings.IgnoreWhitespace = rest.IndexOf("IgnorePatternWhitespace") != -1;
                settings.Multiline = rest.IndexOf("Multiline") != -1;
                settings.Singleline = rest.IndexOf("Singleline") != -1;
                settings.Compiled = rest.IndexOf("Compiled") != -1;
                settings.ExplicitCapture = rest.IndexOf("ExplicitCapture") != -1;
            }

            // change any double "" to "
            string regexText = value.Replace("\"\"", "\"");
            settings.RegexText = regexText;
        }
    }
}