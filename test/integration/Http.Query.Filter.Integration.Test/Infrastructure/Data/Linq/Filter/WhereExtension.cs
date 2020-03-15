namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Condition.Operators;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter.Extensions;

    using static System.String;

    using static Http.Query.Filter.Filters.Condition.Operators.Logical;

    internal static class WhereExtension
    {
        internal static Func<Logical, bool> Where<TParam>(this IFilter @this, TParam param) => logical =>
        {
            var conditions = new List<Condition>(@this
                .Where
                .Where(condition => condition.Logical == logical));

            if (!conditions.Any())
            {
                return true;
            }

            var satisfy = Satisfy(param);

            return logical switch
            {
                Or => conditions.Any(satisfy),
                And => conditions.All(satisfy),
                _ => conditions.All(satisfy)
            };
        };

        private static Func<Condition, bool>? Satisfy<TParam>(TParam param) => condition => param
            .GetOrElse(condition.Field, Empty)
            .ToString()
            .Verify(condition.Value, condition.Comparison);
    }
}
