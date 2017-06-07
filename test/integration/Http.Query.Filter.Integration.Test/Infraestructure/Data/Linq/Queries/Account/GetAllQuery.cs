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
        private readonly ISelect<Filter, Account> select;

        public GetAllQuery(
            ISkip<int, Filter> skip,
            ILimit<int, Filter> limit,
            IWhere<bool, Filter, Account> where,
            ISelect<Filter, Account> select)
        {
            this.skip = skip;
            this.limit = limit;
            this.where = where;
            this.select = select;
        }

        public virtual Paged<dynamic> GetResult(Filter filter)
        {
            var skip = this.skip.Apply(filter);
            var limit = this.limit.Apply(filter);
            var where = this.where.Apply(filter);
            var select = this.select.Apply(filter);

            var data = Accounts
                .Data
                .Skip(skip)
                .Take(limit)
                .Where(where)
                .Select(select)
                .ToList();

            return new Paged<dynamic>(data, skip, limit);
        }
    }
}
