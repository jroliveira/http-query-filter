using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Restful.Query.Filter.Order
{
    public class Order
    {
        private const string Pattern = @"filter\[order]\=(?<property>\w+)(,(\s)?(?<property>\w+))*\s(?<sorts>ASC|DESC)";
        private readonly ICollection<string> _properties;

        public Sorts Sorts { get; private set; }
        public string Property { get { return _properties.First(); } }

        public Order(ICollection<string> properties, Sorts sorts)
        {
            _properties = properties;
            Sorts = sorts;
        }

        public static implicit operator Order(string query)
        {
            query = HttpUtility.UrlDecode(query);

            var properties = GetProperties(query);
            if (properties == null)
            {
                return null;
            }

            var sorts = GetSorts(query);

            return new Order(properties, sorts);
        }

        private static Sorts GetSorts(string query)
        {
            var match = Regex.Match(query, Pattern, RegexOptions.IgnoreCase);

            var sort = match.Groups["sorts"].Value.ToLower();

            var sorts = new Dictionary<string, Sorts>
            {
                { "asc", Sorts.Asc },
                { "desc", Sorts.Desc }
            };

            return sorts[sort];
        }

        private static ICollection<string> GetProperties(string query)
        {
            var matches = Regex.Matches(query, Pattern, RegexOptions.IgnoreCase);

            var properties = new List<string>();

            foreach (Match m in matches)
            {
                var range = from object capture in m.Groups["property"].Captures
                            select capture.ToString();

                properties.AddRange(range);
            }

            if (!properties.Any())
            {
                return null;
            }

            return properties;
        }
    }
}