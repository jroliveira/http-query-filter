namespace System.Linq
{
    using System.Collections.Generic;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter;

    using static System.Convert;

    internal static class EnumerableExtension
    {
        internal static IEnumerable<TSource> Limit<TSource>(this IEnumerable<TSource> @this, Filter filter) => @this.Take(ToInt32(default(Limit).Apply(filter)));

        internal static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> @this, Filter filter) => @this.Skip(ToInt32(default(Skip).Apply(filter)));

        internal static IEnumerable<dynamic> Select<TSource>(this IEnumerable<TSource> @this, Filter filter) => @this.Select(default(Select<TSource>).Apply(filter));

        internal static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> @this, Filter filter) => @this.Where(default(Where<TSource>).Apply(filter));
    }
}
