namespace Http.Query.Filter.Filters.Condition
{
    using Http.Query.Filter.Filters.Condition.Operators;

    public sealed class Condition
    {
        public Condition(string field, string value, Comparison comparison)
        {
            this.Field = field;
            this.Value = value;
            this.Comparison = comparison;
        }

        public string Field { get; }

        public string Value { get; }

        public Comparison Comparison { get; }
    }
}
