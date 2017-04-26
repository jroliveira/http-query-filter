namespace Http.Query.Filter.Filters.Ordering
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infraestructure.Extensions;

    public class OrderBy : Collection<Field>
    {
        private const string Pattern = @"filter\[order](\[\d+])?\=(?<property>\w+)\s(?<direction>asc|desc)";

        public OrderBy(IEnumerable<Field> fields)
        {
            foreach (var field in fields)
            {
                this.Items.Add(field);
            }
        }

        protected OrderBy()
        {
        }

        public static implicit operator OrderBy(string query)
        {
            query = WebUtility.UrlDecode(query);

            var fields = Get(query);
            if (fields == null || !fields.Any())
            {
                return null;
            }

            return new OrderBy(fields);
        }

        private static IEnumerable<Field> Get(string query)
        {
            var matches = Regex.Matches(query, Pattern, RegexOptions.IgnoreCase);

            return
                from Match match in matches
                let property = match.Get("property")
                let orderBy = GetDirection(match)
                select new Field(property, orderBy);
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