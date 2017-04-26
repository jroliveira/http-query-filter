namespace Http.Query.Filter.Test.Filters
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Http.Query.Filter.Filters.Ordering;

    using Xunit;

    public class OrderByTests
    {
        [Theory]
        [InlineData("?filter[order]=id asc", new[] { "id" })]
        [InlineData("?FILTER[ORDER]=ID ASC", new[] { "ID" })]
        [InlineData("?filter%5Border%5D=id%20asc", new[] { "id" })]
        [InlineData("?filter[order][0]=id asc&filter[order][1]=name asc", new[] { "id", "name" })]
        public void Parse_DadoQuery_DeveRetornarProperty(string query, IEnumerable<string> fieldsExpected)
        {
            OrderBy actual = query;

            var expected = new List<Field>(fieldsExpected.Select(name => new Field(name, OrderByDirection.Ascending)));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[order]=id asc", new[] { OrderByDirection.Ascending })]
        [InlineData("?FILTER[ORDER]=id ASC", new[] { OrderByDirection.Ascending })]
        [InlineData("?filter%5Border%5D=id%20asc", new[] { OrderByDirection.Ascending })]
        [InlineData("?filter[order]=id desc", new[] { OrderByDirection.Descending })]
        [InlineData("?FILTER[ORDER]=id DESC", new[] { OrderByDirection.Descending })]
        [InlineData("?filter%5Border%5D=id%20desc", new[] { OrderByDirection.Descending })]
        [InlineData("?filter[order][0]=id asc&filter[order][1]=id desc", new[] { OrderByDirection.Ascending, OrderByDirection.Descending })]
        public void Parse_DadaQuery_DeveRetornarDirection(string query, IEnumerable<OrderByDirection> fieldsExpected)
        {
            OrderBy actual = query;

            var expected = new List<Field>(fieldsExpected.Select(direction => new Field("id", direction)));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[order]=name%20des")]
        [InlineData("?filter[order]=last name asc")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            OrderBy actual = query;

            actual.Should().BeNull();
        }
    }
}
