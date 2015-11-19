using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Restful.Query.Filter.Fields
{
    public class Fields : Collection<Field>
    {
        private const string Pattern = @"filter\[fields]\[(?<property>\w+)]\=(?<show>true|false)";

        protected Fields()
        {

        }

        public Fields(IEnumerable<Field> fields)
        {
            foreach (var field in fields)
            {
                Items.Add(field);
            }
        }

        public static implicit operator Fields(string query)
        {
            query = HttpUtility.UrlDecode(query);

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

            var types = new Dictionary<string, bool>
            {
                { "true", true },
                { "false", false }
            };

            return
                from
                    Match m in matches

                let property = from
                                   object capture in m.Groups["property"].Captures
                               select capture.ToString()

                let sorts = from
                                object capture in m.Groups["show"].Captures
                            select capture.ToString().ToLower()

                select new Field(property.First(), types[sorts.First()]);
        }
    }
}