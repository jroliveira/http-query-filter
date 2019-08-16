namespace Http.Query.Filter.Client.Filters.Condition
{
    public interface IWhere<TReturn>
    {
        IFilter<TReturn> Where(ICondition condition);
    }
}
