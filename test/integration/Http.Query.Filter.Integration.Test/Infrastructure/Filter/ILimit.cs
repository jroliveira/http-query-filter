namespace Http.Query.Filter.Integration.Test.Infrastructure.Filter
{
    using Http.Query.Filter;

    internal interface ILimit<out TReturn, in TFilter>
        where TFilter : IFilter
    {
        TReturn Apply(TFilter filter);
    }
}
