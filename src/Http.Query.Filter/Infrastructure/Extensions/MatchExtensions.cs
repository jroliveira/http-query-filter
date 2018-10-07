namespace Http.Query.Filter.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;

    internal static class MatchExtensions
    {
        internal static string Get(this Match match, string group) => match.Groups[group].Value;
    }
}
