using FluentAssertions;
using NUnit.Framework;

namespace Restful.Query.Filter.Test
{
    [TestFixture]
    public class SkipTests
    {
        [TestCase("?filter[skip]=9", 9)]
        [TestCase("?FILTER[SKIP]=9", 9)]
        [TestCase("?filter%5Bskip%5D=1", 1)]
        public void Parse_DadaQuery_ValueDeveSerIgual(string query, int expected)
        {
            Skip actual = query;

            Assert.AreEqual(expected, actual);
        }

        [TestCase("?filter[skip]=Nine")]
        [TestCase("?filter[skip]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Skip actual = query;

            actual.Should().BeNull();
        }
    }
}