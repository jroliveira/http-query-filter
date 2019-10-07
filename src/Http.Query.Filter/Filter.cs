namespace Http.Query.Filter
{
    using System.Linq;

    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Ordering;
    using Http.Query.Filter.Filters.Pagination;
    using Http.Query.Filter.Filters.Visualization;

    public sealed class Filter : IFilter
    {
        private Filter(string query)
        {
            this.Limit = query;
            this.Skip = query;
            this.OrderBy = query;
            this.Where = query;
            this.Fields = query;
        }

        public Limit Limit { get; }

        public Skip Skip { get; }

        public OrderBy OrderBy { get; }

        public Where Where { get; }

        public Fields Fields { get; }

        public bool HasCondition => this.Where.Any();

        public bool HasOrdering => this.OrderBy.Any();

        public bool HasVisualization => this.Fields.Any();

        public static implicit operator Filter(string query) => new Filter(query);
    }
}
