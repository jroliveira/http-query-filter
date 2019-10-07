namespace Http.Query.Filter.Filters.Pagination
{
    using Http.Query.Filter.Infrastructure;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.UInt32;

    public readonly struct Limit : IPagination
    {
        private static readonly Pattern[] Patterns =
        {
            @"filter\[limit]\=(?<limit>\d+)",
            @"limit\=(?<limit>\d+)",
        };

        private Limit(uint? value) => this.Value = value;

        public uint? Value { get; }

        public static implicit operator uint?(Limit limit) => limit.Value;

        public static implicit operator Limit(string query)
        {
            if (IsNullOrWhiteSpace(query))
            {
                return default;
            }

            var decodedQuery = UrlDecode(query);

            foreach (var pattern in Patterns)
            {
                if (pattern.TryGetValue(decodedQuery, "limit", TryParse, out uint limit))
                {
                    return new Limit(limit);
                }
            }

            return default;
        }
    }
}
