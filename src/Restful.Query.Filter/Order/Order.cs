using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Restful.Query.Filter.Order
{
    public class Order
    {
        private const string Pattern = @"filter\[order](\[\d+])?\=(?<property>\w+)(,(\s)?(?<property>\w+))*\s(?<sorts>asc|desc)";

        public virtual IEnumerable<Field> Fields { get; protected set; }

        protected Order()
        {

        }

        public Order(IEnumerable<Field> fields)
        {
            Fields = fields;
        }

        public static implicit operator Order(string query)
        {
            query = HttpUtility.UrlDecode(query);

            var fields = Get(query);
            if (fields == null || !fields.Any())
            {
                return null;
            }

            return new Order(fields);
        }

        private static IEnumerable<Field> Get(string query)
        {
            var matches = Regex.Matches(query, Pattern, RegexOptions.IgnoreCase);

            var types = new Dictionary<string, Sorts>
            {
                { "asc", Sorts.Asc},
                { "desc", Sorts.Desc }
            };

            return
                from
                    Match m in matches

                let property = from
                                   object capture in m.Groups["property"].Captures
                               select capture.ToString()

                let sorts = from
                                object capture in m.Groups["sorts"].Captures
                            select capture.ToString().ToLower()

                select new Field
                {
                    Name = property.First(),
                    Sorts = types[sorts.First()]
                };
        }
    }
}