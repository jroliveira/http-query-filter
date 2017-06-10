namespace Http.Query.Filter.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Filter<TReturn> : IFilter<TReturn>
    {
        private readonly Func<string, Task<TReturn>> execute;
        private readonly ICollection<string> filters;

        public Filter(Func<string, Task<TReturn>> execute)
        {
            this.execute = execute;
            this.filters = new List<string>();
        }

        public IFilter<TReturn> Skip(int skip)
        {
            if (skip < 0)
            {
                skip = 0;
            }

            this.filters.Add($"filter[skip]={skip}");
            return this;
        }

        public IFilter<TReturn> Limit(int limit)
        {
            if (limit < 1)
            {
                limit = 1;
            }

            this.filters.Add($"filter[limit]={limit}");
            return this;
        }

        public IFilter<TReturn> Select(params object[] fields)
        {
            foreach (var field in fields)
            {
                this.filters.Add($"filter[fields][{field}]=true");
            }

            return this;
        }

        public async Task<TReturn> BuildAsync()
        {
            var queryFilter = string.Join("&", this.filters);
            return await this.execute(queryFilter);
        }
    }
}
