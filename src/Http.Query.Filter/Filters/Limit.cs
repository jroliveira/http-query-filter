namespace Http.Query.Filter.Filters
{
    using System.Net;
    using System.Text.RegularExpressions;

    public class Limit
    {
        private const string Pattern = @"filter\[limit]\=(?<limit>\d+)";
        private static Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public Limit(int value)
        {
            this.Value = value;
        }

        protected Limit()
        {
        }

        public int Value { get; protected set; }

        public static implicit operator int(Limit limit)
        {
            return limit.Value;
        }

        public static implicit operator Limit(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            var match = regex.Match(WebUtility.UrlDecode(query));

            if (match.Success && int.TryParse(match.Groups["limit"].Value, out int limit))
            {
                return new Limit(limit);
            }

            return null;
        }
    }
}