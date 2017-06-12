namespace Http.Query.Filter.Filters.Visualization
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infraestructure.Extensions;

    public class Fields : ReadOnlyCollection<KeyValuePair<string, bool>>
    {
        private const string Pattern = @"filter\[fields]\[(?<field>\w+)]\=(?<show>true|false)";

        private static Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static Dictionary<string, bool> types = new Dictionary<string, bool>
            {
                { "true", true },
                { "false", false }
            };

        public Fields(IList<KeyValuePair<string, bool>> fields)
                : base(fields)
        {
        }

        public static implicit operator Fields(string query)
        {
            var fields = Get(WebUtility.UrlDecode(query));

            if (!fields.Any())
            {
                return null;
            }

            return new Fields(fields);
        }

        private static IList<KeyValuePair<string, bool>> Get(string query)
        {
            var matches = regex.Matches(query);

            return
                (from Match match in matches
                 let field = match.Get("field")
                 let show = GetShow(match)
                 select new KeyValuePair<string, bool>(field, show)).ToList();
        }

        private static bool GetShow(Match match)
        {
            var show = match.Groups["show"].Value.ToLower();

            return types[show];
        }
    }
}