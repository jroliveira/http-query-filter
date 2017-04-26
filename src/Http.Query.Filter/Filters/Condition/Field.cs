namespace Http.Query.Filter.Filters.Condition
{
    using Http.Query.Filter.Filters.Condition.Operators;

    public class Field
    {
        public Field(string name, object value, Comparison comparison, Logical? logical)
        {
            this.Name = name;
            this.Value = value;
            this.Comparison = comparison;
            this.Logical = logical;
        }

        public string Name { get; protected set; }

        public object Value { get; protected set; }

        public Comparison Comparison { get; protected set; }

        public Logical? Logical { get; protected set; }
    }
}