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
            &filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2
            &filter%5Bwhere%5D%5Bid%5D%5Blt%5D=2
            &filter%5Bwhere%5D%5Bid%5D=2
            &filter%5Bfields%5D%5Bid%5D=false";

        private const string QueryDecoded = @"
            ?filter[skip]=1
            &filter[limit]=2
            &filter[order][0]=id%20desc
            &filter[order][1]=name%20asc
            &filter[where][id][gt]=2
            &filter[where][id][lt]=2
            &filter[where][id]=2
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
        public void Parse_DadaQueryComOrder_OrderNaoPodeSerNull(string query)
        {
            Filter actual = query;

            actual.Order.Should().NotBeNull();
            actual.HasOrder.Should().BeTrue();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComWhere_WhereNaoPodeSerNull(string query)
        {
            Filter actual = query;

            actual.Where.Should().NotBeNull();
            actual.HasWhere.Should().BeTrue();
        }
    }
}
