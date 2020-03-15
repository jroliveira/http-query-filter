namespace Http.Query.Filter.Client.Filters.Pagination
{
    public interface ILimit : IFilter
    {
        /// <summary>
        /// A limit filter limits the number of records returned to the specified number (or less).
        /// </summary>
        /// <param name="limit">limit is the maximum number of results (records) to return.</param>
        /// <returns></returns>
        IFilter Limit(uint limit)
        {
            if (limit < 1)
            {
                limit = 1;
            }

            this.AddFilter($"filter[limit]={limit}");
            return this;
        }
    }
}
