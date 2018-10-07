namespace Http.Query.Filter
{
    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Ordering;
    using Http.Query.Filter.Filters.Pagination;
    using Http.Query.Filter.Filters.Visualization;

    public interface IFilter
    {
        Limit Limit { get; }

        Skip Skip { get; }

        OrderBy OrderBy { get; }

        Where Where { get; }

        Fields Fields { get; }

        bool HasCondition { get; }

        bool HasOrdering { get; }

        bool HasVisualization { get; }
    }
}
