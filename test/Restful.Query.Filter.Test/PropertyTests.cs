﻿using FluentAssertions;
using NUnit.Framework;
using Restful.Query.Filter.Where;

namespace Restful.Query.Filter.Test
{
    [TestFixture]
    public class WhereTests
    {
        [TestCase("?filter[where][id][gt]=2", "2")]
        [TestCase("?FILTER[WHERE][NAME][GT]=TEST", "TEST")]
        [TestCase("?Filter[Where][Surname][Gt]=Unit Test", "Unit Test")]
        public void Parse_DadoQuery_DeveRetornarPropertyValue(string query, object expected)
        {
            Where.Where where = query;

            where.Property.Value.Should().Be(expected);
        }

        [TestCase("?filter[where][id][gt]=2", "id")]
        [TestCase("?FILTER[WHERE][NAME][GT]=TEST", "NAME")]
        [TestCase("?Filter[Where][Surname][Gt]=Test", "Surname")]
        public void Parse_DadoQuery_DeveRetornarPropertyName(string query, string expected)
        {
            Where.Where where = query;

            where.Property.Name.Should().Be(expected);
        }

        [TestCase("?filter[where][id][gt]=2", Operator.GreaterThan)]
        [TestCase("?FILTER[WHERE][ID][GT]=2", Operator.GreaterThan)]
        [TestCase("?Filter[Where][Id][Gt]=2", Operator.GreaterThan)]
        [TestCase("?filter[where][id][lt]=2", Operator.LessThan)]
        [TestCase("?FILTER[WHERE][ID][LT]=2", Operator.LessThan)]
        [TestCase("?Filter[Where][Id][Lt]=2", Operator.LessThan)]
        public void Parse_DadaQuery_DeveRetornarSorts(string query, Operator expected)
        {
            Where.Where where = query;

            where.Operator.Should().Be(expected);
        }

        [TestCase("?filter[condition][id][gt]=1")]
        [TestCase("?filter[where][last name]=Test")]
        [TestCase("?filter[where][id][greaterthan]=1")]
        [TestCase("?filter[where][id][gt]=")]
        public void Parse_DadaQuery_DeveRetornarNull(string query)
        {
            Where.Where where = query;

            where.Should().BeNull();
        }
    }
}
