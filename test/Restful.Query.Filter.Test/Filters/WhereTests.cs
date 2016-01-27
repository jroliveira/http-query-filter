using FluentAssertions;
using NUnit.Framework;
using Restful.Query.Filter.Filters.Condition;
using Restful.Query.Filter.Filters.Condition.Operators;

namespace Restful.Query.Filter.Test.Filters
{
    [TestFixture]
    public class WhereTests
    {
        [TestCase("?filter[where][id]=", "")]
        [TestCase("?filter[where][id]=2", "2")]
        [TestCase("?FILTER[WHERE][NAME]=TEST", "TEST")]
        [TestCase("?Filter[Where][Surname]=Unit Test", "Unit Test")]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D=2", "2")]
        [TestCase("?filter[where][id][gt]=", "")]
        [TestCase("?filter[where][id][gt]=2", "2")]
        [TestCase("?FILTER[WHERE][NAME][GT]=TEST", "TEST")]
        [TestCase("?Filter[Where][Surname][Gt]=Unit Test", "Unit Test")]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", "2")]
        [TestCase("?filter[where][id][lt]=", "")]
        [TestCase("?filter[where][id][lt]=2", "2")]
        [TestCase("?FILTER[WHERE][NAME][LT]=TEST", "TEST")]
        [TestCase("?Filter[Where][Surname][Lt]=Unit Test", "Unit Test")]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", "2")]
        [TestCase("?filter[where][and][0][id]=", "")]
        [TestCase("?filter[where][and][0][id]=2", "2")]
        [TestCase("?FILTER[WHERE][AND][1][NAME]=TEST", "TEST")]
        [TestCase("?Filter[Where][And][2][Surname]=Unit Test", "Unit Test")]
        [TestCase("?filter%5Bwhere%5D%5Band%5D%5B0%5D%5Bid%5D=2", "2")]
        [TestCase("?filter[where][or][0][id]=", "")]
        [TestCase("?filter[where][or][0][id]=2", "2")]
        [TestCase("?FILTER[WHERE][OR][1][NAME]=TEST", "TEST")]
        [TestCase("?Filter[Where][Or][2][Surname]=Unit Test", "Unit Test")]
        [TestCase("?filter%5Bwhere%5D%5Bor%5D%5B0%5D%5Bid%5D=2", "2")]
        public void Parse_DadoQuery_DeveRetornarPropertyValue(string query, object expected)
        {
            Where actual = query;

            foreach (var item in actual)
            {
                item.Value.Should().Be(expected);
            }
        }

        [TestCase("?filter[where][id]=", "id")]
        [TestCase("?filter[where][id]=2", "id")]
        [TestCase("?FILTER[WHERE][NAME]=TEST", "NAME")]
        [TestCase("?Filter[Where][Surname]=Test", "Surname")]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D=2", "id")]
        [TestCase("?filter[where][id][gt]=", "id")]
        [TestCase("?filter[where][id][gt]=2", "id")]
        [TestCase("?FILTER[WHERE][NAME][GT]=TEST", "NAME")]
        [TestCase("?Filter[Where][Surname][Gt]=Test", "Surname")]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", "id")]
        [TestCase("?filter[where][id][lt]=", "id")]
        [TestCase("?filter[where][id][lt]=2", "id")]
        [TestCase("?FILTER[WHERE][NAME][LT]=TEST", "NAME")]
        [TestCase("?Filter[Where][Surname][Lt]=Test", "Surname")]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", "id")]
        [TestCase("?filter[where][and][0][id]=", "id")]
        [TestCase("?filter[where][and][0][id]=2", "id")]
        [TestCase("?FILTER[WHERE][AND][1][NAME]=TEST", "NAME")]
        [TestCase("?Filter[Where][And][2][Surname]=Unit Test", "Surname")]
        [TestCase("?filter%5Bwhere%5D%5Band%5D%5B0%5D%5Bid%5D=2", "id")]
        [TestCase("?filter[where][or][0][id]=", "id")]
        [TestCase("?filter[where][or][0][id]=2", "id")]
        [TestCase("?FILTER[WHERE][OR][1][NAME]=TEST", "NAME")]
        [TestCase("?Filter[Where][Or][2][Surname]=Unit Test", "Surname")]
        [TestCase("?filter%5Bwhere%5D%5Bor%5D%5B0%5D%5Bid%5D=2", "id")]
        public void Parse_DadoQuery_DeveRetornarPropertyName(string query, string expected)
        {
            Where actual = query;

            foreach (var item in actual)
            {
                item.Name.Should().Be(expected);
            }
        }

