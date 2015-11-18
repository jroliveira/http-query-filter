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
        [TestCase("?FILTER[ORDER]=NAME%20ASC", new[] { "NAME" })]
        [TestCase("?Filter[Order]=Surname Asc", new[] { "Surname" })]
        [TestCase("?filter%5Border%5D=id%20asc", new[] { "id" })]
        [TestCase("?Filter[order]=id asc", new[] { "id" })]
        [TestCase("?filter[order][0]=id asc&filter[order][1]=name asc", new[] { "id", "name" })]
        public void Parse_DadoQuery_DeveRetornarProperty(string query, IEnumerable<string> expected)
        {
            Order.Order order = query;

            var fields = new List<Field>();

            fields.AddRange(expected.Select(name => new Field { Name = name, Sorts = Sorts.Asc }));

            order.Fields.ShouldBeEquivalentTo(fields);
        }

        [TestCase("?filter[order]=id%20ASC", new[] { Sorts.Asc })]
        [TestCase("?filter[order]=id asc", new[] { Sorts.Asc })]
        [TestCase("?filter[order]=id%20Asc", new[] { Sorts.Asc })]
        [TestCase("?filter[order]=id DESC", new[] { Sorts.Desc })]
        [TestCase("?filter[order]=id%20desc", new[] { Sorts.Desc })]
        [TestCase("?filter[order]=id Desc", new[] { Sorts.Desc })]
        [TestCase("?filter%5Border%5D=id%20desc", new[] { Sorts.Desc })]
        [TestCase("?filter[order]=id ASC", new[] { Sorts.Asc })]
        [TestCase("?filter[order][0]=id asc&filter[order][1]=id desc", new[] { Sorts.Asc, Sorts.Desc })]
        public void Parse_DadaQuery_DeveRetornarSorts(string query, IEnumerable<Sorts> expected)
        {
            Order.Order order = query;

            var fields = new List<Field>();

            fields.AddRange(expected.Select(sorts => new Field { Name = "id", Sorts = sorts }));

            order.Fields.ShouldBeEquivalentTo(fields);
        }

        [TestCase("?filter[order]=Name%20DES")]
        [TestCase("?filter[order]=last name%20ASC")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Order.Order order = query;

            order.Should().BeNull();
        }
    }
}
