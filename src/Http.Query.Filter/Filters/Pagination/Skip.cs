namespace Http.Query.Filter.Filters.Pagination
{
    using Http.Query.Filter.Infrastructure;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.UInt32;

    public readonly struct Skip : IPagination
    {
        private static readonly Pattern[] Patterns =
        {
            @"filter\[skip]\=(?<skip>\d+)",
            @"skip\=(?<skip>\d+)",
        };

        private Skip(uint? value) => this.Value = value;

        public uint? Value { get; }

        public static implicit operator uint?(Skip skip) => skip.Value;

        public static implicit operator Skip(string query)
        {
            if (IsNullOrWhiteSpace(query))
            {
                return default;
            }

            var decodedQuery = UrlDecode(query);

            foreach (var pattern in Patterns)
            {
                if (pattern.TryGetValue(decodedQuery, "skip", TryParse, out uint skip))
                {
                    return new Skip(skip);
                }
            }

            return default;
        }
    }
}
