namespace Http.Query.Filter.Integration.Test.Infraestructure.Filter.Extensions
{
    using System;

    using Http.Query.Filter.Filters.Condition.Operators;

    internal static class ObjectExtension
    {
        public static bool Verify(this string fieldValue, string queryValue, Comparison comparison)
        {
            switch (comparison)
            {
                case Comparison.GreaterThan:
                    return int.Parse(fieldValue) > int.Parse(queryValue);

                case Comparison.LessThan:
                    return int.Parse(fieldValue) < int.Parse(queryValue);

                case Comparison.Equal:
                    return queryValue == fieldValue;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}