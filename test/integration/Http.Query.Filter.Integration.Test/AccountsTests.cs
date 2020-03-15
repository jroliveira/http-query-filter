namespace Http.Query.Filter.Integration.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Http.Query.Filter.Client.Filters.Condition;
    using Http.Query.Filter.Integration.Test.Entities;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Collections;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Queries.Account;

    using Xunit;

    using static Http.Query.Filter.Client.FilterBuilder;

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

            this.getAll
                .GetResult(NewFilterBuilder()
                    .Skip(2)
                    .Build())
                .Data
                .Should()
                .BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterLimit_ShouldReturn()
        {
            var expected = this.accounts.Take(5);

            var actual = this.getAll
                .GetResult(NewFilterBuilder()
                    .Limit(5)
                    .Build())
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereEqualTo_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Password == "333333");

            var actual = this.getAll
                .GetResult(NewFilterBuilder()
                    .Where("password".Equal("333333"))
                    .Build())
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereGreaterThan_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id < 2);

            var actual = this.getAll
                .GetResult(NewFilterBuilder()
                    .Where("id".LessThan(2))
                    .Build())
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereLessThan_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id > 5);

            var actual = this.getAll
                .GetResult(NewFilterBuilder()
                    .Where("id".GreaterThan(5))
                    .Build())
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereAnd_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id > 10 && item.Email == "junoliv.e@gmail.com");

            var actual = this.getAll
                .GetResult(NewFilterBuilder()
                    .Where("id".GreaterThan(10)
                        .And("email".Equal("junoliv.e@gmail.com")))
                    .Build())
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetAll_GivenQueryWithFilterWhereOr_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id > 10 || item.Email == "junoliv.e@gmail.com");

            var actual = this.getAll
                .GetResult(NewFilterBuilder()
                    .Where("id".GreaterThan(10)
                        .Or("email".Equal("junoliv.e@gmail.com")))
                    .Build())
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(Skip = "in development")]
        public void GetAll_GivenQueryWithFilterWhereOrAnd_ShouldReturn()
        {
            var expected = this.accounts.Where(item => item.Id > 9 || (item.Email == "junoliv.e@gmail.com" && item.Id > 10));

            var actual = this.getAll
                .GetResult(NewFilterBuilder()
                    .Where("id".GreaterThan(9)
                        .Or("email".Equal("junoliv.e@gmail.com")
                            .And("id".GreaterThan(10))))
                    .Build())
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
                .GetResult(NewFilterBuilder()
                    .Select("id", "email")
                    .Build())
                .Data;

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
