namespace Http.Query.Filter.Client
{
    using System.Collections.Generic;

    public sealed class FilterBuilder : IFilterBuilder
    {
        private FilterBuilder() => this.Filters = new List<string>();

        public ICollection<string> Filters { get; }

        public IFilterBuilder Builder => this;

        public static IFilterBuilder NewFilterBuilder() => new FilterBuilder();
    }
}
