namespace Http.Query.Filter.Filters.Condition
{
    using Http.Query.Filter.Filters.Condition.Operators;

    public class Condition
    {
        public Condition(string field, object value, Comparison comparison)
        {
            this.Field = field;
            this.Value = value;
            this.Comparison = comparison;
        }

        public string Field { get; protected set; }

        public object Value { get; protected set; }

        public Comparison Comparison { get; protected set; }
    }
}