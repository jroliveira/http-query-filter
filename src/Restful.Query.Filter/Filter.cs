namespace Restful.Query.Filter
{
    public class Filter
    {
        public Limit Limit { get; private set; }
        public Skip Skip { get; private set; }
        public Order.Order Order { get; private set; }
        public Where.Where Where { get; private set; }

        public bool HasOrder { get { return Order != null; } }
        public bool HasWhere { get { return Where != null; } }

        public static implicit operator Filter(string query)
        {
            var filter = new Filter
            {
                Skip = query,
                Limit = query,
                Order = query,
                Where = query
            };

            return filter;
        }
    }
}