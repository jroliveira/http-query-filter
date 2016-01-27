using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Restful.Query.Filter.Filters.Visualization;

namespace Restful.Query.Filter.Test.Filters
{
    [TestFixture]
    public class FieldsTests
    {
        [TestCase("?filter[fields][id]=true", new[] { "id" })]
        [TestCase("?FILTER[FIELDS][ID]=TRUE", new[] { "ID" })]
        [TestCase("?filter%5Bfields%5D%5Bid%5D=true", new[] { "id" })]
        [TestCase("?filter[fields][id]=true&filter[fields][name]=true", new[] { "id", "name" })]
        public void Parse_DadoQuery_DeveRetornarProperty(string query, IEnumerable<string> fieldsExpected)
        {
            Fields actual = query;

            var expected = new List<Field>(fieldsExpected.Select(name => new Field(name, true)));

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestCase("?filter[fields][id]=true", new[] { true })]
        [TestCase("?FILTER[FIELDS][id]=TRUE", new[] { true })]
        [TestCase("?filter%5Bfields%5D%5Bid%5D=true", new[] { true })]
        [TestCase("?filter[fields][id]=false", new[] { false })]
        [TestCase("?FILTER[FIELDS][id]=FALSE", new[] { false })]
        [TestCase("?filter%5Bfields%5D%5Bid%5D=false", new[] { false })]
        [TestCase("?filter[fields][id]=true&filter[fields][id]=false", new[] { true, false })]
        public void Parse_DadaQuery_DeveRetornarShow(string query, IEnumerable<bool> fieldsExpected)
        {
            Fields actual = query;

            var expected = new List<Field>(fieldsExpected.Select(show => new Field("id", show)));

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestCase("?filter[fields][id]=1")]
        [TestCase("?filter[fields][id]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Fields actual = query;

            actual.Should().BeNull();
        }
    }
}
