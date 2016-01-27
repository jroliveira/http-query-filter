using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Restful.Query.Filter.Filters.Condition.Operators;
using Restful.Query.Filter.Infraestructure.Extensions;

namespace Restful.Query.Filter.Filters.Condition
{
    public class Where : Collection<Field>
    {
        private const string Pattern = @"filter\[where](\[(?<logical>and|or)]\[\d+])?\[(?<property>\w+)\](\[(?<comparison>gt|lt)\])?=(?<value>[^&]*)&?";

        protected Where()
        {

        }

        public Where(IEnumerable<Field> fields)
        {
            foreach (var field in fields)
            {
                Items.Add(field);
            }
        }

        public static implicit operator Where(string query)
        {
            query = HttpUtility.UrlDecode(query);

            var fields = Get(query);

            if (fields == null || !fields.Any())
            {
                return null;
            }

            return new Where(fields);
        }

        private static IEnumerable<Field> Get(string query)
        {
            var matches = Regex.Matches(query, Pattern, RegexOptions.IgnoreCase);

            return
                from Match match in matches
                let property = match.Get("property")
                let value = match.Get("value")
                let comparison = GetComparison(match)
                let logical = GetLogical(match)
                select new Field(property, value, comparison, logical);
        }

        private static Comparison GetComparison(Match match)
        {
            var operation = match.Groups["comparison"].Value.ToLower();
            if (string.IsNullOrEmpty(operation))
            {
                return Comparison.Equal;
            }

            var operations = new Dictionary<string, Comparison>
            {
                { "gt", Comparison.GreaterThan },
                { "lt", Comparison.LessThan }
            };

            return operations[operation];
        }

        private static Logical? GetLogical(Match match)
        {
            var operation = match.Groups["logical"].Value.ToLower();
            if (string.IsNullOrEmpty(operation))
            {
                return null;
            }

            var operations = new Dictionary<string, Logical>
            {
                { "and", Logical.And },
                { "or", Logical.Or }
            };

            return operations[operation];
        }
    }
}