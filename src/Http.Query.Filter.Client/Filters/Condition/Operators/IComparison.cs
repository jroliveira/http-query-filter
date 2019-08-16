namespace Http.Query.Filter.Client.Filters.Condition.Operators
{
    public interface IComparison
    {
        ICondition GreaterThan(object value);

        ICondition LessThan(object value);

        ICondition Equal(object value);
    }
}
