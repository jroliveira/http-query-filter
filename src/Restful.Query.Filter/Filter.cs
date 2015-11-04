namespace Restful.Query.Filter
{
    public class Filter
    {
        public virtual Limit Limit { get; protected set; }
        public virtual Skip Skip { get; protected set; }
        public virtual Order.Order Order { get; protected set; }
        public virtual Where.Where Where { get; protected set; }

        public virtual bool HasOrder { get { return Order != null; } }
        public virtual bool HasWhere { get { return Where != null; } }

        protected Filter()
        {

        }

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