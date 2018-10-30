namespace Http.Query.Filter.Integration.Test.Infrastructure.Filter
{
    using System;

    using Http.Query.Filter;

    internal interface ISelect<in TFilter, in TParam>
        where TFilter : IFilter
    {
        Func<TParam, dynamic> Apply(TFilter filter);

        dynamic Apply(TParam param);
    }
}
