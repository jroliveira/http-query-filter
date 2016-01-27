using System.Text.RegularExpressions;

namespace Restful.Query.Filter.Infraestructure.Extensions
{
    public static class MatchExtensions
    {
        public static string Get(this Match match, string group)
        {
            return match.Groups[group].Value;
        }
    }
}
