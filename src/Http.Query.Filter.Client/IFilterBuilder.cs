namespace Http.Query.Filter.Client
{
    using Http.Query.Filter.Client.Filters;
    using Http.Query.Filter.Client.Filters.Condition;
    using Http.Query.Filter.Client.Filters.Pagination;

    public interface IFilterBuilder : ISkip, ILimit, ISelect, IWhere
    {
    }
}
