using Restful.Query.Filter.Filters.Condition.Operators;

namespace Restful.Query.Filter.Filters.Condition
{
    public class Field
    {
        public virtual string Name { get; protected set; }
        public virtual object Value { get; protected set; }
        public virtual Comparison Comparison { get; protected set; }
        public virtual Logical? Logical { get; protected set; }

        public Field(string name, object value, Comparison comparison, Logical? logical)
        {
            Name = name;
            Value = value;
            Comparison = comparison;
            Logical = logical;
        }
    }
}