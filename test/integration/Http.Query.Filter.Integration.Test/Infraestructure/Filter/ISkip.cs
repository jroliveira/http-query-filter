namespace Http.Query.Filter.Integration.Test.Infraestructure.Filter
{
    using Http.Query.Filter;

    internal interface ISkip<out TReturn, in TFilter>
        where TFilter : Filter
    {
        TReturn Apply(TFilter filter);
    }
}