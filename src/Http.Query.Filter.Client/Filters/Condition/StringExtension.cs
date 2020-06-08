namespace Http.Query.Filter.Client.Filters.Condition
{
    using static Http.Query.Filter.Client.Filters.Condition.Field;

    public static class StringExtension
    {
        public static ICondition GreaterThan(this string @this, object value) => NewField(@this).GreaterThan(value);

        public static ICondition LessThan(this string @this, object value) => NewField(@this).LessThan(value);

        public static ICondition Equal(this string @this, object value) => NewField(@this).Equal(value);

        public static ICondition Inq(this string @this, params object[] values) => NewField(@this).Inq(values);
    }
}
