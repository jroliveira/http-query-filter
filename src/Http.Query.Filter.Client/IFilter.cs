namespace Http.Query.Filter.Client
{
    using Http.Query.Filter.Client.Builders;
    using Http.Query.Filter.Client.Filters;
    using Http.Query.Filter.Client.Filters.Pagination;

    public interface IFilter<TReturn> : IPagination<TReturn>, ISelect<TReturn>, IBuilderAsync<TReturn>
    {
    }
}