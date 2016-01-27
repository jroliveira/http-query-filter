using System.Text.RegularExpressions;
using System.Web;

namespace Restful.Query.Filter.Filters
{
    public class Limit
    {
        private const string Pattern = @"filter\[limit]\=(?<limit>\d+)";

        public virtual int Value { get; protected set; }

        protected Limit()
        {
        }

        public Limit(int value)
        {
            Value = value;
        }

        public static implicit operator int(Limit limit)
        {
            return limit.Value;
        }

        public static implicit operator Limit(string query)
        {
            query = HttpUtility.UrlDecode(query);

            var match = Regex.Match(query, Pattern, RegexOptions.IgnoreCase);

            int limit;

            if (int.TryParse(match.Groups["limit"].Value, out limit))
            {
                return new Limit(limit);
            }

            return null;
        }
    }
}