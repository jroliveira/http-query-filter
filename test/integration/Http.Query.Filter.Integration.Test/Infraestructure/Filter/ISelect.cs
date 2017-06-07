namespace Http.Query.Filter.Integration.Test.Infraestructure.Filter
{
    using System;

    using Http.Query.Filter;

    internal interface ISelect<in TFilter, in TParam>
        where TFilter : Filter
    {
        Func<TParam, dynamic> Apply(TFilter filter);

        dynamic Apply(TParam param);
    }
}