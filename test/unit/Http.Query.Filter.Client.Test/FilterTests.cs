namespace Http.Query.Filter.Client.Test
{
    using System;
    using System.Threading.Tasks;

    using FluentAssertions;

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
    }
}
