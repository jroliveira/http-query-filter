namespace Http.Query.Filter.Test.Filters
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Http.Query.Filter.Filters.Visualization;
    using Http.Query.Filter.Test.Utils;

    using Xunit;

    public class FieldsTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void Parse_DadoQuery_DeveRetornar(string query, Fields expected)
        {
            Fields actual = query;

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[fields][id]=1")]
        [InlineData("?filter[fields][id]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Fields actual = query;

            actual.Should().BeNull();
        }

        public class TestData : KeyValuePairTestData<bool, Fields>
        {
            protected override List<object[]> Data => new List<object[]>
            {
                new object[] { "?filter[fields][id]=true",          Field("id", true) },
                new object[] { "?filter[fields][id]=false",         Field("id", false) },
                new object[] { "?FILTER[FIELDS][ID]=TRUE",          Field("ID", true) },
                new object[] { "?filter%5Bfields%5D%5Bid%5D=false", Field("id", false) },
                new object[]
                {
                    "?filter[fields][id]=true&filter[fields][name]=false",
                    Fields(data =>
                    {
                        data.Add(Item("id", true));
                        data.Add(Item("name", false));
                    })
                }
            };
        }
    }
}
