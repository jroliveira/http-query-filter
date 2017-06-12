namespace Http.Query.Filter.Client.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Xunit;

    public class FilterTests
    {
        private readonly Func<string, Task<dynamic>> execute;
        private Func<string, dynamic> verify;

        public FilterTests()
        {
            this.execute = async queryFilter => await Task.Run(() => this.verify(queryFilter));
        }

        [Theory]
        [InlineData(2, "filter[skip]=2")]
        [InlineData(-1, "filter[skip]=0")]
        public async Task Skip_GivenFilter_ShouldReturn(int skip, string expected)
        {
            // Assert
            this.verify = actual => actual.Should().Be(expected);

            // Act
            await new Filter<dynamic>(this.execute)
                .Skip(skip)
                .BuildAsync();
        }

        [Theory]
        [InlineData(100, "filter[limit]=100")]
        [InlineData(0, "filter[limit]=1")]
        [InlineData(-1, "filter[limit]=1")]
        public async Task Limit_GivenFilter_ShouldReturn(int limit, string expected)
        {
            // Assert
            this.verify = actual => actual.Should().Be(expected);

            // Act
            await new Filter<dynamic>(this.execute)
                .Limit(limit)
                .BuildAsync();
        }

        [Theory]
        [InlineData(new[] { "name", "id" }, "filter[fields][name]=true&filter[fields][id]=true")]
        [InlineData(new[] { "id" }, "filter[fields][id]=true")]
        public async Task Select_GivenFilter_ShouldReturn(string[] fields, string expected)
        {
            // Assert
            this.verify = actual => actual.Should().Be(expected);

            // Act
            await new Filter<dynamic>(this.execute)
                .Select(fields)
                .BuildAsync();
        }
    }
}
