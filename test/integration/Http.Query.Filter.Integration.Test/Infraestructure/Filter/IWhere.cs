namespace Http.Query.Filter.Integration.Test.Infraestructure.Filter
{
    using Http.Query.Filter;

    internal interface IWhere<out TReturn, in TFilter, in TParam>
        where TFilter : Filter
    {
        TReturn Apply(TFilter filter, TParam param);
    }
}
