using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Restful.Query.Filter.Order;

namespace Restful.Query.Filter.Test
{
    [TestFixture]
    public class OrderTests
    {
        [TestCase("?filter[order]=id asc", new[] { "id" })]
        [TestCase("?FILTER[ORDER]=ID ASC", new[] { "ID" })]
        [TestCase("?filter%5Border%5D=id%20asc", new[] { "id" })]
        [TestCase("?filter[order][0]=id asc&filter[order][1]=name asc", new[] { "id", "name" })]
        public void Parse_DadoQuery_DeveRetornarProperty(string query, IEnumerable<string> fieldsExpected)
        {
            Order.Order actual = query;

            var expected = new List<Field>(fieldsExpected.Select(name => new Field(name, Sorts.Asc)));

            actual.Fields.ShouldBeEquivalentTo(expected);
        }

        [TestCase("?filter[order]=id asc", new[] { Sorts.Asc })]
        [TestCase("?FILTER[ORDER]=id ASC", new[] { Sorts.Asc })]
        [TestCase("?filter%5Border%5D=id%20asc", new[] { Sorts.Asc })]
        [TestCase("?filter[order]=id desc", new[] { Sorts.Desc })]
        [TestCase("?FILTER[ORDER]=id DESC", new[] { Sorts.Desc })]
        [TestCase("?filter%5Border%5D=id%20desc", new[] { Sorts.Desc })]
        [TestCase("?filter[order][0]=id asc&filter[order][1]=id desc", new[] { Sorts.Asc, Sorts.Desc })]
        public void Parse_DadaQuery_DeveRetornarSorts(string query, IEnumerable<Sorts> fieldsExpected)
        {
            Order.Order actual = query;

            var expected = new List<Field>(fieldsExpected.Select(sorts => new Field("id", sorts)));

            actual.Fields.ShouldBeEquivalentTo(expected);
        }

        [TestCase("?filter[order]=name%20des")]
        [TestCase("?filter[order]=last name asc")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Order.Order actual = query;

            actual.Should().BeNull();
        }
    }
}
