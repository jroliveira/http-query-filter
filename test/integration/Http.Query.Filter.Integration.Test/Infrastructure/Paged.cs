namespace Http.Query.Filter.Integration.Test.Infrastructure
{
    using System.Collections.Generic;

    internal class Paged<TSource>
    {
        internal Paged(IReadOnlyCollection<TSource> data, uint skip, uint limit)
        {
            this.Data = data;
            this.Skip = skip;
            this.Limit = limit;
        }

        internal IReadOnlyCollection<TSource> Data { get; }

        internal uint Skip { get; }

        internal uint Limit { get; }
    }
}
