namespace Http.Query.Filter.Filters.Pagination
{
    using Http.Query.Filter.Infrastructure;

    using static System.Net.WebUtility;
    using static System.String;
    using static System.UInt32;

    public readonly struct Skip
    {
        private static readonly Pattern[] Patterns =
        {
            @"filter\[skip]\=(?<skip>\d+)",
            @"skip\=(?<skip>\d+)",
        };

        private readonly uint? value;

        private Skip(uint? value) => this.value = value;

        public static implicit operator uint?(Skip skip) => skip.value;

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

        public uint GetOrElse(uint @default) => this.value ?? @default;
    }
}
