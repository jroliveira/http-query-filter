namespace Http.Query.Filter.Client.Filters.Pagination
{
    public interface ISkip : IFilterBase
    {
        /// <summary>
        /// A skip filter omits the specified number of returned records.
        /// </summary>
        /// <param name="skip">skip is the number of records to skip.</param>
        /// <returns></returns>
        IFilterBuilder Skip(uint skip)
        {
            if (skip < 1)
            {
                skip = 1;
            }

            return this.Builder
                .AddFilter($"filter[skip]={skip}");
        }
    }
}
