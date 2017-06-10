namespace Http.Query.Filter.Integration.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Http.Query.Filter.Integration.Test.Entities;
    using Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Collections;
    using Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Filter;
    using Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Queries.Account;

    using Xunit;

    public class AccountsTests
    {
        private readonly GetAllQuery getAll;
        private readonly IEnumerable<Account> accounts;

        public AccountsTests()
        {
            this.accounts = new Accounts();
            this.getAll = new GetAllQuery(new Skip(), new Limit());
        }

        [Fact]
        public void GetAll_DadaQueryComFilterWhereEqualTo_DeveRetornar()
        {
            var expected = this.accounts.Where(item => item.Password == "333333");

            var actual = this.getAll
                .GetResult("filter[where][password]=333333")
                .Data;

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_DadaQueryComFilterSkip_DeveRetornar()
        {
            var expected = this.accounts.Skip(2);

            var actual = this.getAll
                .GetResult("filter[skip]=2")
                .Data;

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_DadaQueryComFilterLimit_DeveRetornar()
        {
            var expected = this.accounts.Take(5);

            var actual = this.getAll
                .GetResult("filter[limit]=5")
                .Data;

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_DadaQueryComFilterWhereGreaterThan_DeveRetornar()
        {
            var expected = this.accounts.Where(item => item.Id < 2);

            var actual = this.getAll
                .GetResult("filter[where][id][lt]=2")
                .Data;

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_DadaQueryComFilterWhereLessThan_DeveRetornar()
        {
            var expected = this.accounts.Where(item => item.Id > 5);

            var actual = this.getAll
                .GetResult("filter[where][id][gt]=5")
                .Data;

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_DadaQueryComFilterSelect_DeveRetornar()
        {
            var expected = this.accounts
                .Select(account => new Dictionary<string, object> { { "id", account.Id }, { "email", account.Email } })
                .ToList();

            var actual = this.getAll
                .GetResult("filter[fields][id]=true&filter[fields][email]=true")
                .Data;

            actual.ShouldAllBeEquivalentTo(expected);
        }
    }
}
