namespace Http.Query.Filter.Filters.Visualization
{
    public class Field
    {
        public Field(string name, bool show)
        {
            this.Name = name;
            this.Show = show;
        }

        public string Name { get; protected set; }

        public bool Show { get; protected set; }
    }
}