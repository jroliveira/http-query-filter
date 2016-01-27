using FluentAssertions;
using NUnit.Framework;
using Restful.Query.Filter.Filters;

namespace Restful.Query.Filter.Test.Filters
{
    [TestFixture]
    public class LimitTests
    {
        [TestCase("?filter[limit]=9", 9)]
        [TestCase("?FILTER[LIMIT]=9", 9)]
        [TestCase("?filter%5Blimit%5D=9", 9)]
        public void Parse_DadaQuery_ValueDeveSerIgual(string query, int expected)
        {
            Limit actual = query;

            Assert.AreEqual(expected, actual);
        }

        [TestCase("?filter[limit]=Nine")]
        [TestCase("?filter[limit]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Limit actual = query;

            actual.Should().BeNull();
        }
    }
}
