namespace Http.Query.Filter.Filters.Condition
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Filters.Condition.Operators;
    using Http.Query.Filter.Infrastructure.Extensions;

    using static Http.Query.Filter.Filters.Condition.Operators.Comparison;
    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;

    public sealed class Where : ReadOnlyCollection<Condition>
    {
        private const string Pattern = @"filter\[where]?\[(?<field>\w+)\](\[(?<comparison>gt|lt)\])?=(?<value>[^&]*)&?";

        private static readonly Regex Regex = new Regex(Pattern, IgnoreCase | Compiled);
        private static readonly Dictionary<string, Comparison> Operations = new Dictionary<string, Comparison>
        {
            { "gt", GreaterThan },
            { "lt", LessThan },
        };

        public Where(IList<Condition> conditions)
            : base(conditions)
        {
        }

        public static implicit operator Where(string query)
        {
            if (IsNullOrWhiteSpace(query))
            {
                return null;
            }

            var conditions = Get(UrlDecode(query));

            if (!conditions.Any())
            {
                return null;
            }

            return new Where(conditions);
        }

        private static IList<Condition> Get(string query)
        {
            var matches = Regex.Matches(query);

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

            if (IsNullOrEmpty(operation))
            {
                return Equal;
            }

            return Operations[operation];
        }
    }
}
