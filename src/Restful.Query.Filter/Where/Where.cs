using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Restful.Query.Filter.Where
{
    public class Where
    {
        private const string Pattern = @"filter\[where]\[(?<property>\w+)\](\[(?<op>gt|lt)\])?=(?<value>[^&]*)&?";

        public Operator Operator { get; private set; }
        public Property Property { get; private set; }

        public Where(Property property, Operator @operator)
        {
            Property = property;
            Operator = @operator;
        }

        public static implicit operator Where(string query)
        {
            query = HttpUtility.UrlDecode(query);

            var match = Regex.Match(query, Pattern, RegexOptions.IgnoreCase);

            var property = GetProperty(match);
            if (property == null)
            {
                return null;
            }

            var @operator = GetOperator(match);

            return new Where(property, @operator);
        }

        private static Property GetProperty(Match match)
        {
            var name = match.Groups["property"].Value;
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var value = match.Groups["value"].Value;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return new Property(name, value);
        }

        private static Operator GetOperator(Match match)
        {
            var operation = match.Groups["op"].Value.ToLower();

            var operations = new Dictionary<string, Operator>
            {
                { "gt", Operator.GreaterThan },
                { "lt", Operator.LessThan }
            };

            return operations[operation];
        }
    }
}