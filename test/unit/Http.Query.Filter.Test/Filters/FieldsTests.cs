namespace Http.Query.Filter.Test.Filters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using FluentAssertions;

    using Http.Query.Filter.Filters.Visualization;

    using Xunit;

    public class FieldsTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void Parse_GivenQuery_ShouldReturn(string query, Fields expected)
        {
            Fields actual = query;

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("?filter[fields][id]=1")]
        [InlineData("?filter[fields][id]=")]
        public void Parse_GivenQuery_ShouldReturnEmpty(string query)
        {
            Fields actual = query;

            actual.Should().BeEmpty();
        }

        public class TestData : IEnumerable<object[]>
        {
            private static readonly Func<string, bool, Fields> Field = (key, value) => Fields(new List<KeyValuePair<string, bool>>
            {
                new KeyValuePair<string, bool>(key, value),
            });

            private static readonly Func<IEnumerable<KeyValuePair<string, bool>>, Fields> Fields = data => new Fields(data);

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "?filter[fields][id]=true", Field("id", true) };
                yield return new object[] { "?filter[fields][id]=false", Field("id", false) };
                yield return new object[] { "?FILTER[FIELDS][ID]=TRUE", Field("ID", true) };
                yield return new object[] { "?filter%5Bfields%5D%5Bid%5D=false", Field("id", false) };
                yield return new object[]
                {
                    "?filter[fields][id]=true&filter[fields][name]=false",
                    Fields(new List<KeyValuePair<string, bool>>
                    {
                        new KeyValuePair<string, bool>("id", true),
                        new KeyValuePair<string, bool>("name", false),
                    }),
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}
