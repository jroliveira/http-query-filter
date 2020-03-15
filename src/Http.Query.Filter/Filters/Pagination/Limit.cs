namespace Http.Query.Filter.Filters.Pagination
{
    using Http.Query.Filter.Infrastructure;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.UInt32;

    public readonly struct Limit
    {
        private static readonly Pattern[] Patterns =
        {
            @"filter\[limit]\=(?<limit>\d+)",
            @"limit\=(?<limit>\d+)",
        };

        private readonly uint? value;

        private Limit(uint? value) => this.value = value;

        public static implicit operator uint?(Limit limit) => limit.value;

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

        public uint GetOrElse(uint @default) => this.value ?? @default;
    }
}
