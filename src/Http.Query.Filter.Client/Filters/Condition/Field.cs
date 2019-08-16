namespace Http.Query.Filter.Client.Filters.Condition
{
    using Http.Query.Filter.Client.Filters.Condition.Operators;

    public sealed class Field : IComparison
    {
        private readonly string name;

        internal Field(string name) => this.name = name;

        public static implicit operator Field(string name) => new Field(name);

        public static implicit operator string(Field field) => field.name;

        public static Field NewField(string name) => name;

        public override string ToString() => this.name;

        public ICondition GreaterThan(object value) => new Condition(this.name, value, "gt");

        public ICondition LessThan(object value) => new Condition(this.name, value, "lt");

        public ICondition Equal(object value) => new Condition(this.name, value);
    }
}
