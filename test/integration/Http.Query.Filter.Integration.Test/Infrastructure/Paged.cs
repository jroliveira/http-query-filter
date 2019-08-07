namespace Http.Query.Filter.Integration.Test.Infrastructure
{
    using System;
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

        internal long Pages => this.Limit == 0 ? 1 : (long)Math.Ceiling((double)this.Data.Count / this.Limit);
    }
}
