namespace Http.Query.Filter.Integration.Test.Infrastructure.Filter
{
    using Http.Query.Filter;

    internal interface IPagination<out TReturn, in TFilter>
        where TFilter : IFilter
    {
        TReturn Apply(TFilter filter);
    }
}
