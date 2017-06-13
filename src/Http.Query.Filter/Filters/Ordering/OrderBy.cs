namespace Http.Query.Filter.Filters.Ordering
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infraestructure.Extensions;

    public class OrderBy : ReadOnlyCollection<KeyValuePair<string, OrderByDirection>>
    {
        private const string Pattern = @"filter\[order](\[\d+])?\=(?<field>\w+)\s(?<direction>asc|desc)";
        private static Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static Dictionary<string, OrderByDirection> types = new Dictionary<string, OrderByDirection>
            {
                { "asc", OrderByDirection.Ascending },
                { "desc", OrderByDirection.Descending }
            };

        public OrderBy(IList<KeyValuePair<string, OrderByDirection>> fields)
            : base(fields)
        {
        }

        public static implicit operator OrderBy(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            var fields = Get(WebUtility.UrlDecode(query));

            if (!fields.Any())
            {
                return null;
            }

            return new OrderBy(fields);
        }

        private static IList<KeyValuePair<string, OrderByDirection>> Get(string query)
        {
            var matches = regex.Matches(query);

            return
                (from Match match in matches
                 let field = match.Get("field")
                 let orderBy = GetDirection(match)
                 select new KeyValuePair<string, OrderByDirection>(field, orderBy)).ToList();
        }

        private static OrderByDirection GetDirection(Match match)
        {
            var direction = match.Groups["direction"].Value.ToLower();

            return types[direction];
        }
    }
}