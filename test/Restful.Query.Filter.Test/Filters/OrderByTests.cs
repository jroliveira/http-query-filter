using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Restful.Query.Filter.Filters.Ordering;

namespace Restful.Query.Filter.Test.Filters
{
    [TestFixture]
    public class OrderByTests
    {
        [TestCase("?filter[order]=id asc", new[] { "id" })]
        [TestCase("?FILTER[ORDER]=ID ASC", new[] { "ID" })]
        [TestCase("?filter%5Border%5D=id%20asc", new[] { "id" })]
        [TestCase("?filter[order][0]=id asc&filter[order][1]=name asc", new[] { "id", "name" })]
        public void Parse_DadoQuery_DeveRetornarProperty(string query, IEnumerable<string> fieldsExpected)
        {
            OrderBy actual = query;

            var expected = new List<Field>(fieldsExpected.Select(name => new Field(name, OrderByDirection.Ascending)));

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestCase("?filter[order]=id asc", new[] { OrderByDirection.Ascending })]
        [TestCase("?FILTER[ORDER]=id ASC", new[] { OrderByDirection.Ascending })]
        [TestCase("?filter%5Border%5D=id%20asc", new[] { OrderByDirection.Ascending })]
        [TestCase("?filter[order]=id desc", new[] { OrderByDirection.Descending })]
        [TestCase("?FILTER[ORDER]=id DESC", new[] { OrderByDirection.Descending })]
        [TestCase("?filter%5Border%5D=id%20desc", new[] { OrderByDirection.Descending })]
        [TestCase("?filter[order][0]=id asc&filter[order][1]=id desc", new[] { OrderByDirection.Ascending, OrderByDirection.Descending })]
        public void Parse_DadaQuery_DeveRetornarDirection(string query, IEnumerable<OrderByDirection> fieldsExpected)
        {
            OrderBy actual = query;

            var expected = new List<Field>(fieldsExpected.Select(direction => new Field("id", direction)));

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestCase("?filter[order]=name%20des")]
        [TestCase("?filter[order]=last name asc")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            OrderBy actual = query;

            actual.Should().BeNull();
        }
    }
}
