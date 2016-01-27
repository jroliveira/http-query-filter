namespace Restful.Query.Filter.Filters.Ordering
{
    public class Field
    {
        public virtual string Name { get; protected set; }
        public virtual OrderByDirection Direction { get; protected set; }

        public Field(string name, OrderByDirection direction)
        {
            Name = name;
            Direction = direction;
        }
    }
}