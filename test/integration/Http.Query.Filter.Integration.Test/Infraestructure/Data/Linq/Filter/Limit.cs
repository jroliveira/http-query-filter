namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Filter
{
    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;

    internal class Limit : ILimit<int, Filter>
    {
        public int Apply(Filter filter)
        {
            if (filter.Limit == null)
            {
                return 100;
            }

            if (filter.Limit < 1)
            {
                return 100;
            }

            return filter.Limit;
        }
    }
}