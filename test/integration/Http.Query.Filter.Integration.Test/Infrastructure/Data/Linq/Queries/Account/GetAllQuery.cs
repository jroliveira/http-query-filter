namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Queries.Account
{
    using System.Linq;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Collections;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;

    internal class GetAllQuery
    {
        private readonly IPagination<uint, Filter> skip;
        private readonly IPagination<uint, Filter> limit;

        internal GetAllQuery(
            IPagination<uint, Filter> skip,
            IPagination<uint, Filter> limit)
        {
            this.skip = skip;
            this.limit = limit;
        }

        internal virtual Paged<dynamic> GetResult(Filter filter)
        {
            var data = new Accounts()
                .Skip(filter)
                .Limit(filter)
                .Where(filter)
                .Select(filter)
                .ToList();

            return new Paged<dynamic>(
                data,
                this.skip.Apply(filter),
                this.limit.Apply(filter));
        }
    }
}
