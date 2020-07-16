namespace Http.Query.Filter.Client.Filters
{
    public interface ISelect : IFilterBase
    {
        /// <summary>
        /// A select filter specifies properties to include from the results.
        /// </summary>
        /// <param name="fields">
        /// fields are the names of the properties to include from the results.
        /// </param>
        /// <returns></returns>
        IFilterBuilder Select(params string[] fields)
        {
            foreach (var field in fields)
            {
                this.Builder.AddFilter($"filter[fields][{field}]=true");
            }

            return this.Builder;
        }
    }
}
