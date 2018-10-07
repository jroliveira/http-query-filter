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
        public void Parse_GivenQuery_ValueShouldBe(string query, uint expected)
        {
            Limit actual = query;

            actual.Value.Should().Be(expected);
        }

        [Theory]
        [InlineData("?filter[limit]=Nine")]
        [InlineData("?filter[limit]=")]
        public void Parse_GivenQuery_ShouldReturnNull(string query)
        {
            Limit actual = query;

            actual.Value.Should().BeNull();
        }
    }
}
