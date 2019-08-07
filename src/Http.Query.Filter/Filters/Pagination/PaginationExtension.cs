namespace Http.Query.Filter.Filters.Pagination
{
    public static class PaginationExtension
    {
        public static uint GetOrElse(this IPagination @this, uint @default) => @this.Value ?? @default;
    }
}
