using System;
using System.Text;
using System.Text.RegularExpressions;
using RegexTest;

static internal class RegexEvaluator
{
    public static string Execute(Regex regex, string[] strings, int iterations, bool hideGroupZero)
    {
        string[] groupNames = regex.GetGroupNames();

        StringBuilder outString = new StringBuilder();
        foreach (string s in strings)
        {
            outString.Append(String.Format("Matching: {0}\r\n", s));

            Match m = null;
            for (int i = 0; i < iterations; i++)
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
                        if (!hideGroupZero ||
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

    public static string Split(string[] strings, Regex regex)
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

    public static string DoReplace(Regex regex, string[] strings, bool useMatchEvaluator, string replaceText)
    {
        ReplaceMatchEvaluator replacer = null;
        StringBuilder outString = new StringBuilder();

        if (useMatchEvaluator)
        {
            replacer = new ReplaceMatchEvaluator(regex, replaceText);
            string output = replacer.CreateAndLoadClass();
            if (output != null)
            {
                outString.Append(output);
            }
        }

        string replace = replaceText;
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