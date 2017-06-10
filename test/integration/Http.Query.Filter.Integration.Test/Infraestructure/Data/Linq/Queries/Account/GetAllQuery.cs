namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Queries.Account
{
    using System.Linq;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Collections;
    using Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Extensions;
    using Http.Query.Filter.Integration.Test.Infraestructure.Filter;

    internal class GetAllQuery
    {
        private readonly Paged<object> paged;

        public GetAllQuery(
            ISkip<int, Filter> skip,
            ILimit<int, Filter> limit)
        {
            this.paged = new Paged<dynamic>(skip, limit);
        }

        public virtual Paged<dynamic> GetResult(Filter filter)
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
