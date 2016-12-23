using System.Text.RegularExpressions;

namespace RegexTest
{
    internal interface IRegexOperation
    {
        string Execute(Regex regex, string[] strings);
    }
}