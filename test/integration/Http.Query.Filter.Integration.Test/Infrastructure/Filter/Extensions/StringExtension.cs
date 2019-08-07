namespace Http.Query.Filter.Integration.Test.Infrastructure.Filter.Extensions
{
    using System;

    using Http.Query.Filter.Filters.Condition.Operators;

    using static System.Int32;
    using static Http.Query.Filter.Filters.Condition.Operators.Comparison;

    internal static class StringExtension
    {
        internal static bool Verify(this string fieldValue, string queryValue, Comparison comparison)
        {
            switch (comparison)
            {
                case GreaterThan:
                    return Parse(fieldValue) > Parse(queryValue);

                case LessThan:
                    return int.Parse(fieldValue) < Parse(queryValue);

                case Equal:
                    return queryValue == fieldValue;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
