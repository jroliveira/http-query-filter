namespace Http.Query.Filter.Test.Filters
{
    using System.Collections;
    using System.Collections.Generic;

    using FluentAssertions;

    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Condition.Operators;

    using Xunit;

    using static System.String;
    using static Http.Query.Filter.Filters.Condition.Operators.Logical;

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
        public void Parse_GivenQuery_ShouldReturnEmpty(string query)
        {
            Where actual = query;

            actual.Should().BeEmpty();
        }

        public class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                // Equal to
                yield return new object[] { "?filter[where][id]=", Expected(new Condition("id", Empty)) };
                yield return new object[] { "?filter[where][id]=2", Expected(new Condition("id", "2")) };
                yield return new object[] { "?FILTER[WHERE][NAME]=NAME", Expected(new Condition("NAME", "NAME")) };
                yield return new object[] { "?Filter[Where][Surname]=Surname", Expected(new Condition("Surname", "Surname")) };
                yield return new object[] { "?filter%5Bwhere%5D%5Bid%5D=2", Expected(new Condition("id", "2")) };

                // Greater than
                yield return new object[] { "?filter[where][id][gt]=", Expected(new Condition("id", Empty, Comparison.GreaterThan)) };
                yield return new object[] { "?filter[where][id][gt]=2", Expected(new Condition("id", "2", Comparison.GreaterThan)) };
                yield return new object[] { "?FILTER[WHERE][ID][GT]=2", Expected(new Condition("ID", "2", Comparison.GreaterThan)) };
                yield return new object[] { "?Filter[Where][Id][Gt]=2", Expected(new Condition("Id", "2", Comparison.GreaterThan)) };
                yield return new object[] { "?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", Expected(new Condition("id", "2", Comparison.GreaterThan)) };

                // Less than
                yield return new object[] { "?filter[where][id][lt]=", Expected(new Condition("id", Empty, Comparison.LessThan)) };
                yield return new object[] { "?filter[where][id][lt]=2", Expected(new Condition("id", "2", Comparison.LessThan)) };
                yield return new object[] { "?FILTER[WHERE][ID][LT]=2", Expected(new Condition("ID", "2", Comparison.LessThan)) };
                yield return new object[] { "?Filter[Where][Id][Lt]=2", Expected(new Condition("Id", "2", Comparison.LessThan)) };
                yield return new object[] { "?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", Expected(new Condition("id", "2", Comparison.LessThan)) };

                // And
                yield return new object[] { "?filter[where][and][0][id]=&filter[where][and][1][name]=", Expected(new Condition("id", Empty), new Condition("name", Empty, And, 1)) };
                yield return new object[] { "?filter[where][and][0][id]=2&filter[where][and][1][name]=junior", Expected(new Condition("id", "2"), new Condition("name", "junior", And, 1)) };
                yield return new object[] { "?FILTER[WHERE][AND][0][ID]=2&FILTER[WHERE][AND][1][NAME]=junior", Expected(new Condition("ID", "2"), new Condition("NAME", "junior", And, 1)) };
                yield return new object[] { "?Filter[Where][And][0][Id]=2&Filter[Where][And][1][Name]=junior", Expected(new Condition("Id", "2"), new Condition("Name", "junior", And, 1)) };
                yield return new object[] { "?filter%5Bwhere%5D%5Band%5D%5B0%5D%5Bid%5D=2&filter%5Bwhere%5D%5Band%5D%5B1%5D%5Bname%5D=junior", Expected(new Condition("id", "2"), new Condition("name", "junior", And, 1)) };

                // Or
                yield return new object[] { "?filter[where][or][0][id]=&filter[where][or][1][name]=", Expected(new Condition("id", Empty, Or, 0), new Condition("name", Empty, Or, 1)) };
                yield return new object[] { "?filter[where][or][0][id]=2&filter[where][or][1][name]=junior", Expected(new Condition("id", "2", Or, 0), new Condition("name", "junior", Or, 1)) };
                yield return new object[] { "?FILTER[WHERE][OR][0][ID]=2&FILTER[WHERE][OR][1][NAME]=junior", Expected(new Condition("ID", "2", Or, 0), new Condition("NAME", "junior", Or, 1)) };
                yield return new object[] { "?Filter[Where][Or][0][Id]=2&Filter[Where][Or][1][Name]=junior", Expected(new Condition("Id", "2", Or, 0), new Condition("Name", "junior", Or, 1)) };
                yield return new object[] { "?filter%5Bwhere%5D%5Bor%5D%5B0%5D%5Bid%5D=2&filter%5Bwhere%5D%5Bor%5D%5B1%5D%5Bname%5D=junior", Expected(new Condition("id", "2", Or, 0), new Condition("name", "junior", Or, 1)) };
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

            private static Where Expected(params Condition[] conditions) => new Where(conditions);
        }
    }
}
