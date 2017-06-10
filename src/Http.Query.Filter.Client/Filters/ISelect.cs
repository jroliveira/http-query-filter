namespace Http.Query.Filter.Client.Filters
{
    public interface ISelect<TReturn>
    {
        IFilter<TReturn> Select(params object[] fields);
    }
}