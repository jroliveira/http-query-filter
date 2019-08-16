namespace Http.Query.Filter.Client.Test
{
    using System;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Http.Query.Filter.Client.Filters.Condition;
    using Http.Query.Filter.Client.Test.Infrastructure;

    using Xunit;

    using static System.Threading.Tasks.Task;
    using static Http.Query.Filter.Client.Test.Infrastructure.Util;

    public class FilterTests
    {
        private readonly Func<string, Func<string, Task<Unit>>> done;

        public FilterTests() => this.done = expected => queryFilter =>
        {
            queryFilter.Should().Be(expected);
            return FromResult(Unit());
        };

        [Theory]
        [InlineData(2U, "filter[skip]=2")]
        public Task Skip_GivenFilter_ShouldReturn(uint skip, string expected) => new Filter<Unit>(this.done(expected))
            .Skip(skip)
            .Build();

        [Theory]
        [InlineData(100U, "filter[limit]=100")]
        [InlineData(0U, "filter[limit]=1")]
        public Task Limit_GivenFilter_ShouldReturn(uint limit, string expected) => new Filter<Unit>(this.done(expected))
            .Limit(limit)
            .Build();

        [Theory]
        [InlineData(new[] { "name", "id" }, "filter[fields][name]=true&filter[fields][id]=true")]
        [InlineData(new[] { "id" }, "filter[fields][id]=true")]
        public Task Select_GivenFilter_ShouldReturn(string[] fields, string expected) => new Filter<Unit>(this.done(expected))
            .Select(fields)
            .Build();

        [Fact]
        public Task Equal_GivenFilter_ShouldReturn() => new Filter<Unit>(this.done("filter[where][email]=junoliv.e@gmail.com"))
            .Where("email".Equal("junoliv.e@gmail.com"))
            .Build();

        [Fact]
        public Task Gt_GivenFilter_ShouldReturn() => new Filter<Unit>(this.done("filter[where][id][gt]=10"))
            .Where("id".GreaterThan(10))
            .Build();

        [Fact]
        public Task Lt_GivenFilter_ShouldReturn() => new Filter<Unit>(this.done("filter[where][id][lt]=10"))
            .Where("id".LessThan(10))
            .Build();

        [Fact(Skip = "in development")]
        public Task And_GivenFilter_ShouldReturn() => new Filter<Unit>(this.done("filter[where][and][0][id][gt]=10&filter[where][and][1][email]=junoliv.e@gmail.com"))
            .Where("id".GreaterThan(10)
                .And("email".Equal("junoliv.e@gmail.com")))
            .Build();

        [Fact(Skip = "in development")]
        public Task Or_GivenFilter_ShouldReturn() => new Filter<Unit>(this.done("filter[where][or][0][id][gt]=10&filter[where][or][1][email]=junoliv.e@gmail.com"))
            .Where("id".GreaterThan(10)
                .Or("email".Equal("junoliv.e@gmail.com")))
            .Build();

        [Fact(Skip = "in development")]
        public Task OrAnd_GivenFilter_ShouldReturn() => new Filter<Unit>(this.done("filter[where][or][0][id][gt]=9&filter[where][and][1][email]=junoliv.e@gmail.com&filter[where][and][2][id][gt]=10"))
            .Where("id".GreaterThan(9)
                .Or("email".Equal("junoliv.e@gmail.com")
                    .And("id".GreaterThan(10))))
            .Build();
    }
}
