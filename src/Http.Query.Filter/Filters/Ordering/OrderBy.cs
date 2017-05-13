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

        public OrderBy(IList<KeyValuePair<string, OrderByDirection>> fields)
            : base(fields)
        {
        }

        public static implicit operator OrderBy(string query)
        {
            query = WebUtility.UrlDecode(query);

            var data = Get(query);
            var fields = data as IList<KeyValuePair<string, OrderByDirection>> ?? data.ToList();

            if (!fields.Any())
            {
                return null;
            }

            return new OrderBy(fields);
        }

        private static IEnumerable<KeyValuePair<string, OrderByDirection>> Get(string query)
        {
            var matches = Regex.Matches(query, Pattern, RegexOptions.IgnoreCase);

            return
                from Match match in matches
                let field = match.Get("field")
                let orderBy = GetDirection(match)
                select new KeyValuePair<string, OrderByDirection>(field, orderBy);
        }

        private static OrderByDirection GetDirection(Match match)
        {
            var direction = match.Groups["direction"].Value.ToLower();

            var types = new Dictionary<string, OrderByDirection>
            {
                { "asc", OrderByDirection.Ascending },
                { "desc", OrderByDirection.Descending }
            };

            return types[direction];
        }
    }
}