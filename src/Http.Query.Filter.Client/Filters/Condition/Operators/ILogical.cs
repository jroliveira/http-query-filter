namespace Http.Query.Filter.Client.Filters.Condition.Operators
{
    public interface ILogical
    {
        ICondition And(ICondition condition);

        ICondition Or(ICondition condition);
    }
}
