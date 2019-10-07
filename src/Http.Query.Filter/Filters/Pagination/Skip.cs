namespace Http.Query.Filter.Filters.Pagination
{
    using System;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infrastructure.Extensions;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;
    using static System.UInt32;

    public readonly struct Skip : IPagination
    {
        private const string Pattern = @"filter\[skip]\=(?<skip>\d+)";

        private static readonly Func<string, Match> Match = new Regex(Pattern, IgnoreCase | Compiled).Match;

        private Skip(uint? value) => this.Value = value;

        public uint? Value { get; }

        public static implicit operator uint?(Skip skip) => skip.Value;

        public static implicit operator Skip(string query)
        {
            if (IsNullOrWhiteSpace(query))
            {
                return default;
            }

            var match = Match(UrlDecode(query));

            return match.Success && TryParse(match.GetValue("skip"), out var skip)
                ? new Skip(skip)
                : default;
        }
    }
}
