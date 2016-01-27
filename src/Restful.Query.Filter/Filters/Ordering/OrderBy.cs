using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Restful.Query.Filter.Infraestructure.Extensions;

namespace Restful.Query.Filter.Filters.Ordering
{
    public class OrderBy : Collection<Field>
    {
        private const string Pattern = @"filter\[order](\[\d+])?\=(?<property>\w+)\s(?<direction>asc|desc)";

        protected OrderBy()
        {

        }

        public OrderBy(IEnumerable<Field> fields)
        {
            foreach (var field in fields)
            {
                Items.Add(field);
            }
        }

        public static implicit operator OrderBy(string query)
        {
            query = HttpUtility.UrlDecode(query);

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