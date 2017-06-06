namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Queries.Account
{
    using System.Linq;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Entities;
    using Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Collections;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;

    internal class GetAllQuery
    {
        private readonly ISkip<int, Filter> skip;
        private readonly ILimit<int, Filter> limit;
        private readonly IWhere<bool, Filter, Account> where;

        public GetAllQuery(
            ISkip<int, Filter> skip,
            ILimit<int, Filter> limit,
            IWhere<bool, Filter, Account> where)
        {
            this.skip = skip;
            this.limit = limit;
            this.where = where;
        }

        public virtual Paged<Account> GetResult(Filter filter)
        {
            var skip = this.skip.Apply(filter);
            var limit = this.limit.Apply(filter);

            var data = Accounts
                .Data
                .Skip(skip)
                .Take(limit)
                .Where(transaction => this.where.Apply(filter, transaction))
                .ToList();

            return new Paged<Account>(data, skip, limit);
        }
    }
}
