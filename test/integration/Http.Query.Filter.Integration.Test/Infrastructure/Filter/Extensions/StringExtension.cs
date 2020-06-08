namespace Http.Query.Filter.Integration.Test.Infrastructure.Filter.Extensions
{
    using Http.Query.Filter.Filters.Condition.Operators;

    using static System.Int32;
    using static System.String;

    using static Http.Query.Filter.Filters.Condition.Operators.Comparison;

    internal static class StringExtension
    {
        internal static bool Verify(this string? @this, string queryValue, Comparison comparison)
        {
            @this ??= Empty;

            return comparison switch
            {
                GreaterThan => Parse(@this) > Parse(queryValue),
                LessThan => Parse(@this) < Parse(queryValue),
                Inq => queryValue == @this,
                _ => queryValue == @this,
            };
        }
    }
}
