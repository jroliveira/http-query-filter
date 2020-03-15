namespace Http.Query.Filter.Test.Filters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using FluentAssertions;

    using Http.Query.Filter.Filters.Ordering;

    using Xunit;

    using static Http.Query.Filter.Filters.Ordering.OrderByDirection;

    public class OrderByTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void Parse_GivenQuery_ShouldReturn(string query, OrderBy expected)
        {
            OrderBy actual = query;

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[order]=name%20des")]
        [InlineData("?filter[order]=last name asc")]
        public void Parse_GivenQuery_ShouldReturnEmpty(string query)
        {
            OrderBy actual = query;

            actual.Should().BeEmpty();
        }

        public class TestData : IEnumerable<object[]>
        {
            private static readonly Func<string, OrderByDirection, OrderBy> OrderBy = (key, value) => OrderByData(new List<KeyValuePair<string, OrderByDirection>>
            {
                new KeyValuePair<string, OrderByDirection>(key, value),
            });

            private static readonly Func<IEnumerable<KeyValuePair<string, OrderByDirection>>, OrderBy> OrderByData = data => new OrderBy(data);

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "?filter[order]=id asc", OrderBy("id", Ascending) };
                yield return new object[] { "?FILTER[ORDER]=ID DESC", OrderBy("ID", Descending) };
                yield return new object[] { "?filter%5Border%5D=id%20asc", OrderBy("id", Ascending) };
                yield return new object[]
                {
                    "?filter[order][0]=id asc&filter[order][1]=name desc", OrderByData(new List<KeyValuePair<string, OrderByDirection>>
                    {
                        new KeyValuePair<string, OrderByDirection>("id", Ascending),
                        new KeyValuePair<string, OrderByDirection>("name", Descending),
                    }),
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}