        [TestCase("?filter[where][id]=", Comparison.Equal)]
        [TestCase("?filter[where][id]=2", Comparison.Equal)]
        [TestCase("?FILTER[WHERE][ID]=2", Comparison.Equal)]
        [TestCase("?Filter[Where][Id]=2", Comparison.Equal)]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D=2", Comparison.Equal)]
        [TestCase("?filter[where][id][gt]=", Comparison.GreaterThan)]
        [TestCase("?filter[where][id][gt]=2", Comparison.GreaterThan)]
        [TestCase("?FILTER[WHERE][ID][GT]=2", Comparison.GreaterThan)]
        [TestCase("?Filter[Where][Id][Gt]=2", Comparison.GreaterThan)]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", Comparison.GreaterThan)]
        [TestCase("?filter[where][id][lt]=", Comparison.LessThan)]
        [TestCase("?filter[where][id][lt]=2", Comparison.LessThan)]
        [TestCase("?FILTER[WHERE][ID][LT]=2", Comparison.LessThan)]
        [TestCase("?Filter[Where][Id][Lt]=2", Comparison.LessThan)]
        [TestCase("?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", Comparison.LessThan)]
        public void Parse_DadaQuery_DeveRetornarComparison(string query, Comparison expected)
        {
            Where actual = query;

            foreach (var item in actual)
            {
                item.Comparison.Should().Be(expected);
            }
        }

        [TestCase("?filter[where][and][0][id]=", Logical.And)]
        [TestCase("?filter[where][and][0][id]=2", Logical.And)]
        [TestCase("?FILTER[WHERE][AND][1][NAME]=TEST", Logical.And)]
        [TestCase("?Filter[Where][And][2][Surname]=Unit Test", Logical.And)]
        [TestCase("?filter%5Bwhere%5D%5Band%5D%5B0%5D%5Bid%5D=2", Logical.And)]
        [TestCase("?filter[where][or][0][id]=", Logical.Or)]
        [TestCase("?filter[where][or][0][id]=2", Logical.Or)]
        [TestCase("?FILTER[WHERE][OR][1][NAME]=TEST", Logical.Or)]
        [TestCase("?Filter[Where][Or][2][Surname]=Unit Test", Logical.Or)]
        [TestCase("?filter%5Bwhere%5D%5Bor%5D%5B0%5D%5Bid%5D=2", Logical.Or)]
        public void Parse_DadaQuery_DeveRetornarLogical(string query, Logical expected)
        {
            Where actual = query;

            foreach (var item in actual)
            {
                item.Logical.Should().Be(expected);
            }
        }

        [TestCase("?filter[condition][id]=1")]
        [TestCase("?filter[where][last name]=Test")]
        [TestCase("?filter[where][id][eq]=1")]
        [TestCase("?filter[where][id][equal]=1")]
        [TestCase("?filter[condition][id][gt]=1")]
        [TestCase("?filter[where][last name]=Test")]
        [TestCase("?filter[where][id][greaterthan]=1")]
        [TestCase("?filter[condition][id][lt]=1")]
        [TestCase("?filter[where][last name]=Test")]
        [TestCase("?filter[where][id][lessthan]=1")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Where actual = query;

            actual.Should().BeNull();
        }
    }
}