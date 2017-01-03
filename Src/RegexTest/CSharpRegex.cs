using System;
using System.Collections;
using System.Text.RegularExpressions;

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
    }
}