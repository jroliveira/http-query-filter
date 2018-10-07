namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Queries.Account
{
    using System.Linq;
    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Collections;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Extensions;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;

    internal class GetAllQuery
    {
        private readonly Paged<object> paged;

        internal GetAllQuery(ISkip<uint, Filter> skip, ILimit<uint, Filter> limit) => this.paged = new Paged<dynamic>(skip, limit);

        internal virtual Paged<dynamic> GetResult(Filter filter)
        {
            var data = Accounts
                .Data
                .Skip(filter)
                .Limit(filter)
                .Where(filter)
                .Select(filter)
                .ToList();

            this.paged
                .Paginate(filter)
                .AddRange(data);

            return this.paged;
        }
    }
}
