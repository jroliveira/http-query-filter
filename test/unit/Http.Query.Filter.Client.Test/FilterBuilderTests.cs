namespace Http.Query.Filter.Client.Test
{
    using System;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Http.Query.Filter.Client.Filters.Condition;

    using Xunit;

    using static System.Threading.Tasks.Task;

    using static Http.Query.Filter.Client.FilterBuilder;

    public class FilterBuilderTests
    {
        private readonly Func<string, Func<string, Task>> done;

        public FilterBuilderTests() => this.done = expected => queryFilter =>
        {
            queryFilter.Should().Be(expected);
            return CompletedTask;
        };

        [Theory]
        [InlineData(2U, "filter[skip]=2")]
        public Task Skip_GivenFilter_ShouldReturn(uint skip, string expected) => NewFilterBuilder()
            .Skip(skip)
            .Build(this.done(expected));

        [Theory]
        [InlineData(100U, "filter[limit]=100")]
        [InlineData(0U, "filter[limit]=1")]
        public Task Limit_GivenFilter_ShouldReturn(uint limit, string expected) => NewFilterBuilder()
            .Limit(limit)
            .Build(this.done(expected));

        [Theory]
        [InlineData(new[] { "name", "id" }, "filter[fields][name]=true&filter[fields][id]=true")]
        [InlineData(new[] { "id" }, "filter[fields][id]=true")]
        public Task Select_GivenFilter_ShouldReturn(string[] fields, string expected) => NewFilterBuilder()
            .Select(fields)
            .Build(this.done(expected));

        [Fact]
        public Task Equal_GivenFilter_ShouldReturn() => NewFilterBuilder()
            .Where("email".Equal("junoliv.e@gmail.com"))
            .Build(this.done("filter[where][email]=junoliv.e@gmail.com"));

        [Fact]
        public Task Gt_GivenFilter_ShouldReturn() => NewFilterBuilder()
            .Where("id".GreaterThan(10))
            .Build(this.done("filter[where][id][gt]=10"));

        [Fact]
        public Task Lt_GivenFilter_ShouldReturn() => NewFilterBuilder()
            .Where("id".LessThan(10))
            .Build(this.done("filter[where][id][lt]=10"));

        [Fact]
        public Task Inq_GivenFilter_ShouldReturn() => NewFilterBuilder()
            .Where("id".Inq(10, 11))
            .Build(this.done("filter[where][id][inq]=10&filter[where][id][inq]=11"));

        [Fact]
        public Task And_GivenFilter_ShouldReturn() => NewFilterBuilder()
            .Where("id".GreaterThan(10)
                .And("email".Equal("junoliv.e@gmail.com")))
            .Build(this.done("filter[where][and][id][gt]=10&filter[where][and][email]=junoliv.e@gmail.com"));

        [Fact]
        public Task Or_GivenFilter_ShouldReturn() => NewFilterBuilder()
            .Where("id".GreaterThan(10)
                .Or("email".Equal("junoliv.e@gmail.com")))
            .Build(this.done("filter[where][or][id][gt]=10&filter[where][or][email]=junoliv.e@gmail.com"));

        [Fact]
        public Task OrAnd_GivenFilter_ShouldReturn() => NewFilterBuilder()
            .Where("id".GreaterThan(9)
                .Or("email".Equal("junoliv.e@gmail.com")
                    .And("id".GreaterThan(10))))
            .Build(this.done("filter[where][or][id][gt]=9&filter[where][and][email]=junoliv.e@gmail.com&filter[where][and][id][gt]=10"));
    }
}
