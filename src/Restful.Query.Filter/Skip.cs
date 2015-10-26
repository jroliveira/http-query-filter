using System.Text.RegularExpressions;

namespace Restful.Query.Filter
{
    public class Skip
    {
        private const string Pattern = @"filter\[skip]\=(?<skip>\d+)";

        public int Value { get; private set; }

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