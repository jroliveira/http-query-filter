namespace Http.Query.Filter.Integration.Test.Infraestructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;

    internal class Paged<TSource>
    {
        private readonly ISkip<int, Query.Filter.Filter> skip;
        private readonly ILimit<int, Query.Filter.Filter> limit;

        public Paged(
            ISkip<int, Query.Filter.Filter> skip,
            ILimit<int, Query.Filter.Filter> limit)
        {
            this.skip = skip;
            this.limit = limit;

            this.Data = new List<TSource>();
            this.Skip = this.skip.Apply(null);
            this.Limit = this.limit.Apply(null);
        }

        public ICollection<TSource> Data { get; }

        public int Skip { get; private set; }

        public int Limit { get; private set; }

        public long Pages => this.Limit == 0 ? 1 : (long)Math.Ceiling((double)this.Data.Count / this.Limit);

        public Paged<TSource> Paginate(Query.Filter.Filter filter)
        {
            this.Skip = this.skip.Apply(filter);
            this.Limit = this.limit.Apply(filter);

            return this;
        }

        public void AddRange(ICollection<TSource> data)
        {
            data
                .ToList()
                .ForEach(this.Data.Add);
        }

        public virtual void Add(TSource item)
        {
            this.Data.Add(item);
        }
    }
}
