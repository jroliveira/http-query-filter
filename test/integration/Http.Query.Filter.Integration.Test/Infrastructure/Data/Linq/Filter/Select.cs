namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;
    using static System.Reflection.BindingFlags;

    internal class Select<TEntity> : ISelect<Filter, TEntity>
    {
        private Filter filter;

        public Func<TEntity, dynamic> Apply(Filter filter)
        {
            this.filter = filter;
            return this.Apply;
        }

        public dynamic Apply(TEntity entity)
        {
            if (!this.filter.HasVisualization)
            {
                return entity;
            }

            var props = new Dictionary<string, object>();

            foreach (var field in this.filter.Fields.Where(field => field.Value))
            {
                var value = entity
                    .GetType()
                    .GetProperty(field.Key, IgnoreCase | Public | Instance)
                    .GetValue(entity);

                props.Add(field.Key, value);
            }

            return props;
        }
    }
}
