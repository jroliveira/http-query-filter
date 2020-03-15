namespace Http.Query.Filter.Test.Filters
{
    using FluentAssertions;

    using Http.Query.Filter.Filters.Pagination;

    using Xunit;

    public class LimitTests
    {
        [Theory]
        [InlineData("?filter[limit]=9", 9U)]
        [InlineData("?FILTER[LIMIT]=9", 9U)]
        [InlineData("?filter%5Blimit%5D=9", 9U)]
        [InlineData("?limit=9", 9U)]
        [InlineData("?LIMIT=9", 9U)]
        public void Parse_GivenQuery_ValueShouldBe(string query, uint? expected)
        {
            Limit limit = query;

            uint? actual = limit;

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("?filter[limit]=Nine")]
        [InlineData("?filter[limit]=")]
        [InlineData("?limit=Nine")]
        [InlineData("?limit=")]
        [InlineData("")]
        [InlineData(default)]
        public void Parse_GivenQuery_ShouldReturnNull(string query)
        {
            Limit limit = query;

            uint? actual = limit;

            actual.Should().BeNull();
        }
    }
}
