namespace Restful.Query.Filter.Where
{
    public class Property
    {
        public virtual string Name { get; protected set; }
        public virtual object Value { get; protected set; }

        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}