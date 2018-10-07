namespace Http.Query.Filter.Filters.Ordering
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Http.Query.Filter.Infrastructure.Extensions;
    using static Http.Query.Filter.Filters.Ordering.OrderByDirection;
    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;

    public sealed class OrderBy : ReadOnlyCollection<KeyValuePair<string, OrderByDirection>>
    {
        private const string Pattern = @"filter\[order](\[\d+])?\=(?<field>\w+)\s(?<direction>asc|desc)";
        private static readonly Regex Regex = new Regex(Pattern, IgnoreCase | Compiled);
        private static readonly Dictionary<string, OrderByDirection> Types = new Dictionary<string, OrderByDirection>
        {
            { "asc", Ascending },
            { "desc", Descending },
        };

        public OrderBy(IList<KeyValuePair<string, OrderByDirection>> fields)
            : base(fields)
        {
        }

        public static implicit operator OrderBy(string query)
        {
            if (IsNullOrEmpty(query))
            {
                return null;
            }

            var fields = Get(UrlDecode(query));

            if (!fields.Any())
            {
                return null;
            }

            return new OrderBy(fields);
        }

        private static IList<KeyValuePair<string, OrderByDirection>> Get(string query)
        {
            var matches = Regex.Matches(query);

            return
                (from Match match in matches
                 let field = match.Get("field")
                 let orderBy = GetDirection(match)
                 select new KeyValuePair<string, OrderByDirection>(field, orderBy)).ToList();
        }

        private static OrderByDirection GetDirection(Match match)
        {
            var direction = match.Groups["direction"].Value.ToLower();

            return Types[direction];
        }
    }
}
