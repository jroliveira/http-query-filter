namespace Restful.Query.Filter.Fields
{
    public class Field
    {
        public virtual string Name { get; protected set; }
        public virtual bool Show { get; protected set; }

        public Field(string name, bool show)
        {
            Name = name;
            Show = show;
        }
    }
}