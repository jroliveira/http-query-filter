namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;

    internal readonly struct Limit : IPagination<uint, Filter>
    {
        public uint Apply(Filter filter) => filter
            .Limit
            .GetOrElse(100U);
    }
}
