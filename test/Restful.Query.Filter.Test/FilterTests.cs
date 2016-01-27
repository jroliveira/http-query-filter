using FluentAssertions;
using NUnit.Framework;

namespace Restful.Query.Filter.Test
{
    [TestFixture]
    public class FilterTests
    {
        private const string Query = @"
            ?filter%5Bskip%5D=1
            &filter%5Blimit%5D=2
            &filter%5Border%5D%5B0%5D=id%20desc
            &filter%5Border%5D%5B1%5D=name%20asc
            &filter%5Bwhere%5D%5Bor%5D%5B0%5D%5Bid%5D=2
            &filter%5Bwhere%5D%5Bor%5D%5B1%5D%5Bid%5D=4
            &filter%5Bfields%5D%5Bid%5D=false";

        private const string QueryDecoded = @"
            ?filter[skip]=1
            &filter[limit]=2
            &filter[order][0]=id desc
            &filter[order][1]=name asc
            &filter[where][or][0][id]=2
            &filter[where][or][1][id]=4
            &filter[fields][id]=false";

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComSkip_SkipNaoPodeSerNull(string query)
        {
            Filter actual = query;

            actual.Skip.Should().NotBeNull();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComLimit_LimitNaoPodeSerNull(string query)
        {
            Filter actual = query;

            actual.Limit.Should().NotBeNull();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComOrderBy_OrderByNaoPodeSerNull(string query)
        {
            Filter actual = query;

            actual.OrderBy.Should().NotBeNull();
            actual.HasOrdering.Should().BeTrue();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComOrderBy_OrderByDeveConter2Elementos(string query)
        {
            Filter actual = query;

            actual.OrderBy.Count.Should().Be(2);
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComWhere_WhereNaoPodeSerNull(string query)
        {
            Filter actual = query;

            actual.Where.Should().NotBeNull();
            actual.HasCondition.Should().BeTrue();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComWhere_WhereDeveConter2Elementos(string query)
        {
            Filter actual = query;

            actual.Where.Count.Should().Be(2);
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComFields_FieldsNaoPodeSerNull(string query)
        {
            Filter actual = query;

            actual.Fields.Should().NotBeNull();
            actual.HasVisualization.Should().BeTrue();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComFields_FieldsDeveConter1Elemento(string query)
        {
            Filter actual = query;

            actual.Fields.Count.Should().Be(1);
        }
    }
}
