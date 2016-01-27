using Restful.Query.Filter.Filters;
using Restful.Query.Filter.Filters.Condition;
using Restful.Query.Filter.Filters.Ordering;
using Restful.Query.Filter.Filters.Visualization;

namespace Restful.Query.Filter
{
    public class Filter
    {
        public virtual Limit Limit { get; protected set; }
        public virtual Skip Skip { get; protected set; }
        public virtual OrderBy OrderBy { get; protected set; }
        public virtual Where Where { get; protected set; }
        public virtual Fields Fields { get; protected set; }

        public virtual bool HasCondition { get { return Where != null; } }
        public virtual bool HasOrdering { get { return OrderBy != null; } }
        public virtual bool HasVisualization { get { return Fields != null; } }

        protected Filter()
        {

        }

        public static implicit operator Filter(string query)
        {
            var filter = new Filter
            {
                Skip = query,
                Limit = query,
                OrderBy = query,
                Where = query,
                Fields = query
            };

            return filter;
        }
    }
}