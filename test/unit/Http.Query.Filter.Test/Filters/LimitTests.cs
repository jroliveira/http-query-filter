namespace Http.Query.Filter.Test.Filters
{
    using FluentAssertions;

    using Http.Query.Filter.Filters;

    using Xunit;

    public class LimitTests
    {
        [Theory]
        [InlineData("?filter[limit]=9", 9)]
        [InlineData("?FILTER[LIMIT]=9", 9)]
        [InlineData("?filter%5Blimit%5D=9", 9)]
        public void Parse_DadaQuery_ValueDeveSerIgual(string query, int expected)
        {
            Limit actual = query;

            actual.Value.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[limit]=Nine")]
        [InlineData("?filter[limit]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Limit actual = query;

            actual.Should().BeNull();
        }
    }
}
