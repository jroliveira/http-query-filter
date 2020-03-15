namespace Http.Query.Filter.Filters.Ordering
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infrastructure.Extensions;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;
    using static Http.Query.Filter.Filters.Ordering.OrderByDirection;

    public sealed class OrderBy : ReadOnlyCollection<KeyValuePair<string, OrderByDirection>>
    {
        private const string Pattern = @"filter\[order](\[\d+])?\=(?<field>\w+)\s(?<direction>asc|desc)";

        private static readonly Func<string, MatchCollection> Matches = new Regex(Pattern, IgnoreCase | Compiled).Matches;
        private static readonly IReadOnlyDictionary<string, OrderByDirection> Types = new Dictionary<string, OrderByDirection>
        {
            { "asc", Ascending },
            { "desc", Descending },
        };

        internal OrderBy(IEnumerable<KeyValuePair<string, OrderByDirection>> fields)
            : base(fields.ToList())
        {
        }

        public static implicit operator OrderBy(string query) => new OrderBy(GetFields(query));

        private static IEnumerable<KeyValuePair<string, OrderByDirection>> GetFields(string query) => IsNullOrEmpty(query)
            ? new List<KeyValuePair<string, OrderByDirection>>()
            : new List<KeyValuePair<string, OrderByDirection>>(
                from Match match in Matches(UrlDecode(query))
                let field = match.GetValue("field")
                let orderBy = GetDirection(match)
                select new KeyValuePair<string, OrderByDirection>(field, orderBy));

        private static OrderByDirection GetDirection(Match match) => Types[match.GetValue("direction").ToLower()];
    }
}
