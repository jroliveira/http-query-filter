namespace Http.Query.Filter.Infrastructure
{
    internal delegate bool TryParse<TReturn>(string input, out TReturn @return);
}
