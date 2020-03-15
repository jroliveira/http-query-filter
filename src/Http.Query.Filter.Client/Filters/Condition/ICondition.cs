namespace Http.Query.Filter.Client.Filters.Condition
{
    using System.Collections.Generic;

    public interface ICondition
    {
        IReadOnlyCollection<ICondition> InnerConditions { get; }

        ICondition And(ICondition condition);

        ICondition Or(ICondition condition);
    }
}
