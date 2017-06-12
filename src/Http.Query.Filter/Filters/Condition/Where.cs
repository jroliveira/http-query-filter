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

        private static Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);


        private static Dictionary<string, Comparison> operations = new Dictionary<string, Comparison>
            {
                { "gt", Comparison.GreaterThan },
                { "lt", Comparison.LessThan }
            };

        public Where(IList<Condition> conditions)
            : base(conditions)
        {
        }

        public static implicit operator Where(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            var conditions = Get(WebUtility.UrlDecode(query));

            if (!conditions.Any())
            {
                return null;
            }

            return new Where(conditions);
        }

        private static IList<Condition> Get(string query)
        {
            var matches = regex.Matches(query);

            return
                (from Match match in matches
                 let field = match.Get("field")
                 let value = match.Get("value")
                 let comparison = GetComparison(match)
                 select new Condition(field, value, comparison)).ToList();
        }

        private static Comparison GetComparison(Match match)
        {
            var operation = match.Groups["comparison"].Value.ToLower();

            if (string.IsNullOrEmpty(operation))
            {
                return Comparison.Equal;
            }

            return operations[operation];
        }
    }
}