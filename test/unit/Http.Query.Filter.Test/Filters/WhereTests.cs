namespace Http.Query.Filter.Test.Filters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using FluentAssertions;

    using Http.Query.Filter.Filters.Condition;
    using Http.Query.Filter.Filters.Condition.Operators;

    using Xunit;

    using static System.String;
    using static Http.Query.Filter.Filters.Condition.Operators.Comparison;

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
            private static readonly Func<string, string, Comparison, Where> Field = (field, value, comparison) => new Where(new List<Condition>
            {
                new Condition(field, value, comparison),
            });

            public IEnumerator<object[]> GetEnumerator()
            {
                // Equal to
                yield return new object[] { "?filter[where][id]=", Field("id", Empty, Equal) };
                yield return new object[] { "?filter[where][id]=2", Field("id", "2", Equal) };
                yield return new object[] { "?FILTER[WHERE][NAME]=NAME", Field("NAME", "NAME", Equal) };
                yield return new object[] { "?Filter[Where][Surname]=Surname", Field("Surname", "Surname", Equal) };
                yield return new object[] { "?filter%5Bwhere%5D%5Bid%5D=2", Field("id", "2", Equal) };

                // Greater than
                yield return new object[] { "?filter[where][id][gt]=", Field("id", Empty, GreaterThan) };
                yield return new object[] { "?filter[where][id][gt]=2", Field("id", "2", GreaterThan) };
                yield return new object[] { "?FILTER[WHERE][ID][GT]=2", Field("ID", "2", GreaterThan) };
                yield return new object[] { "?Filter[Where][Id][Gt]=2", Field("Id", "2", GreaterThan) };
                yield return new object[] { "?filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2", Field("id", "2", GreaterThan) };

                // Less than
                yield return new object[] { "?filter[where][id][lt]=", Field("id", Empty, LessThan) };
                yield return new object[] { "?filter[where][id][lt]=2", Field("id", "2", LessThan) };
                yield return new object[] { "?FILTER[WHERE][ID][LT]=2", Field("ID", "2", LessThan) };
                yield return new object[] { "?Filter[Where][Id][Lt]=2", Field("Id", "2", LessThan) };
                yield return new object[] { "?filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2", Field("id", "2", LessThan) };
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}
