namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Filter
{
    using System.Linq;
    using System.Reflection;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Entities;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter.Extensions;

    internal class Where : IWhere<bool, Filter, Account>
    {
        public bool Apply(Filter filter, Account entity)
        {
            if (!filter.HasCondition)
            {
                return true;
            }

            var type = entity.GetType();
            const BindingFlags BindingAttr = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

            return filter
                .Where
                .All(condition => type
                    .GetProperty(condition.Field, BindingAttr)
                    .GetValue(entity)
                    .ToString()
                    .Verify(condition.Value.ToString(), condition.Comparison));
        }
    }
}