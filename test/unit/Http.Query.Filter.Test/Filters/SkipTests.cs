namespace Http.Query.Filter.Test.Filters
{
    using FluentAssertions;
    using Http.Query.Filter.Filters.Pagination;
    using Xunit;

    public class SkipTests
    {
        [Theory]
        [InlineData("?filter[skip]=9", 9U)]
        [InlineData("?FILTER[SKIP]=9", 9U)]
        [InlineData("?filter%5Bskip%5D=1", 1U)]
        public void Parse_GivenQuery_ValueShouldBe(string query, uint expected)
        {
            Skip actual = query;

            actual.Value.Should().Be(expected);
        }

        [Theory]
        [InlineData("?filter[skip]=Nine")]
        [InlineData("?filter[skip]=")]
        public void Parse_GivenQuery_ShouldReturnNull(string query)
        {
            Skip actual = query;

            actual.Value.Should().BeNull();
        }
    }
}
