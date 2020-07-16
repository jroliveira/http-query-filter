namespace Http.Query.Filter.Client.Filters.Condition
{
    public interface IWhere : IFilterBase
    {
        /// <summary>
        /// A where filter specifies a set of logical conditions to match, similar to a WHERE clause in a SQL query.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IFilterBuilder Where(ICondition condition)
        {
            foreach (var filter in condition.InnerConditions)
            {
                this.Builder.AddFilter(filter.ToString());
            }

            return this.Builder;
        }
    }
}
