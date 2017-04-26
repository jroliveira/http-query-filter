namespace Http.Query.Filter.Infraestructure.Extensions
{
    using System.Text.RegularExpressions;

    public static class MatchExtensions
    {
        public static string Get(this Match match, string group)
        {
            return match.Groups[group].Value;
        }
    }
}
