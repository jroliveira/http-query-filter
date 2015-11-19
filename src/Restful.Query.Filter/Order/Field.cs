namespace Restful.Query.Filter.Order
{
    public class Field
    {
        public virtual string Name { get; protected set; }
        public virtual Sorts Sorts { get; protected set; }

        public Field(string name, Sorts sorts)
        {
            Name = name;
            Sorts = sorts;
        }
    }
}