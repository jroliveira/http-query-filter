namespace Restful.Query.Filter.Where
{
    public class Property
    {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}