namespace Http.Query.Filter.Client.Filters.Pagination
{
    public interface IPagination<TReturn> : ISkip<TReturn>, ILimit<TReturn>
    {
    }
}
