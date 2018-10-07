namespace Http.Query.Filter.Test.Filters
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Http.Query.Filter.Filters.Ordering;
    using Http.Query.Filter.Test.Utils;
    using Xunit;

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
        public void Parse_GivenQuery_ShouldReturnNull(string query)
        {
            OrderBy actual = query;

            actual.Should().BeNull();
        }

        public class TestData : KeyValuePairTestData<OrderByDirection, OrderBy>
        {
            protected override List<object[]> Data => new List<object[]>
            {
                new object[] { "?filter[order]=id asc",       Field("id", OrderByDirection.Ascending) },
                new object[] { "?FILTER[ORDER]=ID DESC",      Field("ID", OrderByDirection.Descending) },
                new object[] { "?filter%5Border%5D=id%20asc", Field("id", OrderByDirection.Ascending) },
                new object[]
                {
                    "?filter[order][0]=id asc&filter[order][1]=name desc",
                    Fields(data =>
                    {
                        data.Add(Item("id", OrderByDirection.Ascending));
                        data.Add(Item("name", OrderByDirection.Descending));
                    }),
                },
            };
        }
    }
}
