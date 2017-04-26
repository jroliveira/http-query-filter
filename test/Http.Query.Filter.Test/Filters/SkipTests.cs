namespace Http.Query.Filter.Test.Filters
{
    using FluentAssertions;

    using Http.Query.Filter.Filters;

    using Xunit;

    public class SkipTests
    {
        [Theory]
        [InlineData("?filter[skip]=9", 9)]
        [InlineData("?FILTER[SKIP]=9", 9)]
        [InlineData("?filter%5Bskip%5D=1", 1)]
        public void Parse_DadaQuery_ValueDeveSerIgual(string query, int expected)
        {
            Skip actual = query;

            actual.Value.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[skip]=Nine")]
        [InlineData("?filter[skip]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Skip actual = query;

            actual.Should().BeNull();
        }
    }
}