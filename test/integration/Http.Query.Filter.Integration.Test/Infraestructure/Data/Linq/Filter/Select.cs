namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Entities;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;

    internal class Select : ISelect<Filter, Account>
    {
        private Filter filter;

        public Func<Account, dynamic> Apply(Filter filter)
        {
            this.filter = filter;
            return this.Apply;
        }

        public dynamic Apply(Account entity)
        {
            if (!this.filter.HasVisualization)
            {
                return entity;
            }

            var type = entity.GetType();
            const BindingFlags BindingAttr = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

            var props = new Dictionary<string, object>();

            foreach (var field in this.filter.Fields)
            {
                if (!field.Value)
                {
                    continue;
                }

                var value = type
                    .GetProperty(field.Key, BindingAttr)
                    .GetValue(entity);

                props.Add(field.Key, value);
            }

            return props;
        }
    }
}