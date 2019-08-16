namespace Http.Query.Filter.Integration.Test.Infrastructure.Filter.Extensions
{
    using Http.Query.Filter.Filters.Condition.Operators;

    using static System.Int32;
    using static Http.Query.Filter.Filters.Condition.Operators.Comparison;

    internal static class StringExtension
    {
        internal static bool Verify(this string fieldValue, string queryValue, Comparison comparison) => comparison switch
        {
            GreaterThan => Parse(fieldValue) > Parse(queryValue),
            LessThan => Parse(fieldValue) < Parse(queryValue),
            _ => queryValue == fieldValue,
        };
    }
}
