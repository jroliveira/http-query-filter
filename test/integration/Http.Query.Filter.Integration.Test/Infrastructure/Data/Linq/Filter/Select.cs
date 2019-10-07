namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Http.Query.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter;
    using Http.Query.Filter.Integration.Test.Infrastructure.Filter.Extensions;

    internal readonly struct Select<TParam> : ISelect<Filter, TParam>
    {
        public Func<TParam, dynamic> Apply(Filter filter) => param =>
        {
            if (param == null)
            {
                return new Dictionary<string, object>();
            }

            if (!filter.HasVisualization)
            {
                return param;
            }

            var props = new Dictionary<string, object>();

            foreach (var (key, _) in filter.Fields.Where(field => field.Value))
            {
                if (param.GetOrElse(key, new { }) is { } value)
                {
                    props.Add(key, value);
                }
            }

            return props;
        };
    }
}
