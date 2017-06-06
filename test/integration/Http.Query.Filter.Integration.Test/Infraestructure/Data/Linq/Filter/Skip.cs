namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Filter
{
    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;

    internal class Skip : ISkip<int, Filter>
    {
        public int Apply(Filter filter)
        {
            if (filter.Skip == null)
            {
                return 0;
            }

            if (filter.Skip < 1)
            {
                return 0;
            }

            return filter.Skip;
        }
    }
}