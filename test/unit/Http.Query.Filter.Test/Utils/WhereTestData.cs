namespace Http.Query.Filter.Test.Utils
{
    using System;
    using System.Collections.Generic;

    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Condition.Operators;

    public abstract class WhereTestData : TestDataBase
    {
        protected static readonly Func<string, object, Comparison, Condition> Item = (field, value, comparison) => new Condition(field, value, comparison);
        protected static readonly Func<string, object, Comparison, Where> Field = (field, value, comparison) => Fields(data => data.Add(Item(field, value, comparison)));
        protected static readonly Func<Action<IList<Condition>>, Where> Fields = afterCreating =>
            {
                var data = new List<Condition>();
                afterCreating(data);

                var @return = new Where(data);

                return @return;
            };
    }
}