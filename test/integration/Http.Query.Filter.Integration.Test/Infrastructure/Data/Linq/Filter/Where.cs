namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using System;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;

    using static Http.Query.Filter.Filters.Condition.Operators.Logical;

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

            var satisfy = filter.Where(param);

            return satisfy(And) && satisfy(Or);
        };
    }
}
