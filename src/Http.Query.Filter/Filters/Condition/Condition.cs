namespace Http.Query.Filter.Filters.Condition
{
    using Http.Query.Filter.Filters.Condition.Operators;

    using static Http.Query.Filter.Filters.Condition.Operators.Comparison;
    using static Http.Query.Filter.Filters.Condition.Operators.Logical;

    public sealed class Condition
    {
        internal Condition(string field, string value)
            : this(field, value, Equal, Undefined, 0)
        {
        }

        internal Condition(string field, string value, Comparison comparison)
            : this(field, value, comparison, Undefined, 0)
        {
        }

        internal Condition(string field, string value, Logical logical, ushort index)
            : this(field, value, Equal, logical, index)
        {
        }

        internal Condition(string field, string value, Comparison comparison, Logical logical, ushort index)
        {
            this.Field = field;
            this.Value = value;
            this.Comparison = comparison;
            this.Logical = logical;
            this.Index = index;
        }

        public string Field { get; }

        public string Value { get; }

        public Comparison Comparison { get; }

        public Logical Logical { get; }

        public ushort Index { get; }
    }
}
