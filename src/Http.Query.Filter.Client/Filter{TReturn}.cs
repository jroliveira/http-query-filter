namespace Http.Query.Filter.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Http.Query.Filter.Client.Filters.Condition;

    using static System.String;

    public sealed class Filter<TReturn> : IFilter<TReturn>
    {
        private readonly Func<string, Task<TReturn>> done;
        private readonly ICollection<string> filters = new List<string>();

        /// <summary>
        /// Filter's constructor.
        /// </summary>
        /// <param name="done">done is a function that will be executed after compiling the filters.</param>
        public Filter(Func<string, Task<TReturn>> done) => this.done = done;

        /// <summary>
        /// A skip filter omits the specified number of returned records.
        /// </summary>
        /// <param name="skip">skip is the number of records to skip.</param>
        /// <returns></returns>
        public IFilter<TReturn> Skip(uint skip)
        {
            this.filters.Add($"filter[skip]={skip}");
            return this;
        }

        /// <summary>
        /// A limit filter limits the number of records returned to the specified number (or less).
        /// </summary>
        /// <param name="limit">limit is the maximum number of results (records) to return.</param>
        /// <returns></returns>
        public IFilter<TReturn> Limit(uint limit)
        {
            if (limit < 1)
            {
                limit = 1;
            }

            this.filters.Add($"filter[limit]={limit}");
            return this;
        }

        /// <summary>
        /// A select filter specifies properties to include from the results.
        /// </summary>
        /// <param name="fields">
        /// fields are the names of the properties to include from the results.
        /// </param>
        /// <returns></returns>
        public IFilter<TReturn> Select(params string[] fields)
        {
            foreach (var field in fields)
            {
                this.filters.Add($"filter[fields][{field}]=true");
            }

            return this;
        }

        /// <summary>
        /// Performs the operation with selected filters.
        /// </summary>
        /// <returns>Returns the API data.</returns>
        public Task<TReturn> Build() => this.done(Join("&", this.filters));

        public IFilter<TReturn> Where(ICondition condition)
        {
            foreach (var filter in condition.InnerConditions)
            {
                this.filters.Add(filter.ToString());
            }

            return this;
        }
    }
}
