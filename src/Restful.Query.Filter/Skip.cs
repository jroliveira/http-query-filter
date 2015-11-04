using System.Text.RegularExpressions;
using System.Web;

namespace Restful.Query.Filter
{
    public class Skip
    {
        private const string Pattern = @"filter\[skip]\=(?<skip>\d+)";

        public virtual int Value { get; protected set; }

        protected Skip()
        {

        }

        public Skip(int value)
        {
            Value = value;
        }

        public static implicit operator int(Skip skip)
        {
            return skip.Value;
        }

        public static implicit operator Skip(string query)
        {
            query = HttpUtility.UrlDecode(query);

            var match = Regex.Match(query, Pattern, RegexOptions.IgnoreCase);

            int skip;

            if (int.TryParse(match.Groups["skip"].Value, out skip))
            {
                return new Skip(skip);
            }

            return null;
        }
    }
}