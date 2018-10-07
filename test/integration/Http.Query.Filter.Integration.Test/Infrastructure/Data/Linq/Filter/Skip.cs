namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using Http.Query.Filter;
    using Http.Query.Filter.Filters.Pagination;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;

    internal class Skip : ISkip<uint, Filter>
    {
        public uint Apply(Filter filter) => filter
            .GetOrElse(new Filter())
            .Skip
            .GetOrElse(0U);
    }
}
