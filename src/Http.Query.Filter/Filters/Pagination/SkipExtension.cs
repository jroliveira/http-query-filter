namespace Http.Query.Filter.Filters.Pagination
{
    public static class SkipExtension
    {
        public static uint GetOrElse(in this Skip @this, uint @default) => @this.Value ?? @default;
    }
}
