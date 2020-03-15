namespace Http.Query.Filter.Client.Filters.Pagination
{
    public interface ISkip : IFilter
    {
        /// <summary>
        /// A skip filter omits the specified number of returned records.
        /// </summary>
        /// <param name="skip">skip is the number of records to skip.</param>
        /// <returns></returns>
        IFilter Skip(uint skip)
        {
            if (skip < 1)
            {
                skip = 1;
            }

            this.AddFilter($"filter[skip]={skip}");
            return this;
        }
    }
}
