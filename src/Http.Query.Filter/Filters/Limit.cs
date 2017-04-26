namespace Http.Query.Filter.Filters
{
    using System.Net;
    using System.Text.RegularExpressions;

    public class Limit
    {
        private const string Pattern = @"filter\[limit]\=(?<limit>\d+)";

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
            query = WebUtility.UrlDecode(query);

            var match = Regex.Match(query, Pattern, RegexOptions.IgnoreCase);

            return int.TryParse(match.Groups["limit"].Value, out int limit)
                ? new Limit(limit)
                : null;
        }
    }
}