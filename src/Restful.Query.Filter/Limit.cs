using System.Text.RegularExpressions;

namespace Restful.Query.Filter
{
    public class Limit
    {
        private const string Pattern = @"filter\[limit]\=(?<limit>\d+)";

        public int Value { get; private set; }

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