using FluentAssertions;
using NUnit.Framework;

namespace Restful.Query.Filter.Test
{
    [TestFixture]
    public class FilterTests
    {
        private const string Query = "?filter%5Bskip%5D=1&filter%5Blimit%5D=2&filter%5Border%5D=id%20desc&filter%5Bwhere%5D%5Bid%5D%5Bgt%5D=2";
        private const string QueryDecoded = "?filter[skip]=1&filter[limit]=2&filter[order]=id%20desc&filter[where][id][gt]=2";

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComSkip_SkipNaoPodeSerNull(string query)
        {
            Filter filter = query;

            filter.Skip.Should().NotBeNull();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComLimit_LimitNaoPodeSerNull(string query)
        {
            Filter filter = query;

            filter.Limit.Should().NotBeNull();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComOrder_OrderNaoPodeSerNull(string query)
        {
            Filter filter = query;

            filter.Order.Should().NotBeNull();
            filter.HasOrder.Should().BeTrue();
        }

        [TestCase(Query)]
        [TestCase(QueryDecoded)]
        public void Parse_DadaQueryComWhere_WhereNaoPodeSerNull(string query)
        {
            Filter filter = query;

            filter.Where.Should().NotBeNull();
            filter.HasWhere.Should().BeTrue();
        }
    }
}
