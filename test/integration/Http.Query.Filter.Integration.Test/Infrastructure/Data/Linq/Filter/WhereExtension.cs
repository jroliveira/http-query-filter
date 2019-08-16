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
        internal static bool Check<TParam>(this IFilter @this, TParam param)
        {
            var conditions = new List<Condition>(@this
                .Where
                .OrderBy(condition => condition.Index));

            var list = new List<Condition> { conditions.First() };
            var result = new List<List<Condition>> { list };
            var firstLogicalOperator = conditions.First().Logical;

            for (ushort i = 1; i < conditions.Count; i++)
            {
                if (firstLogicalOperator == conditions[i].Logical)
                {
                    list.Add(conditions[i]);
                }
                else
                {
                    list = new List<Condition>();
                }
            }

            return true;
        }

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

            switch (logical)
            {
                case Or:
                    return conditions.Any(satisfy);

                case And:
                default:
                    return conditions.All(satisfy);
            }
        };

        private static Func<Condition, bool> Satisfy<TParam>(TParam param) => condition => param
            .GetOrElse(condition.Field, Empty)
            .ToString()
            .Verify(condition.Value, condition.Comparison);
    }
}
