namespace Http.Query.Filter.Client.Filters.Pagination
{
    public interface ILimit<TReturn>
    {
        IFilter<TReturn> Limit(uint limit);
    }
}
