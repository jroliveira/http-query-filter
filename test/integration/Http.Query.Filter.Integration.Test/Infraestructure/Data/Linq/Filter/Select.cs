namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;

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

            var type = entity.GetType();
            const BindingFlags BindingAttr = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

            var props = new Dictionary<string, object>();

            foreach (var field in this.filter.Fields.Where(field => field.Value))
            {
                var value = type
                    .GetProperty(field.Key, BindingAttr)
                    .GetValue(entity);

                props.Add(field.Key, value);
            }

            return props;
        }
    }
}