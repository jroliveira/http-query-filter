namespace Http.Query.Filter.Client.Filters.Condition
{
    using System.Collections.Generic;

    using Http.Query.Filter.Client.Filters.Condition.Operators;

    public interface ICondition : ILogical
    {
        IReadOnlyCollection<ICondition> InnerConditions { get; }
    }
}
