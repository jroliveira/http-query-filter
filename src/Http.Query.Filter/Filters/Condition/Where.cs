namespace Http.Query.Filter.Filters.Condition
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Filters.Condition.Operators;
    using Http.Query.Filter.Infrastructure.Extensions;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;
    using static Http.Query.Filter.Filters.Condition.Operators.Comparison;
    using static Http.Query.Filter.Filters.Condition.Operators.Logical;

    public sealed class Where : ReadOnlyCollection<Condition>
    {
        private const string Pattern = @"filter\[where]?(\[(?<logical>and|or)\]\[(?<index>\d+)\])?(\[(?<field>\w+)\])?(\[(?<comparison>gt|lt)\])?=(?<value>[^&]*)&?";

        private static readonly Func<string, MatchCollection> Matches = new Regex(Pattern, IgnoreCase | Compiled).Matches;
        private static readonly IReadOnlyDictionary<string, Comparison> ComparisonOperations = new Dictionary<string, Comparison>
        {
            { "gt", GreaterThan },
            { "lt", LessThan },
        };

        private static readonly IReadOnlyDictionary<string, Logical> LogicalOperations = new Dictionary<string, Logical>
        {
            { "and", And },
            { "or", Or },
        };

        internal Where(IEnumerable<Condition> conditions)
            : base(conditions.ToList())
        {
        }

        private Where()
            : this(new List<Condition>())
        {
        }

        public static implicit operator Where(string query)
        {
            if (IsNullOrWhiteSpace(query))
            {
                return new Where();
            }

            var conditions = GetConditions(query);

            return conditions.Any()
                ? new Where(conditions)
                : new Where();
        }

        private static IReadOnlyCollection<Condition> GetConditions(string query) => new List<Condition>(
            from Match match in Matches(UrlDecode(query))
            let field = match.GetValue("field")
            let value = match.GetValue("value")
            let comparison = GetOrElseOperator(match, "comparison", ComparisonOperations, Equal)
            let logical = GetOrElseOperator(match, "logical", LogicalOperations, And)
            let index = ushort.TryParse(match.GetValue("index"), out var number) ? number : default
            select new Condition(field, value, comparison, logical, index));

        private static TOperator GetOrElseOperator<TOperator>(
            Match match,
            string operatorName,
            IReadOnlyDictionary<string, TOperator> operations,
            TOperator @default)
        {
            var operation = match.GetValue(operatorName).ToLower();

            return IsNullOrEmpty(operation)
                ? @default
                : operations[operation];
        }
    }
}
