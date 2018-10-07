namespace Http.Query.Filter.Filters.Pagination
{
    public static class LimitExtension
    {
        public static uint GetOrElse(in this Limit @this, uint @default) => @this.Value ?? @default;
    }
}
