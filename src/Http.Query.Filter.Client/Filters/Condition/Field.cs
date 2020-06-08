namespace Http.Query.Filter.Client.Filters.Condition
{
    using static Http.Query.Filter.Client.Filters.Condition.Condition;

    internal sealed class Field
    {
        private readonly string name;

        private Field(string name) => this.name = name;

        public static implicit operator Field(string name) => new Field(name);

        public override string ToString() => this.name;

        internal static Field NewField(string name) => new Field(name);

        internal ICondition GreaterThan(object value) => NewCondition(this.name, value, "gt");

        internal ICondition LessThan(object value) => NewCondition(this.name, value, "lt");

        internal ICondition Equal(object value) => NewCondition(this.name, value);

        internal ICondition Inq(params object[] values) => NewCondition(this.name, values, "inq");
    }
}
