namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter;

    using static System.Convert;

    internal static class EnumerableExtensions
    {
        internal static IEnumerable<TSource> Limit<TSource>(this IEnumerable<TSource> source, Filter filter)
        {
            var limit = new Limit().Apply(filter);

            return source.Take(ToInt32(limit));
        }

        internal static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, Filter filter)
        {
            var skip = new Skip().Apply(filter);

            return source.Skip(ToInt32(skip));
        }

        internal static IEnumerable<dynamic> Select<TSource>(this IEnumerable<TSource> source, Filter filter)
        {
            var select = new Select<TSource>().Apply(filter);

            return source.Select(select);
        }

        internal static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Filter filter)
        {
            var where = new Where<TSource>().Apply(filter);

            return source.Where(item => where(item));
        }
    }
}
