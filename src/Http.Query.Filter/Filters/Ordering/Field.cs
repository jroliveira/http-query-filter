namespace Http.Query.Filter.Filters.Ordering
{
    public class Field
    {
        public Field(string name, OrderByDirection direction)
        {
            this.Name = name;
            this.Direction = direction;
        }

        public string Name { get; protected set; }

        public OrderByDirection Direction { get; protected set; }
    }
}