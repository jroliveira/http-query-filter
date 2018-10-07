namespace Http.Query.Filter.Test.Filters
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Test.Utils;
    using Xunit;
    using static Http.Query.Filter.Filters.Condition.Operators.Comparison;
    using static System.String;

    public class WhereTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void Parse_GivenQuery_ShouldReturn(string query, Where expected)
        {
            Where actual = query;

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[condition][id]=1")]
        [InlineData("?filter[where][last name]=Test")]
        [InlineData("?filter[where][id][eq]=1")]
        [InlineData("?filter[where][id][equal]=1")]
        [InlineData("?filter[condition][id][gt]=1")]
        [InlineData("?filter[where][id][greaterthan]=1")]
        [InlineData("?filter[condition][id][lt]=1")]
        [InlineData("?filter[where][id][lessthan]=1")]
        public void Parse_GivenQuery_ShouldReturnNull(string query)
        {
            Where actual = query;

            actual.Should().BeNull();
        }

        public class TestData : WhereTestData
        {
            protected override List<object[]> Data => new List<object[]>
            {
                // Equal to
                new object[] { "?filter[where][id]=",             Field("id",      Empty,     Equal) },
                new object[] { "?filter[where][id]=2",            Field("id",      "2",       Equal) },
                new object[] { "?FILTER[WHERE][NAME]=NAME",       Field("NAME",    "NAME",    Equal) },
                new object[] { "?Filter[Where][Surname]=Surname", Field("Surname", "Surname", Equal) },
                new object[] { "?filter%5Bwhere%5D%5Bid%5D=2",    Field("id",      "2",       Equal) },

                // Greater than
                new object[] { "?filter[where][id][gt]=",              Field("id", Empty, GreaterThan) },
                new object[] { "?filter[where][id][gt]=2",             Field("id", "2",   GreaterThan) },
                new object[] { "?FILTER[WHERE][ID][GT]=2",             Field("ID", "2",   GreaterThan) },
                new object[] { "?Filter[Where][Id][Gt]=2",             Field("Id", "2",   GreaterThan) },
                new object[] { "?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", Field("id", "2",   GreaterThan) },

                // Less than
                new object[] { "?filter[where][id][lt]=",              Field("id", Empty, LessThan) },
                new object[] { "?filter[where][id][lt]=2",             Field("id", "2",    LessThan) },
                new object[] { "?FILTER[WHERE][ID][LT]=2",             Field("ID", "2",    LessThan) },
                new object[] { "?Filter[Where][Id][Lt]=2",             Field("Id", "2",    LessThan) },
                new object[] { "?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", Field("id", "2",    LessThan) },
            };
        }
    }
}
