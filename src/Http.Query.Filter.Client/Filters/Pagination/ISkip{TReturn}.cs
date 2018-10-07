namespace Http.Query.Filter.Client.Filters.Pagination
{
    public interface ISkip<TReturn>
    {
        IFilter<TReturn> Skip(uint skip);
    }
}
