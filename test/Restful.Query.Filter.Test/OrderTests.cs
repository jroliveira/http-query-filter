using FluentAssertions;
using NUnit.Framework;
using Restful.Query.Filter.Order;

namespace Restful.Query.Filter.Test
{
    [TestFixture]
    public class OrderTests
    {
        [TestCase("?filter[order]=id%20asc", "id")]
        [TestCase("?FILTER[ORDER]=NAME%20DESC", "NAME")]
        [TestCase("?Filter[Order]=Surname%20Asc", "Surname")]
        [TestCase("?filter%5Border%5D=id%20desc", "id")]
        public void Parse_DadoQuery_DeveRetornarProperty(string query, string expected)
        {
            Order.Order order = query;

            order.Property.Should().Be(expected);
        }

        [TestCase("?filter[order]=Id%20ASC", Sorts.Asc)]
        [TestCase("?filter[order]=Id%20asc", Sorts.Asc)]
        [TestCase("?filter[order]=Id%20Asc", Sorts.Asc)]
        [TestCase("?filter[order]=Id%20DESC", Sorts.Desc)]
        [TestCase("?filter[order]=Id%20desc", Sorts.Desc)]
        [TestCase("?filter[order]=Id%20Desc", Sorts.Desc)]
        [TestCase("?filter%5Border%5D=id%20desc", Sorts.Desc)]
        [TestCase("?filter[order]=id ASC", Sorts.Asc)]
        public void Parse_DadaQuery_DeveRetornarSorts(string query, Sorts expected)
        {
            Order.Order order = query;

            order.Sorts.Should().Be(expected);
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
