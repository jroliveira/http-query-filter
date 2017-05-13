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

        public Fields(IList<KeyValuePair<string, bool>> fields)
            : base(fields)
        {
        }

        public static implicit operator Fields(string query)
        {
            query = WebUtility.UrlDecode(query);

            var data = Get(query);
            var fields = data as IList<KeyValuePair<string, bool>> ?? data.ToList();

            if (!fields.Any())
            {
                return null;
            }

            return new Fields(fields);
        }

        private static IEnumerable<KeyValuePair<string, bool>> Get(string query)
        {
            var matches = Regex.Matches(query, Pattern, RegexOptions.IgnoreCase);

            return
                from Match match in matches
                let field = match.Get("field")
                let show = GetShow(match)
                select new KeyValuePair<string, bool>(field, show);
        }

        private static bool GetShow(Match match)
        {
            var show = match.Groups["show"].Value.ToLower();

            var types = new Dictionary<string, bool>
            {
                { "true", true },
                { "false", false }
            };

            return types[show];
        }
    }
}