namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using Http.Query.Filter;
    using Http.Query.Filter.Filters.Pagination;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;

    internal class Limit : ILimit<uint, Filter>
    {
        public uint Apply(Filter filter) => filter
            .GetOrElse(new Filter())
            .Limit
            .GetOrElse(100U);
    }
}
