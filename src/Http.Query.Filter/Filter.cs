namespace Http.Query.Filter
{
    using Http.Query.Filter.Filters;
    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Ordering;
    using Http.Query.Filter.Filters.Visualization;

    public class Filter
    {
        public virtual Limit Limit { get; protected set; }

        public virtual Skip Skip { get; protected set; }

        public virtual OrderBy OrderBy { get; protected set; }

        public virtual Where Where { get; protected set; }

        public virtual Fields Fields { get; protected set; }

        public virtual bool HasCondition => this.Where != null;

        public virtual bool HasOrdering => this.OrderBy != null;

        public virtual bool HasVisualization => this.Fields != null;

        public static implicit operator Filter(string query)
        {
            var filter = new Filter
            {
                Skip = query,
                Limit = query,
                OrderBy = query,
                Where = query,
                Fields = query
            };

            return filter;
        }
    }
}