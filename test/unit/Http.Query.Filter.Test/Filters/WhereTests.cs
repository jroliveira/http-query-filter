namespace Http.Query.Filter.Test.Filters
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Condition.Operators;
    using Http.Query.Filter.Test.Utils;

    using Xunit;

    public class WhereTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void Parse_DadaQuery_DeveRetornarPropertyValue(string query, Where expected)
        {
            Where actual = query;

            actual.ShouldAllBeEquivalentTo(expected);
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

        public class TestData : WhereTestData
        {
            protected override List<object[]> Data => new List<object[]>
            {
                // Equal to
                new object[] { "?filter[where][id]=",             Field("id",      string.Empty, Comparison.Equal) },
                new object[] { "?filter[where][id]=2",            Field("id",      2,            Comparison.Equal) },
                new object[] { "?FILTER[WHERE][NAME]=NAME",       Field("NAME",    "NAME",       Comparison.Equal) },
                new object[] { "?Filter[Where][Surname]=Surname", Field("Surname", "Surname",    Comparison.Equal) },
                new object[] { "?filter%5Bwhere%5D%5Bid%5D=2",    Field("id",      2,            Comparison.Equal) },

                // Greater than
                new object[] { "?filter[where][id][gt]=",              Field("id", string.Empty, Comparison.GreaterThan) },
                new object[] { "?filter[where][id][gt]=2",             Field("id", 2,            Comparison.GreaterThan) },
                new object[] { "?FILTER[WHERE][ID][GT]=2",             Field("ID", 2,            Comparison.GreaterThan) },
                new object[] { "?Filter[Where][Id][Gt]=2",             Field("Id", 2,            Comparison.GreaterThan) },
                new object[] { "?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", Field("id", 2,            Comparison.GreaterThan) },

                // Less than
                new object[] { "?filter[where][id][lt]=",              Field("id", string.Empty, Comparison.LessThan) },
                new object[] { "?filter[where][id][lt]=2",             Field("id", 2,            Comparison.LessThan) },
                new object[] { "?FILTER[WHERE][ID][LT]=2",             Field("ID", 2,            Comparison.LessThan) },
                new object[] { "?Filter[Where][Id][Lt]=2",             Field("Id", 2,            Comparison.LessThan) },
                new object[] { "?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", Field("id", 2,            Comparison.LessThan) },
            };
        }
    }
}