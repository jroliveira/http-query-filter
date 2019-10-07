namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using System;
    using System.Linq;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter.Extensions;

    using static System.String;

    internal readonly struct Where<TParam> : IWhere<bool, Filter, TParam>
    {
        public Func<TParam, bool> Apply(Filter filter) => param =>
        {
            if (param == null)
            {
                return true;
            }

            if (!filter.HasCondition)
            {
                return true;
            }

            return filter
                .Where
                .All(condition => param
                    .GetOrElse(condition.Field, Empty)
                    .ToString()
                    .Verify(condition.Value, condition.Comparison));
        };
    }
}
