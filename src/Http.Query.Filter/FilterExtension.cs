namespace Http.Query.Filter
{
    public static class FilterExtension
    {
        public static IFilter GetOrElse(this IFilter @this, IFilter @default) => @this ?? @default;
    }
}
