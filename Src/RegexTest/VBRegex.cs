using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace RegexTest
{
    internal class VbRegex
    {
        public static string GetVbString(string regexText, RegexOptions regexOptions)
        {
#if fred
   Dim r As Regex
      Dim m As Match
    
      r = New Regex("href\s*=\s*(?:""(?<1>[^""]*)""|(?<1>\S+))", _
         RegexOptions.IgnoreCase Or RegexOptions.Compiled)
    
      m = r.Match(inputString)
      While m.Success
         Console.WriteLine("Found href " & m.Groups(1).Value _
            & " at " & m.Groups(1).Index.ToString())
         m = m.NextMatch()
      End While
   End Sub
#endif
            string s =
                "Dim r as Regex\r\n\r\n" +
                "r = new Regex( _\r\n";

            Regex splitter = new Regex("\r\n");
            string[] lines = splitter.Split(regexText);

            s += "\"";
            s += String.Join("\" + _ \r\n\"", lines);
            s += "\" _\r\n";

            ArrayList optionStrings = new ArrayList();

            foreach (RegexOptions option in Enum.GetValues(typeof (RegexOptions)))
            {
                if ((regexOptions & option) != 0)
                {
                    optionStrings.Add("RegexOptions." + Enum.GetName(typeof (RegexOptions), option));
                }
            }

            if (optionStrings.Count != 0)
            {
                s += ", ";
                s += String.Join(" Or ", (string[]) optionStrings.ToArray(typeof (string)));
            }

            s += ")";


            return s;
        }
    }
}