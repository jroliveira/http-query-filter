namespace Http.Query.Filter.Integration.Test.Infrastructure.Filter.Extensions
{
    using static System.Reflection.BindingFlags;

    internal static class ObjectExtension
    {
        internal static object GetOrElse<TObject>(this TObject @this, string property, object @default) => @this == null
            ? @default
            : @this
                .GetType()
                .GetProperty(property, IgnoreCase | Public | Instance)
                ?.GetValue(@this) ?? @default;
    }
}
