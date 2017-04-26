namespace Http.Query.Filter.Filters.Visualization
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infraestructure.Extensions;

    public class Fields : Collection<Field>
    {
        private const string Pattern = @"filter\[fields]\[(?<property>\w+)]\=(?<show>true|false)";

        public Fields(IEnumerable<Field> fields)
        {
            foreach (var field in fields)
            {
                this.Items.Add(field);
            }
        }

        protected Fields()
        {
        }

        public static implicit operator Fields(string query)
        {
            query = WebUtility.UrlDecode(query);

            var fields = Get(query);
            if (fields == null || !fields.Any())
            {
                return null;
            }

            return new Fields(fields);
        }

        private static IEnumerable<Field> Get(string query)
        {
            var matches = Regex.Matches(query, Pattern, RegexOptions.IgnoreCase);

            return
                from Match match in matches
                let property = match.Get("property")
                let show = GetShow(match)
                select new Field(property, show);
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