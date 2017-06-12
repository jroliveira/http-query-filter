namespace Http.Query.Filter.Filters
{
    using System.Net;
    using System.Text.RegularExpressions;

    public class Skip
    {
        private const string Pattern = @"filter\[skip]\=(?<skip>\d+)";
        private static Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public Skip(int value)
        {
            this.Value = value;
        }

        protected Skip()
        {
        }

        public int Value { get; protected set; }

        public static implicit operator int(Skip skip)
        {
            return skip.Value;
        }

        public static implicit operator Skip(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            var match = regex.Match(WebUtility.UrlDecode(query));

            if (match.Success && int.TryParse(match.Groups["skip"].Value, out int skip))
            {
                return new Skip(skip);
            }

            return null;
        }
    }
}