namespace Http.Query.Filter.Test.Filters
{
    using FluentAssertions;

    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Condition.Operators;

    using Xunit;

    public class WhereTests
    {
        [Theory]
        [InlineData("?filter[where][id]=", "")]
        [InlineData("?filter[where][id]=2", "2")]
        [InlineData("?FILTER[WHERE][NAME]=TEST", "TEST")]
        [InlineData("?Filter[Where][Surname]=Unit Test", "Unit Test")]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D=2", "2")]
        [InlineData("?filter[where][id][gt]=", "")]
        [InlineData("?filter[where][id][gt]=2", "2")]
        [InlineData("?FILTER[WHERE][NAME][GT]=TEST", "TEST")]
        [InlineData("?Filter[Where][Surname][Gt]=Unit Test", "Unit Test")]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", "2")]
        [InlineData("?filter[where][id][lt]=", "")]
        [InlineData("?filter[where][id][lt]=2", "2")]
        [InlineData("?FILTER[WHERE][NAME][LT]=TEST", "TEST")]
        [InlineData("?Filter[Where][Surname][Lt]=Unit Test", "Unit Test")]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", "2")]
        [InlineData("?filter[where][and][0][id]=", "")]
        [InlineData("?filter[where][and][0][id]=2", "2")]
        [InlineData("?FILTER[WHERE][AND][1][NAME]=TEST", "TEST")]
        [InlineData("?Filter[Where][And][2][Surname]=Unit Test", "Unit Test")]
        [InlineData("?filter%5Bwhere%5D%5Band%5D%5B0%5D%5Bid%5D=2", "2")]
        [InlineData("?filter[where][or][0][id]=", "")]
        [InlineData("?filter[where][or][0][id]=2", "2")]
        [InlineData("?FILTER[WHERE][OR][1][NAME]=TEST", "TEST")]
        [InlineData("?Filter[Where][Or][2][Surname]=Unit Test", "Unit Test")]
        [InlineData("?filter%5Bwhere%5D%5Bor%5D%5B0%5D%5Bid%5D=2", "2")]
        public void Parse_DadoQuery_DeveRetornarPropertyValue(string query, object expected)
        {
            Where actual = query;

            foreach (var item in actual)
            {
                item.Value.Should().Be(expected);
            }
        }

        [Theory]
        [InlineData("?filter[where][id]=", "id")]
        [InlineData("?filter[where][id]=2", "id")]
        [InlineData("?FILTER[WHERE][NAME]=TEST", "NAME")]
        [InlineData("?Filter[Where][Surname]=Test", "Surname")]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D=2", "id")]
        [InlineData("?filter[where][id][gt]=", "id")]
        [InlineData("?filter[where][id][gt]=2", "id")]
        [InlineData("?FILTER[WHERE][NAME][GT]=TEST", "NAME")]
        [InlineData("?Filter[Where][Surname][Gt]=Test", "Surname")]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", "id")]
        [InlineData("?filter[where][id][lt]=", "id")]
        [InlineData("?filter[where][id][lt]=2", "id")]
        [InlineData("?FILTER[WHERE][NAME][LT]=TEST", "NAME")]
        [InlineData("?Filter[Where][Surname][Lt]=Test", "Surname")]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", "id")]
        [InlineData("?filter[where][and][0][id]=", "id")]
        [InlineData("?filter[where][and][0][id]=2", "id")]
        [InlineData("?FILTER[WHERE][AND][1][NAME]=TEST", "NAME")]
        [InlineData("?Filter[Where][And][2][Surname]=Unit Test", "Surname")]
        [InlineData("?filter%5Bwhere%5D%5Band%5D%5B0%5D%5Bid%5D=2", "id")]
        [InlineData("?filter[where][or][0][id]=", "id")]
        [InlineData("?filter[where][or][0][id]=2", "id")]
        [InlineData("?FILTER[WHERE][OR][1][NAME]=TEST", "NAME")]
        [InlineData("?Filter[Where][Or][2][Surname]=Unit Test", "Surname")]
        [InlineData("?filter%5Bwhere%5D%5Bor%5D%5B0%5D%5Bid%5D=2", "id")]
        public void Parse_DadoQuery_DeveRetornarPropertyName(string query, string expected)
        {
            Where actual = query;

            foreach (var item in actual)
            {
                item.Name.Should().Be(expected);
            }
        }

        [Theory]
        [InlineData("?filter[where][id]=", Comparison.Equal)]
        [InlineData("?filter[where][id]=2", Comparison.Equal)]
        [InlineData("?FILTER[WHERE][ID]=2", Comparison.Equal)]
        [InlineData("?Filter[Where][Id]=2", Comparison.Equal)]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D=2", Comparison.Equal)]
        [InlineData("?filter[where][id][gt]=", Comparison.GreaterThan)]
        [InlineData("?filter[where][id][gt]=2", Comparison.GreaterThan)]
        [InlineData("?FILTER[WHERE][ID][GT]=2", Comparison.GreaterThan)]
        [InlineData("?Filter[Where][Id][Gt]=2", Comparison.GreaterThan)]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", Comparison.GreaterThan)]
        [InlineData("?filter[where][id][lt]=", Comparison.LessThan)]
        [InlineData("?filter[where][id][lt]=2", Comparison.LessThan)]
        [InlineData("?FILTER[WHERE][ID][LT]=2", Comparison.LessThan)]
        [InlineData("?Filter[Where][Id][Lt]=2", Comparison.LessThan)]
        [InlineData("?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", Comparison.LessThan)]
        public void Parse_DadaQuery_DeveRetornarComparison(string query, Comparison expected)
        {
            Where actual = query;

            foreach (var item in actual)
            {
                item.Comparison.Should().Be(expected);
            }
        }

        [Theory]
        [InlineData("?filter[where][and][0][id]=", Logical.And)]
        [InlineData("?filter[where][and][0][id]=2", Logical.And)]
        [InlineData("?FILTER[WHERE][AND][1][NAME]=TEST", Logical.And)]
        [InlineData("?Filter[Where][And][2][Surname]=Unit Test", Logical.And)]
        [InlineData("?filter%5Bwhere%5D%5Band%5D%5B0%5D%5Bid%5D=2", Logical.And)]
        [InlineData("?filter[where][or][0][id]=", Logical.Or)]
        [InlineData("?filter[where][or][0][id]=2", Logical.Or)]
        [InlineData("?FILTER[WHERE][OR][1][NAME]=TEST", Logical.Or)]
        [InlineData("?Filter[Where][Or][2][Surname]=Unit Test", Logical.Or)]
        [InlineData("?filter%5Bwhere%5D%5Bor%5D%5B0%5D%5Bid%5D=2", Logical.Or)]
        public void Parse_DadaQuery_DeveRetornarLogical(string query, Logical expected)
        {
            Where actual = query;

            foreach (var item in actual)
            {
                item.Logical.Should().Be(expected);
            }
        }

        [Theory]
        [InlineData("?filter[condition][id]=1")]
        [InlineData("?filter[where][last name]=Test")]
        [InlineData("?filter[where][id][eq]=1")]
        [InlineData("?filter[where][id][equal]=1")]
        [InlineData("?filter[condition][id][gt]=1")]
        [InlineData("?filter[where][last name]=Test")]
        [InlineData("?filter[where][id][greaterthan]=1")]
        [InlineData("?filter[condition][id][lt]=1")]
        [InlineData("?filter[where][last name]=Test")]
        [InlineData("?filter[where][id][lessthan]=1")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Where actual = query;

            actual.Should().BeNull();
        }
    }
}