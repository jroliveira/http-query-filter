namespace Http.Query.Filter.Infrastructure
{
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infrastructure.Extensions;

    using static System.Text.RegularExpressions.RegexOptions;

    internal sealed class Pattern
    {
        private readonly Regex regex;

        private Pattern(Regex regex) => this.regex = regex;

        public static implicit operator Pattern(string pattern) => new Pattern(new Regex(pattern, IgnoreCase | Compiled));

        internal bool TryGetValue<TReturn>(string input, string group, TryParse<TReturn> tryParse, out TReturn @return)
        {
            @return = default;

            return this.TryMatch(input, out var match) && tryParse(match.GetValue(group), out @return);
        }

        internal bool TryMatch(string input, out Match match)
        {
            match = this.regex.Match(input);

            return match.Success;
        }
    }
}
