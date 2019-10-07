namespace Http.Query.Filter.Filters.Pagination
{
    internal delegate bool TryParse<TReturn>(string input, out TReturn @return);
}
