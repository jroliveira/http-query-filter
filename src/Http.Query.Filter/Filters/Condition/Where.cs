namespace Http.Query.Filter.Filters.Condition
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Filters.Condition.Operators;
    using Http.Query.Filter.Infraestructure.Extensions;

    public class Where : ReadOnlyCollection<Condition>
    {
        private const string Pattern = @"filter\[where]?\[(?<field>\w+)\](\[(?<comparison>gt|lt)\])?=(?<value>[^&]*)&?";

        public Where(IList<Condition> conditions)
            : base(conditions)
        {
        }

        public static implicit operator Where(string query)
        {
            query = WebUtility.UrlDecode(query);

            var data = Get(query);
            var conditions = data as IList<Condition> ?? data.ToList();

            if (!conditions.Any())
            {
                return null;
            }

            return new Where(conditions);
        }

        private static IEnumerable<Condition> Get(string query)
        {
            var matches = Regex.Matches(query, Pattern, RegexOptions.IgnoreCase);

            return
                from Match match in matches
                let field = match.Get("field")
                let value = match.Get("value")
                let comparison = GetComparison(match)
                select new Condition(field, value, comparison);
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
    }
}