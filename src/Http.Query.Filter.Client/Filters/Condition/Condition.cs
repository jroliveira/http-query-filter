namespace Http.Query.Filter.Client.Filters.Condition
{
    using System.Collections.Generic;
    using System.Text;

    using static System.String;

    public sealed class Condition : ICondition
    {
        private readonly Field field;
        private readonly object value;
        private readonly string comparison;
        private readonly List<ICondition> conditions;

        private string logical;

        public Condition(Field field, object value, string comparison = "")
        {
            this.field = field;
            this.value = value;
            this.comparison = comparison;
            this.conditions = new List<ICondition> { this };
        }

        public IReadOnlyCollection<ICondition> InnerConditions => new List<ICondition>(this.conditions);

        public override string ToString()
        {
            var result = new StringBuilder("filter[where]");

            if (!IsNullOrEmpty(this.logical))
            {
                result.Append($"[{this.logical}][0]");
            }

            result.Append($"[{this.field}]");

            if (!IsNullOrEmpty(this.comparison))
            {
                result.Append($"[{this.comparison}]");
            }

            result.Append($"={this.value}");

            return result.ToString();
        }

        public ICondition And(ICondition condition)
        {
            this.logical = "and";
            this.conditions.Add(condition);
            return this;
        }

        public ICondition Or(ICondition condition)
        {
            this.logical = "or";
            this.conditions.AddRange(condition.InnerConditions);
            return this;
        }
    }
}
