namespace Http.Query.Filter.Test.Filters
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Http.Query.Filter.Filters.Visualization;

    using Xunit;

    public class FieldsTests
    {
        [Theory]
        [InlineData("?filter[fields][id]=true", new[] { "id" })]
        [InlineData("?FILTER[FIELDS][ID]=TRUE", new[] { "ID" })]
        [InlineData("?filter%5Bfields%5D%5Bid%5D=true", new[] { "id" })]
        [InlineData("?filter[fields][id]=true&filter[fields][name]=true", new[] { "id", "name" })]
        public void Parse_DadoQuery_DeveRetornarProperty(string query, IEnumerable<string> fieldsExpected)
        {
            Fields actual = query;

            var expected = new List<Field>(fieldsExpected.Select(name => new Field(name, true)));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[fields][id]=true", new[] { true })]
        [InlineData("?FILTER[FIELDS][id]=TRUE", new[] { true })]
        [InlineData("?filter%5Bfields%5D%5Bid%5D=true", new[] { true })]
        [InlineData("?filter[fields][id]=false", new[] { false })]
        [InlineData("?FILTER[FIELDS][id]=FALSE", new[] { false })]
        [InlineData("?filter%5Bfields%5D%5Bid%5D=false", new[] { false })]
        [InlineData("?filter[fields][id]=true&filter[fields][id]=false", new[] { true, false })]
        public void Parse_DadaQuery_DeveRetornarShow(string query, IEnumerable<bool> fieldsExpected)
        {
            Fields actual = query;

            var expected = new List<Field>(fieldsExpected.Select(show => new Field("id", show)));

            actual.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[fields][id]=1")]
        [InlineData("?filter[fields][id]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Fields actual = query;

            actual.Should().BeNull();
        }
    }
}
