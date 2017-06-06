namespace Http.Query.Filter.Integration.Test.Infraestructure
{
    using System;
    using System.Collections.Generic;

    internal class Paged<T>
    {
        public Paged(ICollection<T> data, int skip, int limit)
        {
            this.Data = data;
            this.Skip = skip;
            this.Limit = limit;
        }

        public ICollection<T> Data { get; }

        public int Skip { get; }

        public int Limit { get; }

        public long Pages => this.Limit == 0 ? 1 : (long)Math.Ceiling((double)this.Data.Count / this.Limit);

        public virtual void Add(T item)
        {
            this.Data.Add(item);
        }
    }
}
