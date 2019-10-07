namespace Http.Query.Filter.Filters.Pagination
{
    using System;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infrastructure.Extensions;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;
    using static System.UInt32;

    public readonly struct Limit : IPagination
    {
        private const string Pattern = @"filter\[limit]\=(?<limit>\d+)";

        private static readonly Func<string, Match> Match = new Regex(Pattern, IgnoreCase | Compiled).Match;

        private Limit(uint? value) => this.Value = value;

        public uint? Value { get; }

        public static implicit operator uint?(Limit limit) => limit.Value;

        public static implicit operator Limit(string query)
        {
            if (IsNullOrWhiteSpace(query))
            {
                return default;
            }

            var match = Match(UrlDecode(query));

            return match.Success && TryParse(match.GetValue("limit"), out var limit)
                ? new Limit(limit)
                : default;
        }
    }
}
