namespace Http.Query.Filter
{
    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Ordering;
    using Http.Query.Filter.Filters.Pagination;
    using Http.Query.Filter.Filters.Visualization;
    using static System.String;

    public sealed class Filter : IFilter
    {
        public Limit Limit { get; private set; }

        public Skip Skip { get; private set; }

        public OrderBy OrderBy { get; private set; }

        public Where Where { get; private set; }

        public Fields Fields { get; private set; }

        public bool HasCondition => this.Where != null;

        public bool HasOrdering => this.OrderBy != null;

        public bool HasVisualization => this.Fields != null;

        public static implicit operator Filter(string query) => IsNullOrEmpty(query)
            ? new Filter()
            : new Filter
            {
                Skip = query,
                Limit = query,
                OrderBy = query,
                Where = query,
                Fields = query,
            };
    }
}
