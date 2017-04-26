namespace Http.Query.Filter.Filters
{
    using System.Net;
    using System.Text.RegularExpressions;

    public class Skip
    {
        private const string Pattern = @"filter\[skip]\=(?<skip>\d+)";

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
            query = WebUtility.UrlDecode(query);

            var match = Regex.Match(query, Pattern, RegexOptions.IgnoreCase);

            return int.TryParse(match.Groups["skip"].Value, out int skip)
                ? new Skip(skip)
                : null;
        }
    }
}