namespace Http.Query.Filter.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;

    internal static class MatchExtension
    {
        internal static string GetValue(this Match @this, string group) => @this.Groups[group].Value;
    }
}
