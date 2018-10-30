namespace Http.Query.Filter.Client.Test
{
    using System;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Xunit;

    public class FilterTests
    {
        private readonly Func<string, Task<dynamic>> execute;
        private Func<string, dynamic> verify;

        public FilterTests() => this.execute = async queryFilter => await Task.Run(() => this.verify(queryFilter));

        [Theory]
        [InlineData(2U, "filter[skip]=2")]
        public async Task Skip_GivenFilter_ShouldReturn(uint skip, string expected)
        {
            this.verify = actual => actual.Should().Be(expected);

            await new Filter<dynamic>(this.execute)
                .Skip(skip)
                .BuildAsync();
        }

        [Theory]
        [InlineData(100U, "filter[limit]=100")]
        [InlineData(0U, "filter[limit]=1")]
        public async Task Limit_GivenFilter_ShouldReturn(uint limit, string expected)
        {
            this.verify = actual => actual.Should().Be(expected);

            await new Filter<dynamic>(this.execute)
                .Limit(limit)
                .BuildAsync();
        }

        [Theory]
        [InlineData(new[] { "name", "id" }, "filter[fields][name]=true&filter[fields][id]=true")]
        [InlineData(new[] { "id" }, "filter[fields][id]=true")]
        public async Task Select_GivenFilter_ShouldReturn(string[] fields, string expected)
        {
            this.verify = actual => actual.Should().Be(expected);

            await new Filter<dynamic>(this.execute)
                .Select(fields)
                .BuildAsync();
        }
    }
}
