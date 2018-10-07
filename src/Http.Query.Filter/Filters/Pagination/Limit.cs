namespace Http.Query.Filter.Filters.Pagination
{
    using System.Text.RegularExpressions;
    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;
    using static System.UInt32;

    public readonly struct Limit
    {
        private const string Pattern = @"filter\[limit]\=(?<limit>\d+)";
        private static readonly Regex Regex = new Regex(Pattern, IgnoreCase | Compiled);

        public Limit(uint? value) => this.Value = value;

        public uint? Value { get; }

        public static implicit operator uint?(Limit limit) => limit.Value;

        public static implicit operator Limit(string query)
        {
            if (IsNullOrWhiteSpace(query))
            {
                return default;
            }

            var match = Regex.Match(UrlDecode(query));

            if (match.Success && TryParse(match.Groups["limit"].Value, out var limit))
            {
                return new Limit(limit);
            }

            return default;
        }
    }
}
