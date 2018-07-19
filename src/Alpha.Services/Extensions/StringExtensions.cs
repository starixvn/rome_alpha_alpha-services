using System.Text.RegularExpressions;

namespace Alpha.Services.Extensions
{
    public static class StringExtensions
    {
        private const string HtmlTagPattern = "<.*?>";

        public static string StripHTML(this string value)
            => Regex.Replace(value, HtmlTagPattern, string.Empty);
    }
}