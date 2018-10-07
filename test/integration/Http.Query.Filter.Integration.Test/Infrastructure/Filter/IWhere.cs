namespace Http.Query.Filter.Integration.Test.Infrastructure.Filter
{
    using System;
    using Http.Query.Filter;

    internal interface IWhere<out TReturn, in TFilter, in TParam>
        where TFilter : IFilter
    {
        Func<TParam, TReturn> Apply(TFilter filter);

        TReturn Apply(TParam param);
    }
}
