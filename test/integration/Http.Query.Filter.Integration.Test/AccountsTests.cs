namespace Http.Query.Filter.Integration.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Http.Query.Filter.Integration.Test.Entities;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Collections;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Queries.Account;

    using Xunit;

    public class AccountsTests
    {
        private readonly GetAllQuery getAll;
        private readonly IEnumerable<Account> accounts;

        public AccountsTests()
        {
            this.accounts = new Accounts();
            this.getAll = new GetAllQuery(default(Skip), default(Limit));
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterSkip_ShouldReturn()
        {
            var expected = this.accounts.Skip(2);

            var actual = this.getAll
                .GetResult("filter[skip]=2")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterLimit_ShouldReturn()
        {
            var expected = this.accounts.Take(5);

            var actual = this.getAll
                .GetResult("filter[limit]=5")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereEqualTo_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Password == "333333");

            var actual = this.getAll
                .GetResult("filter[where][password]=333333")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereGreaterThan_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id < 2);

            var actual = this.getAll
                .GetResult("filter[where][id][lt]=2")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereLessThan_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id > 5);

            var actual = this.getAll
                .GetResult("filter[where][id][gt]=5")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereAnd_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id > 10 && item.Email == "junoliv.e@gmail.com");

            var actual = this.getAll
                .GetResult("filter[where][and][0][id][gt]=10&filter[where][and][1][email]=junoliv.e@gmail.com")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereOr_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id > 10 || item.Email == "junoliv.e@gmail.com");

            var actual = this.getAll
                .GetResult("filter[where][or][0][id][gt]=10&filter[where][or][1][email]=junoliv.e@gmail.com")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(Skip = "in development")]
        public void GetAll_GivenQueryWithFilterWhereOrAnd_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id > 9 || (item.Email == "junoliv.e@gmail.com" && item.Id > 10));

            var actual = this.getAll
                .GetResult("filter[where][or][0][id][gt]=9&filter[where][and][1][email]=junoliv.e@gmail.com&filter[where][and][2][id][gt]=10")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterSelect_ShouldReturn()
        {
            var expected = this.accounts
                .Select(account => new Dictionary<string, object>
                {
                    { "id", account.Id },
                    { "email", account.Email },
                })
                .ToList();

            var actual = this.getAll
                .GetResult("filter[fields][id]=true&filter[fields][email]=true")
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
