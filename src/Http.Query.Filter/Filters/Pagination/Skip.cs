namespace Http.Query.Filter.Filters.Pagination
{
    using System.Text.RegularExpressions;
    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;
    using static System.UInt32;

    public readonly struct Skip
    {
        private const string Pattern = @"filter\[skip]\=(?<skip>\d+)";
        private static readonly Regex Regex = new Regex(Pattern, IgnoreCase | Compiled);

        public Skip(uint? value) => this.Value = value;

        public uint? Value { get; }

        public static implicit operator uint?(Skip skip) => skip.Value;

        public static implicit operator Skip(string query)
        {
            if (IsNullOrWhiteSpace(query))
            {
                return default;
            }

            var match = Regex.Match(UrlDecode(query));

            if (match.Success && TryParse(match.Groups["skip"].Value, out var skip))
            {
                return new Skip(skip);
            }

            return default;
        }
    }
}
