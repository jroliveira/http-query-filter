namespace Http.Query.Filter.Integration.Test.Infrastructure
{
    using System;
    using System.Collections.Generic;

    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;

    internal class Paged<TSource>
    {
        private readonly ISkip<uint, Query.Filter.Filter> skip;
        private readonly ILimit<uint, Query.Filter.Filter> limit;

        internal Paged(
            ISkip<uint, Query.Filter.Filter> skip,
            ILimit<uint, Query.Filter.Filter> limit)
        {
            this.skip = skip;
            this.limit = limit;

            this.Data = new List<TSource>();
            this.Skip = this.skip.Apply(default);
            this.Limit = this.limit.Apply(default);
        }

        internal List<TSource> Data { get; }

        internal uint Skip { get; private set; }

        internal uint Limit { get; private set; }

        internal long Pages => this.Limit == 0 ? 1 : (long)Math.Ceiling((double)this.Data.Count / this.Limit);

        internal Paged<TSource> Paginate(Query.Filter.Filter filter)
        {
            this.Skip = this.skip.Apply(filter);
            this.Limit = this.limit.Apply(filter);

            return this;
        }

        internal void AddRange(ICollection<TSource> data) => this.Data.AddRange(data);
    }
}
