﻿namespace Http.Query.Filter.Filters.Visualization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Http.Query.Filter.Infrastructure.Extensions;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.Text.RegularExpressions.RegexOptions;

    public sealed class Fields : ReadOnlyCollection<KeyValuePair<string, bool>>
    {
        private const string Pattern = @"filter\[fields]\[(?<field>\w+)]\=(?<show>true|false)";

        private static readonly Func<string, MatchCollection> Matches = new Regex(Pattern, IgnoreCase | Compiled).Matches;
        private static readonly IReadOnlyDictionary<string, bool> Types = new Dictionary<string, bool>
        {
            { "true", true },
            { "false", false },
        };

        internal Fields(IEnumerable<KeyValuePair<string, bool>> fields)
            : base(fields.ToList())
        {
        }

        public static implicit operator Fields(string query) => new Fields(GetFields(query));

        private static IEnumerable<KeyValuePair<string, bool>> GetFields(string query) => IsNullOrWhiteSpace(query)
            ? new List<KeyValuePair<string, bool>>()
            : new List<KeyValuePair<string, bool>>(
                from Match match in Matches(UrlDecode(query))
                let field = match.GetValue("field")
                let show = GetShow(match)
                select new KeyValuePair<string, bool>(field, show));

        private static bool GetShow(Match match) => Types[match.GetValue("show").ToLower()];
    }
}
