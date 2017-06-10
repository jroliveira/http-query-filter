namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Filter;

    internal static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Limit<TSource>(this IEnumerable<TSource> source, Filter filter)
        {
            var limit = new Limit().Apply(filter);

            return source.Take(limit);
        }

        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, Filter filter)
        {
            var skip = new Skip().Apply(filter);

            return source.Skip(skip);
        }

        public static IEnumerable<dynamic> Select<TSource>(this IEnumerable<TSource> source, Filter filter)
        {
            var select = new Select<TSource>().Apply(filter);

            return source.Select(select);
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Filter filter)
        {
            var where = new Where<TSource>().Apply(filter);

            return source.Where(item => where(item));
        }
    }
}