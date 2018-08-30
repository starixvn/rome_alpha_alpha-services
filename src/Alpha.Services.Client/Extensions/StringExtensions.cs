namespace Alpha.Services.Client.Extensions
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        private const string HtmlTagPattern = "<.*?>";

        public static string StripHTML(this string value)
            => Regex.Replace(value, HtmlTagPattern, string.Empty);
    }
}