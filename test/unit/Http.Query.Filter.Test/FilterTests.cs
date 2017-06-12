namespace Http.Query.Filter.Test
{
    using FluentAssertions;

    using Xunit;

    public class FilterTests
    {
        private const string Query = @"
            ?filter%5Bskip%5D=1
            &filter%5Blimit%5D=2
            &filter%5Border%5D%5B0%5D=id%20desc
            &filter%5Border%5D%5B1%5D=name%20asc
            &filter%5Bwhere%5D%5Bid%5D=2
            &filter%5Bwhere%5D%5Bid%5D=4
            &filter%5Bfields%5D%5Bid%5D=false";

        private const string QueryDecoded = @"
            ?filter[skip]=1
            &filter[limit]=2
            &filter[order][0]=id desc
            &filter[order][1]=name asc
            &filter[where][id]=2
            &filter[where][id]=4
            &filter[fields][id]=false";

        [Theory]
        [InlineData(Query)]
        [InlineData(QueryDecoded)]
        public void Parse_GivenQuery_SkipCannotBeNull(string query)
        {
            Filter actual = query;

            actual.Skip.Should().NotBeNull();
        }

        [Theory]
        [InlineData(Query)]
        [InlineData(QueryDecoded)]
        public void Parse_GivenQuery_LimitCannotBeNull(string query)
        {
            Filter actual = query;

            actual.Limit.Should().NotBeNull();
        }

        [Theory]
        [InlineData(Query)]
        [InlineData(QueryDecoded)]
        public void Parse_GivenQuery_OrderByCannotBeNull(string query)
        {
            Filter actual = query;

            actual.OrderBy.Should().NotBeNull();
            actual.HasOrdering.Should().BeTrue();
        }

        [Theory]
        [InlineData(Query)]
        [InlineData(QueryDecoded)]
        public void Parse_GivenQuery_OrderByMustContainTwoElements(string query)
        {
            Filter actual = query;

            actual.OrderBy.Count.Should().Be(2);
        }

        [Theory]
        [InlineData(Query)]
        [InlineData(QueryDecoded)]
        public void Parse_GivenQuery_WhereCannotBeNull(string query)
        {
            Filter actual = query;

            actual.Where.Should().NotBeNull();
            actual.HasCondition.Should().BeTrue();
        }

        [Theory]
        [InlineData(Query)]
        [InlineData(QueryDecoded)]
        public void Parse_GivenQuery_WhereMustContainTwoElements(string query)
        {
            Filter actual = query;

            actual.Where.Count.Should().Be(2);
        }

        [Theory]
        [InlineData(Query)]
        [InlineData(QueryDecoded)]
        public void Parse_GivenQuery_FieldsCannotBeNull(string query)
        {
            Filter actual = query;

            actual.Fields.Should().NotBeNull();
            actual.HasVisualization.Should().BeTrue();
        }

        [Theory]
        [InlineData(Query)]
        [InlineData(QueryDecoded)]
        public void Parse_GivenQuery_FieldsMustContainOneElement(string query)
        {
            Filter actual = query;

            actual.Fields.Count.Should().Be(1);
        }
    }
}
