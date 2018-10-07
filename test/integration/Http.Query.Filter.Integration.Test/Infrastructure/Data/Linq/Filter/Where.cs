namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using System;
    using System.Linq;
    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter.Extensions;
    using static System.Reflection.BindingFlags;

    internal class Where<TEntity> : IWhere<bool, Filter, TEntity>
    {
        private Filter filter;

        public Func<TEntity, bool> Apply(Filter filter)
        {
            this.filter = filter;
            return this.Apply;
        }

        public bool Apply(TEntity entity) => !this.filter.HasCondition || this.filter
             .Where
             .All(condition => entity
                 .GetType()
                 .GetProperty(condition.Field, IgnoreCase | Public | Instance)
                 .GetValue(entity)
                 .ToString()
                 .Verify(condition.Value.ToString(), condition.Comparison));
    }
}
