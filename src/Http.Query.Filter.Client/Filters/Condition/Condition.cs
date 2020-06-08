namespace Http.Query.Filter.Client.Filters.Condition
{
    using System.Collections.Generic;
    using System.Text;

    using static System.String;

    internal sealed class Condition : ICondition
    {
        private readonly Field field;
        private readonly object[] values;
        private readonly string comparison;
        private readonly List<ICondition> conditions;

        private string? logical;

        private Condition(Field field, object[] values, string comparison)
        {
            this.field = field;
            this.values = values;
            this.comparison = comparison;
            this.conditions = new List<ICondition> { this };
        }

        public IReadOnlyCollection<ICondition> InnerConditions => new List<ICondition>(this.conditions);

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var value in this.values)
            {
                result.Append("filter[where]");

                if (!IsNullOrEmpty(this.logical))
                {
                    result.Append($"[{this.logical}]");
                }

                result.Append($"[{this.field}]");

                if (!IsNullOrEmpty(this.comparison))
                {
                    result.Append($"[{this.comparison}]");
                }

                result.Append($"={value}&");
            }

            return result
                .ToString()
                .Remove(result.Length - 1);
        }

        public ICondition And(ICondition condition)
        {
            this.logical = "and";

            var otherCondition = (Condition)condition;
            otherCondition.logical ??= "and";
            this.conditions.Add(otherCondition);

            return this;
        }

        public ICondition Or(ICondition condition)
        {
            this.logical = "or";

            var otherCondition = (Condition)condition;
            otherCondition.logical ??= "or";
            this.conditions.AddRange(condition.InnerConditions);

            return this;
        }

        internal static Condition NewCondition(Field field, object value, string comparison = "") => new Condition(field, new[] { value }, comparison);

        internal static Condition NewCondition(Field field, object[] values, string comparison = "") => new Condition(field, values, comparison);
    }
}
